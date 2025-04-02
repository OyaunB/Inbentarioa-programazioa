using System;
using System.Data;
using MySql.Data.MySqlClient;

public class DBEzabatutakoak
{
    private string konekzioString = "Server=localhost;Database=zure_datu_basea;Uid=root;Pwd=;";

    public DataTable LortuEzabatutakoGailuak()
    {
        DataTable dt = new DataTable();
        try
        {
            using (MySqlConnection konekzioa = new MySqlConnection(konekzioString))
            {
                konekzioa.Open();
                string query = "SELECT * FROM EzabatutakoGailuak";
                using (MySqlCommand cmd = new MySqlCommand(query, konekzioa))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errorea datuak lortzerakoan: " + ex.Message);
        }
        return dt;
    }
}
