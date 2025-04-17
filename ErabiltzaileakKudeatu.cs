//ErabiltzaileakKudeatu.cs
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
            // Configurar el DataGridView para solo lectura y selección de filas completas
            dataGridViewErabiltzailea.ReadOnly = true;
            dataGridViewErabiltzailea.AllowUserToAddRows = false;
            dataGridViewErabiltzailea.AllowUserToDeleteRows = false;
            dataGridViewErabiltzailea.AllowUserToResizeRows = false;
            dataGridViewErabiltzailea.MultiSelect = false; // Solo permitir selección de una fila a la vez
            dataGridViewErabiltzailea.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewErabiltzailea.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Ocultar el indicador de edición (el lápiz)
            dataGridViewErabiltzailea.RowHeadersVisible = false;
            //Cambiar colores
            dataGridViewErabiltzailea.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridViewErabiltzailea.DefaultCellStyle.SelectionForeColor = Color.Black;

            CargarDatos();
        }

        private void CargarDatos()
        {
            dataGridViewErabiltzailea.DataSource = dbErabiltzaileak.LortuErabiltzaileak();
            //Erabiltzailearen pasahitza ezkutatu=
            if (dataGridViewErabiltzailea.Columns.Contains("ErabiltzailePasahitza"))
            {
                dataGridViewErabiltzailea.Columns["ErabiltzailePasahitza"].Visible = false;
            }
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
            // Obtener el siguiente ID disponible de la base de datos
            int id = dbErabiltzaileak.LortuHurrengoId();

            if (id == -1)
            {
                MessageBox.Show("Ezin da hurrengo IDa lortu!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Resto del código permanece igual...
            string izena = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen izena:", "Gehitu Erabiltzailea", "");
            string errola = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen errola:", "Gehitu Erabiltzailea", "");
            string erabiltzailea = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen username-a:", "Gehitu Erabiltzailea", "");
            string pasahitza = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen pasahitza:", "Gehitu Erabiltzailea", "");

            if (string.IsNullOrWhiteSpace(izena) || string.IsNullOrWhiteSpace(errola) ||
                string.IsNullOrWhiteSpace(erabiltzailea) || string.IsNullOrWhiteSpace(pasahitza))
            {
                MessageBox.Show("Bete datu guztiak!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool gehituta = dbErabiltzaileak.GehituErabiltzailea(id, izena, errola, erabiltzailea, pasahitza);

            if (gehituta)
            {
                bool fitxategianGordeta = dbErabiltzaileak.GordeErabiltzaileaFitxategian(erabiltzailea, erabiltzailea, pasahitza, errola, true);

                if (fitxategianGordeta)
                {
                    MessageBox.Show("Erabiltzailea gehitu da eta fitxategian gorde da!", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Erabiltzailea datu-basean gehitu da, baina errorea fitxategian gordetzean!", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Hautatu erabiltzaile bat eguneratzeko!", "Errorea",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridViewErabiltzailea.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["ID_Erabiltzaileak"].Value);
            string izenaActual = row.Cells["Izena"].Value.ToString();
            string errolaActual = row.Cells["Errola"].Value.ToString();
            string erabiltzaileaIzanaActual = row.Cells["ErabiltzaileIzena"].Value?.ToString();

            // Pedir nuevos valores al usuario
            string izenaBerria = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen izen berria:",
                                                                          "Eguneratu Erabiltzailea",
                                                                          izenaActual);

            string errolaBerria = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen errol berria:",
                                                                            "Eguneratu Erabiltzailea",
                                                                            errolaActual);

            string erabiltzaileaIzenaBerria = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen username berria:",
                                                                                      "Eguneratu Erabiltzailea",
                                                                                      erabiltzaileaIzanaActual);

            string pasahitzBerria = Microsoft.VisualBasic.Interaction.InputBox("Sartu erabiltzailearen pasahitz berria (utzi hutsik ez aldatzeko):",
                                                                             "Eguneratu Erabiltzailea",
                                                                             "");

            // Validar datos obligatorios
            if (string.IsNullOrWhiteSpace(izenaBerria) ||
                string.IsNullOrWhiteSpace(errolaBerria) ||
                string.IsNullOrWhiteSpace(erabiltzaileaIzenaBerria))
            {
                MessageBox.Show("Izena, errola eta erabiltzaile izena bete behar dira!",
                                "Errorea",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Actualizar en la base de datos
            bool eguneratuta = dbErabiltzaileak.EguneratuErabiltzailea(id, izenaBerria, errolaBerria, erabiltzaileaIzenaBerria);

            if (eguneratuta)
            {
                // Si se proporcionó nueva contraseña o cambió el nombre de usuario, actualizar el archivo
                if (!string.IsNullOrWhiteSpace(pasahitzBerria) ||
                    erabiltzaileaIzanaActual != erabiltzaileaIzenaBerria)
                {
                    bool fitxategiaEguneratuta = dbErabiltzaileak.GordeErabiltzaileaFitxategian(
                        erabiltzaileaIzanaActual,
                        erabiltzaileaIzenaBerria,
                        string.IsNullOrWhiteSpace(pasahitzBerria) ? "" : pasahitzBerria,
                        errolaBerria,
                        true);

                    if (fitxategiaEguneratuta)
                    {
                        MessageBox.Show("Erabiltzailea eguneratu da datu-basean eta fitxategian!",
                                       "Ongi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erabiltzailea datu-basean eguneratu da, baina errorea fitxategian!",
                                      "Abisua",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Erabiltzailea eguneratu da datu-basean!",
                                   "Ongi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarDatos(); // Actualizar la vista
            }
            else
            {
                MessageBox.Show("Errorea erabiltzailea eguneratzerakoan!",
                               "Errorea",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string erabiltzailea = row.Cells["ErabiltzaileIzena"].Value?.ToString();

            DialogResult result = MessageBox.Show("Ziur zaude erabiltzaile hau ezabatu nahi duzula?", "Kontuz", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool ezabatuta = dbErabiltzaileak.EzabatuErabiltzailea(id);

                if (ezabatuta)
                {
                    // También borrar del archivo
                    if (!string.IsNullOrEmpty(erabiltzailea))
                    {
                        bool fitxategitikEzabatuta = dbErabiltzaileak.GordeErabiltzaileaFitxategian(erabiltzailea, erabiltzailea, "", "", false);

                        if (!fitxategitikEzabatuta)
                        {
                            MessageBox.Show("Erabiltzailea datu-basean ezabatu da, baina errorea fitxategitik ezabatzean!", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

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
