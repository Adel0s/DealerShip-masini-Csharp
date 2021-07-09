using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CarDealership
{
    public class Masini
    {
        public int IdMasina{ get; set; }
        public int An { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        public int Pret { get; set; }
        public string CaleFisier { get; set; }
        public string Descriere { get; set; }
        public string DataInchiriere { get; set; }
        public string DataReturnare { get; set; }

        public Masini(SqlConnection con, int id)
        {
            this.IdMasina = id;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Masini WHERE IdMasina = @id",con);
            cmd.Parameters.AddWithValue("id",id);
            var red = cmd.ExecuteReader();

            while (red.Read())
            {
                this.Marca = red[1].ToString();
                this.Model = red[2].ToString();
                this.An = Convert.ToInt32(red[3]);
                this.Pret = Convert.ToInt32(red[4]);
                this.CaleFisier = red[5].ToString();
                this.Descriere = red[6].ToString();
                this.DataInchiriere = red[8].ToString();
                this.DataReturnare = red[9].ToString();
            }

        }
    }
}
