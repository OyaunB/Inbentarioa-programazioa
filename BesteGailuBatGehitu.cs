using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // Rellenar el ComboBox con las opciones de estado
            comboBoxEgoera.Items.AddRange(new string[] { "Ongi", "Apurtuta", "Kompontzen" });
            comboBoxEgoera.SelectedIndex = 0; // Seleccionar "Ongi" por defecto
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

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void BtAukeraAutatu_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bidaliBotoia_Click(object sender, EventArgs e)
        {
            string marka = tbMarka.Text.Trim();
            string modeloa = tbModeloa.Text.Trim();
            string egoera = comboBoxEgoera.SelectedItem?.ToString() ?? "Ongi";  // Obtiene el valor de ComboBox, o 'Ongi' si no se selecciona ninguno

            if (string.IsNullOrEmpty(marka) || string.IsNullOrEmpty(modeloa))
            {
                MessageBox.Show("Marka eta Modeloa eremuak bete behar dira.");
                return;
            }

            try
            {
                string connectionString = "server=localhost;database=inbentarioa;uid=root;pwd=root;";
                GailuakDAL gailuakDAL = new GailuakDAL(connectionString);

                bool result = gailuakDAL.GehituBesteGailua(marka, modeloa, egoera); // Ahora pasas egoera

                if (result)
                {
                    MessageBox.Show("Gailua ondo gehitu da!");
                    tbMarka.Text = "";
                    tbModeloa.Text = "";
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
