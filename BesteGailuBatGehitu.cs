using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class BesteGailuBatGehitu : Form
    {
        public BesteGailuBatGehitu()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            CargarComboBoxEgoera();

            // Llamar al método para cargar los mintegiak en el ComboBox
            CargarMintegiakCombo();
            // Centrar el formulario en la pantalla
            this.CenterToScreen();
        }
        private void CargarMintegiakCombo()
        {
            GailuakDAL dal = new GailuakDAL();
            cbMintegiaB.DisplayMember = "Izena";
            cbMintegiaB.ValueMember = "ID_Mintegia";
            cbMintegiaB.DataSource = dal.ObtenerMintegiak();
        }
        private void CargarComboBoxEgoera()
        {
            if (comboBoxEgoera.Items.Count == 0)
            {
                comboBoxEgoera.Items.AddRange(new string[] { "Ongi", "Apurtuta", "Kompontzen" });
                comboBoxEgoera.SelectedIndex = 0;
            }
        }


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
        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            aukeraAutatzeko form8 = new aukeraAutatzeko();
            form8.ShowDialog();
        }

        private void bidaliBotoia_Click(object sender, EventArgs e)
        {
            string marka = tbMarka.Text.Trim();
            string modeloa = tbModeloa.Text.Trim();
            string egoera = comboBoxEgoera.SelectedItem?.ToString() ?? "Ongi";
            int idMintegia;

            if (cbMintegiaB.SelectedValue == null || !int.TryParse(cbMintegiaB.SelectedValue.ToString(), out idMintegia))
            {
                MessageBox.Show("Mesedez, hautatu mintegi bat ComboBoxean.");
                return;
            }

            if (string.IsNullOrEmpty(marka) || string.IsNullOrEmpty(modeloa))
            {
                MessageBox.Show("Marka eta Modeloa eremuak bete behar dira.");
                return;
            }

            try
            {
                string connectionString = DBKonexioa.GetConnectionString();
                GailuakDAL gailuakDAL = new GailuakDAL(connectionString);

                bool result = gailuakDAL.GehituBesteGailua(idMintegia, marka, modeloa, egoera);

                if (result)
                {
                    MessageBox.Show("Gailua ondo gehitu da!");
                    tbMarka.Text = "";
                    tbModeloa.Text = "";
                    cbMintegiaB.SelectedIndex = -1; // Limpiar ComboBox
                }
                else
                {
                    MessageBox.Show("Errorea gertatu da gailua gehitzean.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea: " + ex.Message);
            }
        }
    }
}
