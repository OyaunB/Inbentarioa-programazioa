﻿//SarrerakoAutaketa.cs
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
    public partial class Aukerak : Form
    {
        public string erabiltzaileRola;
        public Aukerak()
        {
            InitializeComponent();
        }

        private void KudeatuBaimenak()
        {
            // Primero, deshabilitamos TODOS los botones por defecto
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button)
                    ctrl.Enabled = false;
            }

            // Convertimos el rol a minúsculas para evitar problemas de mayúsculas/minúsculas
            string rol = Errola.ErabiltzaileRola.ToLower();

            // Resto del código permanece igual...
            if (rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt")
            {
                // **Zuzendaria / IKT Irakaslea**: Acceso total
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Button)
                        ctrl.Enabled = true;
                }
            }
            else if (rol == "irakaslea")
            {
                // **Irakaslea**: Solo puede usar el botón "ALDATU"
                BtGailuakKudeatu.Enabled = true;
                BtEzabatutakoakIkusi.Enabled = true;
                BtMintegiakKudeatu.Enabled = true;
            }
            else if (rol == "ordezkaria")
            {
                // **Ordezkaria**: Solo puede usar BtGailuakKudeatu y BtEzabatutakoakIkusi
                BtGailuakKudeatu.Enabled = true;
                BtEzabatutakoakIkusi.Enabled = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Definir los colores del degradado usando códigos hexadecimales
            Color colorInicio = ColorTranslator.FromHtml("#5de0e6"); // Azul claro
            Color colorFin = ColorTranslator.FromHtml("#004aad");    // Azul oscuro

            // Crear un pincel con degradado lineal
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, // Área donde se aplicará el degradado
                colorInicio,         // Color inicial
                colorFin,            // Color final
                LinearGradientMode.Horizontal)) // Dirección del degradado (horizontal)
            {
                // Rellenar el fondo del formulario con el degradado
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        

        public Aukerak(string rola)
        {
            InitializeComponent();
            erabiltzaileRola = rola;
            KudeatuBaimenak();
        }

      
        private void BtGailuakKudeatu_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultar formulario actual
            using (GailuakGehitu f3 = new GailuakGehitu())
            {
                f3.ShowDialog(); // Mostrar formulario hijo
            }
            // this.Show(); // Volver al formulario principal
            this.Close(); // Cerramos el formulario actual
            KudeatuBaimenak(); // Reaplicar permisos
        }

        private void BtEzabatutakoakIkusi_Click(object sender, EventArgs e)
        {
           // this.Hide();
            EzabatutakoakIkusi f4 = new EzabatutakoakIkusi();
            f4.ShowDialog();
        }

        private void BtMintegiakKudeatu_Click(object sender, EventArgs e)
        {
            this.Hide();
            MintegienKudeaketa f5 = new MintegienKudeaketa();
            f5.ShowDialog();

        }

        private void BtErabiltzaileakKudeatu_Click(object sender, EventArgs e)
        {
            this.Hide();
            ErabiltzaileakKudeatu f6 = new ErabiltzaileakKudeatu();
            f6.ShowDialog();
        }

        private void Aukerak_Load(object sender, EventArgs e)
        {
            KudeatuBaimenak();
        }
    }
}
