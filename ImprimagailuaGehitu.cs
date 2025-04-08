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
    public partial class tbIDMintegia : Form
    {
        public tbIDMintegia()
        {
            InitializeComponent();
        }

        private void tbIDMintegia_Load(object sender, EventArgs e)
        {
            // Kargatu ComboBox-a hasierako egoerarekin
            CargarComboBoxEgoera();
        }
        private void CargarComboBoxEgoera()
        {
            // ComboBox-a hutsik dagoen egiaztatu eta gero kargatu
            if (comboBoxEgoeraImprimagailua.Items.Count == 0)
            {
                comboBoxEgoeraImprimagailua.Items.AddRange(new string[] { "Ongi", "Apurtuta", "Kompontzen" });
                comboBoxEgoeraImprimagailua.SelectedIndex = 0; // "Ongi" hautatuko da lehen aldiz
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

        private void lbmintkodea_Click(object sender, EventArgs e)
        {

        }

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            aukeraAutatzeko form8 = new aukeraAutatzeko();
            form8.ShowDialog();
        }



        private void bidaliBotoia_Click(object sender, EventArgs e)
        {
            // 1. Mintegi ID-a beharrezkoa den kasuetan, beharrezkoa izango litzateke ID bat lortzea
            if (!int.TryParse(tbIDMintegiaImp.Text.Trim(), out int mintegiId))
            {
                MessageBox.Show("Mesedez, sartu balio numeriko bat Mintegi Kodea eremuan.");
                return;
            }

            // 2. ComboBox-eko egoera lortu (Ongi, Apurtuta, Kompontzen)
            string egoera = comboBoxEgoeraImprimagailua.SelectedItem?.ToString();

            // 3. Datuak balidatu
            string marka = tbMarkaImp.Text.Trim();
            string modeloa = btModeloaImprimagailua.Text.Trim();

            if (string.IsNullOrEmpty(marka) || string.IsNullOrEmpty(modeloa) || string.IsNullOrEmpty(egoera))
            {
                MessageBox.Show("Marka, Modeloa eta Egoera eremuak bete behar dira.");
                return;
            }

            try
            {
                GailuakDAL gailuakDAL = new GailuakDAL();
                bool result = gailuakDAL.GehituImprimagailua(mintegiId, marka, modeloa, egoera);

                if (result)
                {
                    MessageBox.Show("Imprimagailua ondo gehitu da!");
                    tbIDMintegiaImp.Text = "";
                    tbMarkaImp.Text = "";
                    btModeloaImprimagailua.Text = "";
                    comboBoxEgoeraImprimagailua.SelectedIndex = -1; // Ez hautatu ezer
                }
                else
                {
                    MessageBox.Show("Errorea gertatu da imprimagailua gehitzean.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea: " + ex.Message);
            }
        }


        private void btModeloaImp_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}