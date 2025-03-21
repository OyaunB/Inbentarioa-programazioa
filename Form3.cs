using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Importar librería de MySQL

namespace Inbentarioa
{
    public partial class Form3 : Form
    {
        // Cadena de conexión a la base de datos
        private string konexioa = "server=localhost;database=Inbentarioa;user=root;password=;";

        public Form3()
        {
            InitializeComponent();
        }

        // Método para conectar a MySQL
        private MySqlConnection Konektatu()
        {
            MySqlConnection kon = new MySqlConnection(konexioa);
            kon.Open();
            return kon;
        }

        // Método para cargar datos en el DataGridView
        private void CargarDatos()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(konexioa))
                {
                    connection.Open();
                    string query = "SELECT * FROM ID_Gailuak"; // Consulta SQL correcta

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Asignar los datos al DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        // PANTALLA KOLOREZTATZEKO
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Color colorInicio = ColorTranslator.FromHtml("#5de0e6");
            Color colorFin = ColorTranslator.FromHtml("#004aad");

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, colorInicio, colorFin, LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            CargarDatos(); // Cargar datos al iniciar el formulario
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarDatos(); // Cargar datos al hacer clic en el botón
            this.Hide();
            Form7 f7 = new Form7();
            f7.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
