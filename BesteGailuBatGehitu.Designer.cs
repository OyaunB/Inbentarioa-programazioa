namespace Inbentarioa
{
    partial class BesteGailuBatGehitu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BesteGailuBatGehitu));
            BtAukeraAutatu = new Label();
            lbmintkodea = new Label();
            label1 = new Label();
            label2 = new Label();
            tbMarka = new TextBox();
            tbModeloa = new TextBox();
            bidaliBotoia = new Button();
            btAtzera = new Button();
            comboBoxEgoera = new ComboBox();
            SuspendLayout();
            // 
            // BtAukeraAutatu
            // 
            BtAukeraAutatu.AutoSize = true;
            BtAukeraAutatu.BackColor = Color.Transparent;
            BtAukeraAutatu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtAukeraAutatu.ForeColor = SystemColors.ControlLightLight;
            BtAukeraAutatu.Location = new Point(118, 31);
            BtAukeraAutatu.Name = "BtAukeraAutatu";
            BtAukeraAutatu.Size = new Size(520, 41);
            BtAukeraAutatu.TabIndex = 30;
            BtAukeraAutatu.Text = "BESTE GAILU BAT GEHITU";
            BtAukeraAutatu.Click += BtAukeraAutatu_Click;
            // 
            // lbmintkodea
            // 
            lbmintkodea.AutoSize = true;
            lbmintkodea.BackColor = Color.Transparent;
            lbmintkodea.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbmintkodea.ForeColor = SystemColors.ControlLightLight;
            lbmintkodea.Location = new Point(154, 176);
            lbmintkodea.Name = "lbmintkodea";
            lbmintkodea.Size = new Size(115, 28);
            lbmintkodea.TabIndex = 43;
            lbmintkodea.Text = "MARKA:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(118, 229);
            label1.Name = "label1";
            label1.Size = new Size(151, 28);
            label1.TabIndex = 44;
            label1.Text = "MODELOA:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(159, 289);
            label2.Name = "label2";
            label2.Size = new Size(110, 28);
            label2.TabIndex = 45;
            label2.Text = "Egoera:";
            // 
            // tbMarka
            // 
            tbMarka.Location = new Point(345, 180);
            tbMarka.Name = "tbMarka";
            tbMarka.Size = new Size(260, 27);
            tbMarka.TabIndex = 46;
            // 
            // tbModeloa
            // 
            tbModeloa.Location = new Point(345, 233);
            tbModeloa.Name = "tbModeloa";
            tbModeloa.Size = new Size(260, 27);
            tbModeloa.TabIndex = 47;
            // 
            // bidaliBotoia
            // 
            bidaliBotoia.BackgroundImage = (Image)resources.GetObject("bidaliBotoia.BackgroundImage");
            bidaliBotoia.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bidaliBotoia.Location = new Point(186, 341);
            bidaliBotoia.Name = "bidaliBotoia";
            bidaliBotoia.Size = new Size(173, 80);
            bidaliBotoia.TabIndex = 49;
            bidaliBotoia.UseVisualStyleBackColor = true;
            bidaliBotoia.Click += bidaliBotoia_Click;
            // 
            // btAtzera
            // 
            btAtzera.Anchor = AnchorStyles.None;
            btAtzera.BackColor = Color.Transparent;
            btAtzera.BackgroundImage = (Image)resources.GetObject("btAtzera.BackgroundImage");
            btAtzera.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAtzera.ForeColor = Color.Transparent;
            btAtzera.Location = new Point(382, 341);
            btAtzera.Name = "btAtzera";
            btAtzera.Size = new Size(180, 80);
            btAtzera.TabIndex = 50;
            btAtzera.UseVisualStyleBackColor = false;
            btAtzera.Click += btAtzera_Click;
            // 
            // comboBoxEgoera
            // 
            comboBoxEgoera.FormattingEnabled = true;
            comboBoxEgoera.Location = new Point(345, 289);
            comboBoxEgoera.Name = "comboBoxEgoera";
            comboBoxEgoera.Size = new Size(260, 28);
            comboBoxEgoera.TabIndex = 55;
            comboBoxEgoera.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // BesteGailuBatGehitu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBoxEgoera);
            Controls.Add(btAtzera);
            Controls.Add(bidaliBotoia);
            Controls.Add(tbModeloa);
            Controls.Add(tbMarka);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lbmintkodea);
            Controls.Add(BtAukeraAutatu);
            Name = "BesteGailuBatGehitu";
            Text = "Form9";
            Load += Form9_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label BtAukeraAutatu;
        private Label lbmintkodea;
        private Label label1;
        private Label label2;
        private TextBox tbMarka;
        private TextBox tbModeloa;
        private Button bidaliBotoia;
        private Button btAtzera;
        private ComboBox comboBoxEgoera;
    }
}