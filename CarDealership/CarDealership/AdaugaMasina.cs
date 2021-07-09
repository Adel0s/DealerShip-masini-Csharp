using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarDealership
{
    public partial class AdaugaMasina : Form
    {
        private string cale;
        SqlConnection con;
        public AdaugaMasina(SqlConnection con)
        {
            InitializeComponent();
            this.con = con;
        }

        private void AdaugaMasina_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(40, 199, 250);
            button1.BackColor = Color.FromArgb(40, 199, 250);
            button2.BackColor = Color.FromArgb(40, 199, 250);
            button3.BackColor = Color.FromArgb(40, 199, 250);
            pictureBox1.BackgroundImage = Image.FromFile("car.png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            label1.Visible = false;
            pictureBox3.BackgroundImage = Image.FromFile("photo.png");
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox3.Hide();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Cauta";
            ofd.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                label1.Visible = true;
                label1.Text = ofd.FileName;
                pictureBox2.BackgroundImage = Image.FromFile(ofd.FileName);
                pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked && textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                cale = label1.Text;
                cale = cale.Substring(cale.LastIndexOf('\\') + 1);
                SqlCommand cmd = new SqlCommand("INSERT INTO Masini(Marca,Model,An,Pret,CaleFisier,Descriere,Status) VALUES(@marca,@model,@an,@pret,@cale,@descriere,@status)", con);

                if (radioButton1.Checked) cmd.Parameters.AddWithValue("status", 0); ///status 0 = de vanzare
                if (radioButton2.Checked) cmd.Parameters.AddWithValue("status", 1); ///status 1 = de inchiriat
                cmd.Parameters.AddWithValue("marca", textBox1.Text);
                cmd.Parameters.AddWithValue("model", textBox2.Text);
                cmd.Parameters.AddWithValue("an", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("pret", textBox4.Text);
                cmd.Parameters.AddWithValue("descriere", textBox5.Text);
                cmd.Parameters.AddWithValue("cale", cale);

                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Ai adaugat cu succes masina!");
                    this.Close();
                }
                else MessageBox.Show("Adugare nereusita!");
            }
            else MessageBox.Show("Toate campurile sunt obligatorii!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
