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
using System.Diagnostics;

namespace CarDealership
{
    public partial class Detalii : Form
    {
        SqlConnection con;
        Masini masini;
        double dDays;
        int days;
        int pretTotal;

        public Detalii(SqlConnection con, Masini masini)
        {
            InitializeComponent();
            this.con = con;
            this.masini = masini;
        }

        private void Detalii_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";

            dateTimePicker1.Text = DateTime.Now.ToShortTimeString();
            dateTimePicker2.Text = DateTime.Now.ToShortTimeString();

            button1.BackColor = Color.FromArgb(40, 199, 250);
            button2.BackColor = Color.FromArgb(40, 199, 250);
            label1.Text = label1.Text + " " + masini.Marca;
            label2.Text = label2.Text + " " + masini.Model ;
            label3.Text = label3.Text + " " + masini.An;
            label4.Text = label4.Text + " " + masini.Pret + " EURO";
            textBox5.Text = masini.Descriere;
            pictureBox1.BackgroundImage = Image.FromFile(masini.CaleFisier);
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Masini SET DataInchiriere=@di, DataReturnare=@dr WHERE idMasina = @id", con);
            cmd.Parameters.AddWithValue("id", masini.IdMasina);
            cmd.Parameters.AddWithValue("di", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("dr", dateTimePicker2.Text);

            string nume = textBox1.Text;
            string dataInceput = dateTimePicker1.Text;
            string dataSfarsit = dateTimePicker2.Text;
            DateTime d1 = dateTimePicker1.Value;
            DateTime d2 = dateTimePicker2.Value;
            TimeSpan t = d2 - d1;
            dDays = t.TotalDays;
            days = Convert.ToInt32(dDays);

            if (textBox1.Text != "")
            {
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    if (MessageBox.Show("Inchiriere reusita!Printati factura?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        PrintPreviewDialog p = new PrintPreviewDialog();
                        p.Document = new PrintDocument();
                        p.Document.PrintPage += (a, b) =>
                        {
                            b.Graphics.DrawString("======================================================", DefaultFont, Brushes.Black, 10, 140);
                            b.Graphics.DrawString("Nume cumprator: " + textBox1.Text, DefaultFont, Brushes.Black, 10, 200);
                            b.Graphics.DrawString("Data inchirerii: " + dataInceput, DefaultFont, Brushes.Black, 10, 240);
                            b.Graphics.DrawString("Data returnare: " + dataSfarsit, DefaultFont, Brushes.Black, 10, 280);
                            b.Graphics.DrawString("Total de plata: " + pretTotal + " EURO", DefaultFont, Brushes.Black, 10, 320);
                            b.Graphics.DrawString("======================================================", DefaultFont, Brushes.Black, 10, 360);
                        };
                        p.ShowDialog();
                    }
                }
                else MessageBox.Show("Modificare esuata. Mai incearca!");
            }
            else MessageBox.Show("Completati numele!");
        }

        void update()
        {
            label9.Text = "Total de plata: ";
            label10.Text = "Numar total zile: ";
            DateTime d1 = dateTimePicker1.Value;
            DateTime d2 = dateTimePicker2.Value;
            TimeSpan t = d2 - d1;
            dDays = t.TotalDays;
            days = Convert.ToInt32(dDays) + 1;

            pretTotal = days*masini.Pret;

            label10.Text = label10.Text + " " + days.ToString();
            label9.Text = label9.Text + days + "X" + masini.Pret + " = " + pretTotal;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            update();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
