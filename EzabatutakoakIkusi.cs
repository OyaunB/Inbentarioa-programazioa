﻿using System;
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
    public partial class EzabatutakoakIkusi : Form
    {
        public EzabatutakoakIkusi()
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

        private void Form4_Load(object sender, EventArgs e)
        {
            DBEzabatutakoak db = new DBEzabatutakoak();  // Crear instancia
            DataTable dt = db.LortuEzabatutakoGailuak(); // Llamar al método desde la instancia
            DataGridViewEzabatutakoak.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Tabla f2 = new Izarraitz();
            //f2.Show();
        }

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            Aukerak f2 = new Aukerak();
            f2.ShowDialog();
        }

        private void TbGailuakGehitu_Click(object sender, EventArgs e)
        {

        }

        private void DataGridViewEzabatutakoak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
