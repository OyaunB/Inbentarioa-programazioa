using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class Form6 : Form
    {
        private DBErabiltzaileak dbErabiltzaileak = new DBErabiltzaileak(); // 🔹 Instancia de DBErabiltzaileak

        public Form6()
        {
            InitializeComponent();
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

        private void Form6_Load(object sender, EventArgs e)
        {
            CargarDatos(); // 🔹 Carga los datos al abrir el formulario
        }

        private void CargarDatos()
        {
            dataGridViewErabiltzailea.DataSource = dbErabiltzaileak.LortuErabiltzaileak();
        }

        // 🔹 Al seleccionar una fila, almacenamos los datos en variables
        private void DataGridViewErabiltzailea_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewErabiltzailea.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewErabiltzailea.SelectedRows[0];

                int id = Convert.ToInt32(row.Cells["ID_Erabiltzaileak"].Value);
                string izena = row.Cells["Izena"].Value.ToString();
                string errola = row.Cells["Errola"].Value.ToString();

                BtGehitu.Tag = new Tuple<int, string, string>(id, izena, errola); // Guardar datos en el botón
            }
        }

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            Aukerak f2 = new Aukerak();
            f2.ShowDialog();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatos(); // 🔹 Botón para actualizar los datos sin cerrar la ventana 
        }

        private void BtGehitu_Click(object sender, EventArgs e)
        {
            // Ilara bat hautatuta dagoen egiaztatu
            if (dataGridViewErabiltzailea.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hautatu edo gehitu erabiltzaile bat!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Aukeratutako ilarako datuak lortu
            DataGridViewRow row = dataGridViewErabiltzailea.SelectedRows[0];

            if (row.Cells["ID_Erabiltzaileak"].Value == null ||
                row.Cells["Izena"].Value == null ||
                row.Cells["Errola"].Value == null)
            {
                MessageBox.Show("Bete datu guztiak!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Sartutako datuak aldagaietan gorde
            int id;
            if (!int.TryParse(row.Cells["ID_Erabiltzaileak"].Value.ToString(), out id))
            {
                MessageBox.Show("ID zenbakia izan behar da!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string izena = row.Cells["Izena"].Value.ToString();
            string errola = row.Cells["Errola"].Value.ToString();

            // Datu basean datuak gorde
            DBErabiltzaileak db = new DBErabiltzaileak();
            bool gehituta = db.GehituErabiltzailea(id, izena, errola);

            if (gehituta)
            {
                MessageBox.Show("Erabiltzailea gehitu da!", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos(); //Datuak kargatu sartutako erabiltaile berria ikusteko
            }
            else
            {
                MessageBox.Show("Errorea erabiltzailea gehitzean!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btAldatu_Click(object sender, EventArgs e)
        {
            if (dataGridViewErabiltzailea.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hautatu erabiltzaile bat eguneratzeko!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridViewErabiltzailea.SelectedRows[0];

            int id = Convert.ToInt32(row.Cells["ID_Erabiltzaileak"].Value);
            string izena = row.Cells["Izena"].Value.ToString();
            string errola = row.Cells["Errola"].Value.ToString();

            bool eguneratuta = dbErabiltzaileak.EguneratuErabiltzailea(id, izena, errola);

            if (eguneratuta)
            {
                MessageBox.Show("Erabiltzailea eguneratu da!", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
            }
            else
            {
                MessageBox.Show("Errorea erabiltzailea eguneratzerakoan!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btEzabatu_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewErabiltzailea.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hautatu erabiltzaile bat ezabatzeko!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridViewErabiltzailea.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["ID_Erabiltzaileak"].Value);

            DialogResult result = MessageBox.Show("Ziur zaude erabiltzaile hau ezabatu nahi duzula?", "Kontuz", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool ezabatuta = dbErabiltzaileak.EzabatuErabiltzailea(id);

                if (ezabatuta)
                {
                    MessageBox.Show("Erabiltzailea ezabatu da!", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Errorea erabiltzailea ezabatzerakoan!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
