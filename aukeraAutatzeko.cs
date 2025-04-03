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
    public partial class aukeraAutatzeko : Form
    {
        public aukeraAutatzeko()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

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
            GailuakGehitu form3 = new GailuakGehitu();
            form3.ShowDialog();
        }

        private void BtGehitu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ordenagailuagehitu form7 = new Ordenagailuagehitu();
            form7.ShowDialog();
        }

        private void btAldatu_Click(object sender, EventArgs e)
        {
            this.Hide();
            IMPRIMAGAILUAK form8 = new IMPRIMAGAILUAK();
            form8.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            BesteGailuBatGehitu form9 = new BesteGailuBatGehitu();
            form9.ShowDialog();
        }
    }
}
