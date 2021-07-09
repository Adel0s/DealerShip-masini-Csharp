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
    public partial class Inchirieri : Form
    {
        SqlConnection con;
        public Inchirieri(SqlConnection con)
        {
            InitializeComponent();
            this.con = con;

            dataGridView1.CellClick += (a, b) =>
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].FormattedValue.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].FormattedValue.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].FormattedValue.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].FormattedValue.ToString();
            };
        }

        void populate()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("SELECT * FROM Masini WHERE Status=@status", con);
            cmd.Parameters.AddWithValue("status", 1);
            var red = cmd.ExecuteReader();
            while (red.Read())
            {
                dataGridView1.Rows.Add(red[0].ToString(), red[1].ToString(), red[2].ToString(), red[3].ToString(), red[4].ToString(), "Detalii");
            }
        }

        private void Inchirieri_Load(object sender, EventArgs e)
        {
            populate();
            panel1.BackColor = Color.FromArgb(40, 199, 250);
            button4.BackColor = Color.FromArgb(40, 199, 250);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].FormattedValue);
                Detalii detalii = new Detalii(con, new Masini(con, id));

                this.Hide();
                detalii.Show();

                detalii.FormClosed += (a, b) =>
                {
                    this.Show();
                };
            }
        }
    }
}
