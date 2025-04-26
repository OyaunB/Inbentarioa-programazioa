using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Inbentarioa  // Asegúrate de que coincida con el namespace de tu proyecto
{
    public partial class ErrolaAukeraketaForm : Form
    {
        private ComboBox comboBoxErrolak;
        private Button btnOnartu;
        private Button btnUtzi;

        public string AukeratutakoErrola { get; set; } 

        public ErrolaAukeraketaForm(List<string> errolak)
        {
            InitializeComponent();

            // Configurar ComboBox
            comboBoxErrolak = new ComboBox();
            comboBoxErrolak.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxErrolak.DataSource = errolak;
            comboBoxErrolak.Location = new Point(10, 10);
            comboBoxErrolak.Size = new Size(200, 20);
            this.Controls.Add(comboBoxErrolak);

            // Configurar botón Aceptar
            btnOnartu = new Button();
            btnOnartu.Text = "Onartu";
            btnOnartu.Location = new Point(10, 40);
            btnOnartu.Click += (sender, e) =>
            {
                AukeratutakoErrola = comboBoxErrolak.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.Controls.Add(btnOnartu);

            // Configurar botón Cancelar
            btnUtzi = new Button();
            btnUtzi.Text = "Utzi";
            btnUtzi.Location = new Point(100, 40);
            btnUtzi.Click += (sender, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            this.Controls.Add(btnUtzi);

            // Configurar el formulario
            this.Text = "Aukeratu Errola";
            this.ClientSize = new Size(220, 80);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
    }
}