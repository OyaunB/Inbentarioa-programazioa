namespace Inbentarioa
{
    partial class IMPRIMAGAILUAK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IMPRIMAGAILUAK));
            BtAukeraAutatu = new Label();
            textBox1 = new TextBox();
            textBox3 = new TextBox();
            lbmintkodea = new Label();
            label1 = new Label();
            label2 = new Label();
            btAtzera = new Button();
            bidaliBotoia = new Button();
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // BtAukeraAutatu
            // 
            BtAukeraAutatu.AutoSize = true;
            BtAukeraAutatu.BackColor = Color.Transparent;
            BtAukeraAutatu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtAukeraAutatu.ForeColor = SystemColors.ControlLightLight;
            BtAukeraAutatu.Location = new Point(141, 48);
            BtAukeraAutatu.Name = "BtAukeraAutatu";
            BtAukeraAutatu.Size = new Size(534, 41);
            BtAukeraAutatu.TabIndex = 29;
            BtAukeraAutatu.Text = "IMPRIMAGAILUAK GEHITU";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(364, 208);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(260, 27);
            textBox1.TabIndex = 30;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(364, 151);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(260, 27);
            textBox3.TabIndex = 32;
            // 
            // lbmintkodea
            // 
            lbmintkodea.AutoSize = true;
            lbmintkodea.BackColor = Color.Transparent;
            lbmintkodea.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbmintkodea.ForeColor = SystemColors.ControlLightLight;
            lbmintkodea.Location = new Point(188, 147);
            lbmintkodea.Name = "lbmintkodea";
            lbmintkodea.Size = new Size(115, 28);
            lbmintkodea.TabIndex = 42;
            lbmintkodea.Text = "MARKA:";
            lbmintkodea.Click += lbmintkodea_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(152, 208);
            label1.Name = "label1";
            label1.Size = new Size(151, 28);
            label1.TabIndex = 43;
            label1.Text = "MODELOA:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(175, 270);
            label2.Name = "label2";
            label2.Size = new Size(128, 28);
            label2.TabIndex = 44;
            label2.Text = "EGOERA:";
            // 
            // btAtzera
            // 
            btAtzera.Anchor = AnchorStyles.None;
            btAtzera.BackColor = Color.Transparent;
            btAtzera.BackgroundImage = (Image)resources.GetObject("btAtzera.BackgroundImage");
            btAtzera.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAtzera.ForeColor = Color.Transparent;
            btAtzera.Location = new Point(403, 336);
            btAtzera.Name = "btAtzera";
            btAtzera.Size = new Size(180, 80);
            btAtzera.TabIndex = 45;
            btAtzera.UseVisualStyleBackColor = false;
            btAtzera.Click += btAtzera_Click;
            // 
            // bidaliBotoia
            // 
            bidaliBotoia.BackgroundImage = (Image)resources.GetObject("bidaliBotoia.BackgroundImage");
            bidaliBotoia.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bidaliBotoia.Location = new Point(199, 336);
            bidaliBotoia.Name = "bidaliBotoia";
            bidaliBotoia.Size = new Size(173, 80);
            bidaliBotoia.TabIndex = 46;
            bidaliBotoia.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Ongi", "Apurtuta", "Kompontzen" });
            comboBox1.Location = new Point(364, 270);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(260, 28);
            comboBox1.TabIndex = 56;
            // 
            // IMPRIMAGAILUAK
            // 
            AccessibleName = "IMPRIMAGAILUAK";
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBox1);
            Controls.Add(bidaliBotoia);
            Controls.Add(btAtzera);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lbmintkodea);
            Controls.Add(textBox3);
            Controls.Add(textBox1);
            Controls.Add(BtAukeraAutatu);
            Name = "IMPRIMAGAILUAK";
            Text = "Izarraitz";
            Load += IMPRIMAGAILUAK_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label BtAukeraAutatu;
        private TextBox textBox1;
        private TextBox textBox3;
        private Label lbmintkodea;
        private Label label1;
        private Label label2;
        private Button btAtzera;
        private Button bidaliBotoia;
        private ComboBox comboBox1;
    }
}