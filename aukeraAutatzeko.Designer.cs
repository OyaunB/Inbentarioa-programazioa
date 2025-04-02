namespace Inbentarioa
{
    partial class aukeraAutatzeko
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(aukeraAutatzeko));
            BtAukeraAutatu = new Label();
            BtGehitu = new Button();
            btAldatu = new Button();
            btAtzera = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // BtAukeraAutatu
            // 
            BtAukeraAutatu.AutoSize = true;
            BtAukeraAutatu.BackColor = Color.Transparent;
            BtAukeraAutatu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtAukeraAutatu.ForeColor = SystemColors.ControlLightLight;
            BtAukeraAutatu.Location = new Point(354, 52);
            BtAukeraAutatu.Name = "BtAukeraAutatu";
            BtAukeraAutatu.Size = new Size(340, 41);
            BtAukeraAutatu.TabIndex = 28;
            BtAukeraAutatu.Text = "AUTATU AUKERA";
            // 
            // BtGehitu
            // 
            BtGehitu.Anchor = AnchorStyles.None;
            BtGehitu.BackColor = Color.Transparent;
            BtGehitu.BackgroundImage = (Image)resources.GetObject("BtGehitu.BackgroundImage");
            BtGehitu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtGehitu.ForeColor = Color.Transparent;
            BtGehitu.Location = new Point(60, 184);
            BtGehitu.Name = "BtGehitu";
            BtGehitu.Size = new Size(284, 80);
            BtGehitu.TabIndex = 29;
            BtGehitu.UseVisualStyleBackColor = false;
            BtGehitu.Click += BtGehitu_Click;
            // 
            // btAldatu
            // 
            btAldatu.Anchor = AnchorStyles.None;
            btAldatu.BackColor = Color.Transparent;
            btAldatu.BackgroundImage = (Image)resources.GetObject("btAldatu.BackgroundImage");
            btAldatu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAldatu.ForeColor = Color.Transparent;
            btAldatu.Location = new Point(379, 184);
            btAldatu.Name = "btAldatu";
            btAldatu.Size = new Size(284, 80);
            btAldatu.TabIndex = 30;
            btAldatu.UseVisualStyleBackColor = false;
            btAldatu.Click += btAldatu_Click;
            // 
            // btAtzera
            // 
            btAtzera.Anchor = AnchorStyles.None;
            btAtzera.BackColor = Color.Transparent;
            btAtzera.BackgroundImage = (Image)resources.GetObject("btAtzera.BackgroundImage");
            btAtzera.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAtzera.ForeColor = Color.Transparent;
            btAtzera.Location = new Point(396, 325);
            btAtzera.Name = "btAtzera";
            btAtzera.Size = new Size(180, 80);
            btAtzera.TabIndex = 31;
            btAtzera.UseVisualStyleBackColor = false;
            btAtzera.Click += btAtzera_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(699, 184);
            button1.Name = "button1";
            button1.Size = new Size(291, 80);
            button1.TabIndex = 32;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // aukeraAutatzeko
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1035, 450);
            Controls.Add(button1);
            Controls.Add(btAtzera);
            Controls.Add(btAldatu);
            Controls.Add(BtGehitu);
            Controls.Add(BtAukeraAutatu);
            Name = "aukeraAutatzeko";
            Text = "Form8";
            Load += Form8_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label BtAukeraAutatu;
        private Button BtGehitu;
        private Button btAldatu;
        private Button btAtzera;
        private Button button1;
    }
}