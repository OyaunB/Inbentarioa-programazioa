// Gailuak.cs
using System;
using System.Data;
using MySql.Data.MySqlClient;

public class GailuakDAL
{
    private readonly string connectionString;

    public GailuakDAL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public DataTable ObtenerTodosGailuak()
    {
        DataTable table = new DataTable();

        string query = @"
        SELECT g.ID_Gailuak, g.Mota, 
               CASE 
                   WHEN o.ID_Gailuak IS NOT NULL THEN 'Ordenagailuak'
                   WHEN i.ID_Gailuak IS NOT NULL THEN 'Imprimagailuak'
                   WHEN b.ID_Gailuak IS NOT NULL THEN 'BesteGailuak'
               END AS GailuMota,
               o.Memoria_RAM, o.TxartelGrafikoa, o.USB_Portuak, 
               COALESCE(o.Kolorea, i.Kolorea) AS Kolorea,
               COALESCE(o.Egoera, i.Egoera, b.Egoera) AS Egoera,
               o.Marka
        FROM Gailuak g
        LEFT JOIN Ordenagailuak o ON g.ID_Gailuak = o.ID_Gailuak
        LEFT JOIN Imprimagailuak i ON g.ID_Gailuak = i.ID_Gailuak
        LEFT JOIN BesteGailuak b ON g.ID_Gailuak = b.ID_Gailuak
        WHERE o.ID_Gailuak IS NOT NULL OR i.ID_Gailuak IS NOT NULL OR b.ID_Gailuak IS NOT NULL";

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.Fill(table);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener los gailuak: " + ex.Message);
        }

        return table;
    }

    // Puedes añadir más métodos aquí para insertar, actualizar, eliminar, etc.
}