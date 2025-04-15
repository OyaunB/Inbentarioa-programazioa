//DBKonexioa.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Inbentarioa
{
    static class DBKonexioa
    {
        private static MySqlConnection nirekonexioa;

        static DBKonexioa()
        {
            string zerbitzaria = "127.0.0.1";
            string DB = "inbentarioa";
            string erabiltzailea = "root";
            string pasahitza = "root";
            string portua = "3306";

            string KONEXIOA = $"Database={DB}; Data Source={zerbitzaria}; Port={portua}; User Id={erabiltzailea}; Password={pasahitza};";
            nirekonexioa = new MySqlConnection(KONEXIOA);
        }
        public static string GetConnectionString()
        {
            return "server=localhost;database=inbentarioa;uid=root;pwd=root;";
        }

        public static MySqlConnection Konektatu()
        {
            try
            {
                if (nirekonexioa == null)
                {
                    string zerbitzaria = "127.0.0.1";
                    string DB = "inbentarioa";
                    string erabiltzailea = "root";
                    string pasahitza = "root";
                    string portua = "3306";

                    string KONEXIOA = $"Database={DB}; Data Source={zerbitzaria}; Port={portua}; User Id={erabiltzailea}; Password={pasahitza};";
                    nirekonexioa = new MySqlConnection(KONEXIOA);
                }

                Console.WriteLine("Konexio egoera: " + nirekonexioa.State); // Egiaztatu egoera

                if (nirekonexioa.State == System.Data.ConnectionState.Closed)
                {
                    nirekonexioa.Open();
                    Console.WriteLine("✅ Konexioa ireki da");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("⚠️ Konexio errorea: " + e.Message);
            }

            return nirekonexioa;
        }


        public static void ItxiKonexioa()
        {
            if (nirekonexioa != null && nirekonexioa.State == System.Data.ConnectionState.Open)
            {
                nirekonexioa.Close();  // 🔹 Ahora sí cierra la conexión correctamente
                Console.WriteLine("Konexioa itxita");
            }
        }

    }
}