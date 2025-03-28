﻿using System;
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

        public static MySqlConnection Konektatu()
        {
            try
            {
                if (nirekonexioa.State == System.Data.ConnectionState.Closed)
                {
                    nirekonexioa.Open();
                    Console.WriteLine("Konexioa eginda");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ondorengo arazoa egon da: " + e.Message);
            }
            return nirekonexioa;
        }

        public static void ItxiKonexioa()
        {
            if (nirekonexioa.State == System.Data.ConnectionState.Open)
            {
                nirekonexioa.Close();
                Console.WriteLine("Konexioa itxita");
            }
        }
    }
}