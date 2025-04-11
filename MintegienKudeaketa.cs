using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class MintegienKudeaketa : Form
    {
        private DBMintegiak dbMintegiak = new DBMintegiak();

        public MintegienKudeaketa()
        {
            InitializeComponent();
            // Configurar DataGridView al iniciar
            DataGridViewMintegiak.AutoGenerateColumns = true;
            DataGridViewMintegiak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            // Configuración inicial del DataGridView
            DataGridViewMintegiak.ReadOnly = true;
            DataGridViewMintegiak.AllowUserToAddRows = false;
            DataGridViewMintegiak.AllowUserToDeleteRows = false;
            DataGridViewMintegiak.MultiSelect = false;
            DataGridViewMintegiak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewMintegiak.RowHeadersVisible = false;
            DataGridViewMintegiak.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            DataGridViewMintegiak.DefaultCellStyle.SelectionForeColor = Color.Black;

            CargarMintegiak();

            // Control de permisos según el rol
            string rol = Errola.ErabiltzaileRola?.ToLower() ?? "";

            btAtzera.Enabled = true; // Siempre permitido
            BtGehitu.Enabled = (rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt"); // Irakaslea NO puede añadir
            btAldatu.Enabled = (rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt"); // Irakaslea NO puede modificar
            btEzabatu.Enabled = (rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt"); // Irakaslea NO puede eliminar

            // Opcional: Deshabilitar la selección si el rol es "irakaslea"
            DataGridViewMintegiak.Enabled = !(rol == "irakaslea");
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



        private void CargarMintegiak()
        {
            try
            {
                DataGridViewMintegiak.DataSource = dbMintegiak.LortuMintegiak();


                // Configurar columnas (si es necesario)
                if (DataGridViewMintegiak.Columns.Count > 0)
                {
                    DataGridViewMintegiak.Columns["ID_Mintegia"].HeaderText = "ID";
                    DataGridViewMintegiak.Columns["Izena"].HeaderText = "Izena";
                    DataGridViewMintegiak.Columns["Kokapena"].HeaderText = "Kokapena";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea. zerbait ez duzu ondo egin");
            }
        }

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            Aukerak f2 = new Aukerak();
            f2.ShowDialog();
        }
        private void btAldatu_Click(object sender, EventArgs e)
        {
            if (DataGridViewMintegiak.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hautatu mintegi bat eguneratzeko!", "Errorea");
                return;
            }

            DataGridViewRow row = DataGridViewMintegiak.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["ID_Mintegia"].Value);
            string izenaActual = row.Cells["Izena"].Value.ToString();
            string kokapenaActual = row.Cells["Kokapena"].Value.ToString();

            // Pedir nuevos valores
            string izenaBerria = Microsoft.VisualBasic.Interaction.InputBox("Sartu izen berria:",
                                                                          "Aldatu Mintegia",
                                                                          izenaActual);
            string kokapenaBerria = Microsoft.VisualBasic.Interaction.InputBox("Sartu kokapen berria:",
                                                                             "Aldatu Mintegia",
                                                                             kokapenaActual);

            if (string.IsNullOrWhiteSpace(izenaBerria) || string.IsNullOrWhiteSpace(kokapenaBerria))
            {
                MessageBox.Show("Izena eta kokapena bete behar dira!", "Errorea");
                return;
            }

            if (dbMintegiak.EguneratuMintegia(id, izenaBerria, kokapenaBerria))
            {
                MessageBox.Show("Mintegia eguneratu da!", "Ongi");
                CargarMintegiak();
            }
            else
            {
                MessageBox.Show("Errorea mintegia eguneratzerakoan!", "Errorea");
            }
        }
        //Gehitu botoiaren funtzioa
        private void BtGehitu_Click(object sender, EventArgs e)
        {
            string izena = Microsoft.VisualBasic.Interaction.InputBox("Sartu mintegiaren izena:", "Gehitu Mintegia", "");
            if (string.IsNullOrWhiteSpace(izena))
            {
                MessageBox.Show("Mintegiaren izena beharrezkoa da!", "Errorea");
                return;
            }

            string kokapena = Microsoft.VisualBasic.Interaction.InputBox("Sartu kokapena:", "Gehitu Mintegia", "");
            if (string.IsNullOrWhiteSpace(kokapena))
            {
                MessageBox.Show("Kokapena beharrezkoa da!", "Errorea");
                return;
            }

            if (dbMintegiak.GehituMintegia(izena, kokapena))
            {
                MessageBox.Show("Mintegia gehitu da!", "Ongi");
                CargarMintegiak();
            }
            else
            {
                MessageBox.Show("Errorea mintegia gehitzean!", "Errorea");
            }
        }
        private void btEzabatu_Click(object sender, EventArgs e)
        {
            if (DataGridViewMintegiak.SelectedRows.Count == 0)
            {
                MessageBox.Show("Mesedez hautatu mintegi bat ezabatzeko!");
                return;
            }

            DataGridViewRow row = DataGridViewMintegiak.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["ID_Mintegia"].Value);
            string izena = row.Cells["Izena"].Value.ToString();

            DialogResult result = MessageBox.Show($"Ziur zaude '{izena}' mintegia ezabatu nahi duzula?",
                                                "Kontuz",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (dbMintegiak.EzabatuMintegia(id))
                {
                    MessageBox.Show("Mintegia ezabatu da!", "Ongi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarMintegiak();
                }
                else
                {
                    MessageBox.Show("Errorea mintegia ezabatzerakoan!", "Errorea");
                }
            }
        }
    }
}