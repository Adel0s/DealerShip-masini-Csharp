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
    public partial class Meniu : Form
    {
        SqlConnection con;
        public Meniu(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Meniu_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(40, 199, 250);
            panel2.BackColor = Color.FromArgb(40, 199, 250);
            pictureBox1.BackgroundImage = Image.FromFile("car.png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.BackgroundImage = Image.FromFile("user.png");
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Image = Image.FromFile("rent128.png");
            button2.Image = Image.FromFile("sell.png");
            button3.Image = Image.FromFile("addrent.png");
            button4.Image = Image.FromFile("close.png");
            button5.Image = Image.FromFile("logout.png");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdaugaMasina am = new AdaugaMasina(con);
            this.Hide();
            am.Show();
            am.FormClosed += (a, b) =>
            {
                this.Show();
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inchirieri inchirieri = new Inchirieri(con);
            this.Hide();
            inchirieri.Show();
            inchirieri.FormClosed += (a, b) =>
            {
                this.Show();
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cumpara c = new Cumpara(con);
            this.Hide();
            c.Show();
            c.FormClosed += (a, b) =>
            {
                this.Show();
            };
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Despre d = new Despre();
            this.Hide();
            d.Show();
            d.FormClosed += (a, b) =>
            {
                this.Show();
            };
        }
    }
}
