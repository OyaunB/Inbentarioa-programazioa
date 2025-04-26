using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Inbentarioa
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

            // Tamaños configurados
            Size buttonSize = new Size(90, 30);  // Ancho aumentado a 90, alto a 30
            int verticalSpacing = 10;            // Espaciado entre controles
            int formWidth = 230;                 // Ancho del formulario aumentado

            // Configurar ComboBox
            comboBoxErrolak = new ComboBox();
            comboBoxErrolak.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxErrolak.DataSource = errolak;
            comboBoxErrolak.Location = new Point(10, 10);
            comboBoxErrolak.Size = new Size(formWidth - 20, 25); // Ancho relativo al formulario
            this.Controls.Add(comboBoxErrolak);

            // Configurar botón Aceptar
            btnOnartu = new Button();
            btnOnartu.Text = "Onartu";
            btnOnartu.Size = buttonSize;
            btnOnartu.Location = new Point(10, comboBoxErrolak.Bottom + verticalSpacing);
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
            btnUtzi.Size = buttonSize;
            btnUtzi.Location = new Point(btnOnartu.Right + 10, btnOnartu.Top);
            btnUtzi.Click += (sender, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            this.Controls.Add(btnUtzi);

            // Configurar el formulario
            this.Text = "Aukeratu Errola";
            this.ClientSize = new Size(formWidth, btnOnartu.Bottom + 15); // Altura automática
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void ErrolaAukeraketaForm_Load(object sender, EventArgs e)
        {
            // Código de carga si es necesario
        }
    }
}