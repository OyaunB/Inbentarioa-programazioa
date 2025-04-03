using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class ErabiltzaileakKudeatu : Form
    {
        private DBErabiltzaileak dbErabiltzaileak = new DBErabiltzaileak(); // 🔹 Instancia de DBErabiltzaileak

        public ErabiltzaileakKudeatu()
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
            CargarDatos(); //    Carga los datos al abrir el formulario

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
            // IDa automatikoki kalkulatu
            int id = dataGridViewErabiltzailea.Rows.Count + 1;

            // Erabiltzailearen izena eskatu
            string izena = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen izena:", "Gehitu Erabiltzailea", "");

            // Erabiltzailearen errola eskatu
            string errola = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen errola:", "Gehitu Erabiltzailea", "");

            // Erabiltzailearen kontua (username) eskatu
            string erabiltzailea = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen username-a:", "Gehitu Erabiltzailea", "");

            // Erabiltzailearen pasahitza eskatu
            string pasahitza = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen pasahitza:", "Gehitu Erabiltzailea", "");

            // Balidazioa: eremu guztiak bete behar dira
            if (string.IsNullOrWhiteSpace(izena) || string.IsNullOrWhiteSpace(errola) ||
                string.IsNullOrWhiteSpace(erabiltzailea) || string.IsNullOrWhiteSpace(pasahitza))
            {
                MessageBox.Show("Bete datu guztiak!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Datu basean gehitu
            DBErabiltzaileak db = new DBErabiltzaileak();
            bool gehituta = db.GehituErabiltzailea(id, izena, errola, erabiltzailea);

            if (gehituta)
            {
                // Erabiltzaileak.txt fitxategian gordetzeko ruta
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Erabiltzaileak.txt");

                try
                {
                    // Fitxategian erabiltzaile berria gehitu (formatua: Erabiltzailea;Pasahitza,Errola)
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine($"{erabiltzailea};{pasahitza};{errola}");
                    }

                    MessageBox.Show("Erabiltzailea gehitu da eta fitxategian gorde da!", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos(); // Datuak eguneratu
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea erabiltzailea fitxategian gordetzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            string erabiltzailea = row.Cells["ErabiltzaileIzena"].Value?.ToString(); // Asegurar que no es nulo

            bool eguneratuta = dbErabiltzaileak.EguneratuErabiltzailea(id, izena, errola, erabiltzailea);

            if (eguneratuta)
            {
                // También actualizar en el archivo, asegurando que se mantiene el password
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Erabiltzaileak.txt");
                string pasahitza = "";

                // Leer el archivo para obtener la contraseña actual del usuario antes de sobreescribirlo
                if (File.Exists(filePath))
                {
                    foreach (string line in File.ReadLines(filePath))
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length >= 2 && parts[0] == erabiltzailea)
                        {
                            pasahitza = parts[1]; // Guardamos la contraseña existente
                            break;
                        }
                    }
                }

                //bool fitxategiaEguneratuta = dbErabiltzaileak.GordeErabiltzaileaFitxategian(erabiltzailea, pasahitza, errola, false);
                // de prueba, para quitar el error
                // bool fitxategiaEguneratuta = dbErabiltzaileak.GordeErabiltzaileaFitxategian(erabiltzailea, pasahitza, errola, false);
                // copiloten erantzuna=
                bool fitxategiaEguneratuta = dbErabiltzaileak.GordeErabiltzaileaFitxategian(erabiltzailea, erabiltzailea, pasahitza, errola, false);



                if (fitxategiaEguneratuta)
                {
                    MessageBox.Show("Erabiltzailea eguneratu da datu-basean eta fitxategian!", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erabiltzailea eguneratu da datu-basean, baina errorea fitxategian!", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
