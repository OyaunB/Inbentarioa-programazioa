namespace Inbentarioa
{
    partial class Form9
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form9));
            BtAukeraAutatu = new Label();
            lbmintkodea = new Label();
            label1 = new Label();
            label2 = new Label();
            textBox3 = new TextBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            bidaliBotoia = new Button();
            btAtzera = new Button();
            SuspendLayout();
            // 
            // BtAukeraAutatu
            // 
            BtAukeraAutatu.AutoSize = true;
            BtAukeraAutatu.BackColor = Color.Transparent;
            BtAukeraAutatu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtAukeraAutatu.ForeColor = SystemColors.ControlLightLight;
            BtAukeraAutatu.Location = new Point(118, 73);
            BtAukeraAutatu.Name = "BtAukeraAutatu";
            BtAukeraAutatu.Size = new Size(520, 41);
            BtAukeraAutatu.TabIndex = 30;
            BtAukeraAutatu.Text = "BESTE GAILU BAT GEHITU";
            // 
            // lbmintkodea
            // 
            lbmintkodea.AutoSize = true;
            lbmintkodea.BackColor = Color.Transparent;
            lbmintkodea.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbmintkodea.ForeColor = SystemColors.ControlLightLight;
            lbmintkodea.Location = new Point(148, 176);
            lbmintkodea.Name = "lbmintkodea";
            lbmintkodea.Size = new Size(157, 28);
            lbmintkodea.TabIndex = 43;
            lbmintkodea.Text = "ID Gailuak:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(202, 233);
            label1.Name = "label1";
            label1.Size = new Size(103, 28);
            label1.TabIndex = 44;
            label1.Text = "IZENA:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(186, 292);
            label2.Name = "label2";
            label2.Size = new Size(110, 28);
            label2.TabIndex = 45;
            label2.Text = "Egoera:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(345, 180);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(260, 27);
            textBox3.TabIndex = 46;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(345, 233);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(260, 27);
            textBox1.TabIndex = 47;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(345, 293);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(260, 27);
            textBox2.TabIndex = 48;
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
            // Form9
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btAtzera);
            Controls.Add(bidaliBotoia);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(textBox3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lbmintkodea);
            Controls.Add(BtAukeraAutatu);
            Name = "Form9";
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
        private TextBox textBox3;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button bidaliBotoia;
        private Button btAtzera;
    }
}