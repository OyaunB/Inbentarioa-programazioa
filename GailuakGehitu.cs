using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class GailuakGehitu : Form
    {
        private readonly string connectionString = "server=127.0.0.1;database=inbentarioa;uid=root;pwd=root;";
        private readonly GailuakDAL gailuakDAL;

        public GailuakGehitu()
        {
            InitializeComponent();
            gailuakDAL = new GailuakDAL(connectionString);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Definir los colores del degradado usando códigos hexadecimales
            Color colorInicio = ColorTranslator.FromHtml("#5de0e6"); // Azul claro
            Color colorFin = ColorTranslator.FromHtml("#004aad");    // Azul oscuro

            // Crear un pincel con degradado lineal
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, // Área donde se aplicará el degradado
                colorInicio,         // Color inicial
                colorFin,            // Color final
                LinearGradientMode.Horizontal)) // Dirección del degradado (horizontal)
            {
                // Rellenar el fondo del formulario con el degradado
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void CargarDatos()
        {
            try
            {
                DataTable table = gailuakDAL.ObtenerTodosGailuak();
                dataGridViewGailuakGehitu.DataSource = table;
                dataGridViewGailuakGehitu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GailuakGehitu_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Manejo del evento click del label si es necesario
        }

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            Aukerak f2 = new Aukerak();
            f2.ShowDialog();
        }

        private void BtGehitu_Click(object sender, EventArgs e)
        {
            this.Hide();
            aukeraAutatzeko f7 = new aukeraAutatzeko();
            f7.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Manejo del evento click de la celda si es necesario
        }

        private void btAldatu_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataGridViewRow row in dataGridViewGailuakGehitu.Rows)
                    {
                        if (row.Cells["ID"].Value != null) // Saihestu lerro hutsak
                        {
                            int id = Convert.ToInt32(row.Cells["ID"].Value);
                            int idMintegia = Convert.ToInt32(row.Cells["ID_Mintegia"].Value);
                            string marka = row.Cells["Marka"].Value.ToString();
                            string izena = row.Cells["Izena"].Value.ToString();
                            DateTime erosketadata = Convert.ToDateTime(row.Cells["ErosketaData"].Value);
                            bool egoera = Convert.ToBoolean(row.Cells["Egoera"].Value);

                            string query = "UPDATE gailuak SET ID_Mintegia = @ID_Mintegia, Marka = @Marka, Izena = @Izena, ErosketaData = @ErosketaData, Egoera = @Egoera WHERE ID = @ID";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@ID", id);
                                cmd.Parameters.AddWithValue("@ID_Mintegia", idMintegia);
                                cmd.Parameters.AddWithValue("@Marka", marka);
                                cmd.Parameters.AddWithValue("@Izena", izena);
                                cmd.Parameters.AddWithValue("@ErosketaData", erosketadata);
                                cmd.Parameters.AddWithValue("@Egoera", egoera);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("Cambios guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos(); // Recargar los datos después de la actualización

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
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
    SELECT 
        g.ID_Gailuak,
        g.Gailu_Mota,
        g.ID_Mintegia,
        g.Marka,
        g.Izena,
        g.Erosketa_data,
        g.Egoera AS Gailu_Egoera,  -- Alias para evitar confusión
        o.Memoria_RAM,
        o.TxartelGrafikoa,
        o.USB_Portuak,
        o.Kolorea AS Ordenagailu_Kolorea,
        i.Kolorea AS Imprimagailu_Kolorea,
        b.Egoera AS BesteGailu_Egoera
    FROM Gailuak g
    LEFT JOIN Ordenagailuak o ON g.ID_Gailuak = o.ID_Gailuak AND g.Gailu_Mota = 'Ordenagailuak'
    LEFT JOIN Imprimagailuak i ON g.ID_Gailuak = i.ID_Gailuak AND g.Gailu_Mota = 'Inprimagailuak'
    LEFT JOIN BesteGailuak b ON g.ID_Gailuak = b.ID_Gailuak AND g.Gailu_Mota = 'BesteGailuak'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    adapter.Fill(table);

                    // Agregar columna combinada para características específicas
                    table.Columns.Add("Ezaugarriak", typeof(string));
                    foreach (DataRow row in table.Rows)
                    {
                        switch (row["Gailu_Mota"].ToString())
                        {
                            case "Ordenagailuak":
                                row["Ezaugarriak"] = $"RAM: {row["Memoria_RAM"]}GB, GPU: {row["TxartelGrafikoa"]}, USB: {row["USB_Portuak"]}, Kolorea: {row["Ordenagailu_Kolorea"]}";
                                break;
                            case "Inprimagailuak":
                                row["Ezaugarriak"] = $"Kolorea: {row["Imprimagailu_Kolorea"]}";
                                break;
                            case "BesteGailuak":
                                // Verificación segura del valor booleano
                                bool egoera = false;
                                if (row["BesteGailu_Egoera"] != DBNull.Value)
                                {
                                    egoera = Convert.ToBoolean(row["BesteGailu_Egoera"]);
                                }
                                row["Ezaugarriak"] = $"Egoera: {(egoera ? "Ongi" : "Apurtuta")}";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los gailuak: " + ex.Message);
            }

            return table;
        }
    }
}