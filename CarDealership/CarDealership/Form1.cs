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
    public partial class Form1 : Form
    {
        SqlConnection con;
        public Form1()
        {
            con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DealershipDB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True; MultipleActiveResultSets = True;");
            InitializeComponent();
            con.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(40, 199, 250);
            button1.BackColor = Color.FromArgb(40, 199, 250);
            button2.ForeColor = Color.FromArgb(40, 199, 250);
            label4.ForeColor = Color.FromArgb(40, 199, 250);
            label4.BackColor = Color.WhiteSmoke;
            label1.ForeColor = Color.FromArgb(0, 38, 81);
            label2.ForeColor = Color.FromArgb(0, 38, 81);
            label6.ForeColor = Color.FromArgb(0, 38, 81);
            pictureBox1.BackgroundImage = Image.FromFile("car.png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            //SqlCommand cmd = new SqlCommand("TRUNCATE TABLE Masini",con);
            //cmd.ExecuteNonQuery();
            //SqlCommand cmd1 = new SqlCommand("TRUNCATE TABLE Utilizatori", con);
            //cmd1.ExecuteNonQuery();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.BackColor = Color.FromArgb(40, 199, 250);
            label4.ForeColor = Color.White;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.FromArgb(40, 199, 250);
            label4.BackColor = Color.WhiteSmoke;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Register r = new Register(con);
            this.Hide();
            r.Show();
            r.FormClosed += (a, b) =>
            {
                this.Show();
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("SELECT * FROM Utilizatori WHERE Email = @email AND Parola = @parola", con);
            cmd.Parameters.AddWithValue("email", textBox1.Text);
            cmd.Parameters.AddWithValue("parola", textBox2.Text);

            var red = cmd.ExecuteReader();
            if (red.Read())
            {
                //int id = Convert.ToInt32(red[0]);
                Meniu meniu = new Meniu(con);
                this.Hide();
                meniu.Show();
                meniu.FormClosed += (a, b) =>
                {
                    this.Show();
                    textBox2.Text = "";
                };
            }
            else MessageBox.Show("Date de logare incorecte! Mai incearca.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
