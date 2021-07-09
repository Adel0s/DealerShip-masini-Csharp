using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarDealership
{
    public partial class Despre : Form
    {
        public Despre()
        {
            InitializeComponent();
        }

        private void Despre_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(40, 199, 250);
            panel1.BackColor = Color.FromArgb(40, 199, 250);
            label1.ForeColor = Color.FromArgb(0, 38, 81);
            label2.ForeColor = Color.FromArgb(0, 38, 81);
            label3.ForeColor = Color.FromArgb(0, 38, 81);
            label4.ForeColor = Color.FromArgb(0, 38, 81);
            pictureBox2.BackgroundImage = Image.FromFile("user.png");
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
