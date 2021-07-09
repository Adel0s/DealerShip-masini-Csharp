using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace CarDealership
{
    public partial class Cumpara : Form
    {
        SqlConnection con;
        Masini masini;
        public Cumpara(SqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            
            dataGridView1.CellClick += (a, b) =>
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].FormattedValue);
                masini = new Masini(con,id);
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].FormattedValue.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].FormattedValue.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].FormattedValue.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].FormattedValue.ToString();
                textBox6.Text = masini.Descriere;
                pictureBox1.BackgroundImage = Image.FromFile(masini.CaleFisier);
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            };
        }

        void populate()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("SELECT * FROM Masini WHERE Status=@status", con);
            cmd.Parameters.AddWithValue("status", 0);
            var red = cmd.ExecuteReader();
            while (red.Read())
            {
                dataGridView1.Rows.Add(red[0].ToString(), red[1].ToString(), red[2].ToString(), red[3].ToString(), red[4].ToString());
            }
        }

        private void Cumpara_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Card");
            comboBox1.Items.Add("Cash");
            populate();
            button1.BackColor = Color.FromArgb(40, 199, 250);
            button2.BackColor = Color.FromArgb(40, 199, 250);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dataCurenta = DateTime.UtcNow.Date;
            string dataCumparare = dataCurenta.ToString();
            dataCumparare = dataCurenta.ToString("dd/MM/yyyy");

            if (comboBox1.Text != "")
            {
                if (textBox5.Text != "")
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].FormattedValue);
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    DataGridViewRow newDataRow = dataGridView1.Rows[rowIndex];
                    dataGridView1.Rows.RemoveAt(rowIndex);
                    SqlCommand cmd = new SqlCommand("DELETE FROM Masini WHERE IdMasina = @id", con);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    if (MessageBox.Show("Printati factura?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        PrintPreviewDialog p = new PrintPreviewDialog();
                        p.Document = new PrintDocument();
                        p.Document.PrintPage += (a, b) =>
                        {
                            b.Graphics.DrawString("======================================================", DefaultFont, Brushes.Black, 10, 140);
                            b.Graphics.DrawString("Nume Cumparator: " + textBox5.Text, DefaultFont, Brushes.Black, 10, 200);
                            b.Graphics.DrawString("Marca: " + textBox1.Text, DefaultFont, Brushes.Black, 10, 240);
                            b.Graphics.DrawString("Model: " + textBox2.Text, DefaultFont, Brushes.Black, 10, 280);
                            b.Graphics.DrawString("Pret: " + textBox4.Text, DefaultFont, Brushes.Black, 10, 320);
                            b.Graphics.DrawString("Metoda de plata: " + comboBox1.Text, DefaultFont, Brushes.Black, 10, 360);
                            b.Graphics.DrawString("Data: " + dataCumparare, DefaultFont, Brushes.Black, 300, 390);
                            b.Graphics.DrawString("======================================================", DefaultFont, Brushes.Black, 10, 420);
                        };
                        p.ShowDialog();
                        this.Close();
                    }
                }
                else MessageBox.Show("Numele este obligatoriu!");
            }
            else MessageBox.Show("Alegeti o metoda de plata!");
           
        }
    }
}
