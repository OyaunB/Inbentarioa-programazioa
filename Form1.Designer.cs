namespace Inbentarioa
{
    partial class Sarrera
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sarrera));
            LbErabiltzailea = new Label();
            label2 = new Label();
            LbPasahitza = new Label();
            TbErabiltzailea = new TextBox();
            TbPasahitza = new TextBox();
            LbIzarraitz = new Label();
            PB1JB = new PictureBox();
            BtBidali = new Button();
            ((System.ComponentModel.ISupportInitialize)PB1JB).BeginInit();
            SuspendLayout();
            // 
            // LbErabiltzailea
            // 
            LbErabiltzailea.AutoSize = true;
            LbErabiltzailea.BackColor = Color.Transparent;
            LbErabiltzailea.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LbErabiltzailea.ForeColor = SystemColors.ControlLightLight;
            LbErabiltzailea.Location = new Point(231, 156);
            LbErabiltzailea.Name = "LbErabiltzailea";
            LbErabiltzailea.Size = new Size(146, 25);
            LbErabiltzailea.TabIndex = 0;
            LbErabiltzailea.Text = "Erabiltzailea:";
            LbErabiltzailea.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(204, 203);
            label2.Name = "label2";
            label2.Size = new Size(0, 20);
            label2.TabIndex = 1;
            // 
            // LbPasahitza
            // 
            LbPasahitza.AutoSize = true;
            LbPasahitza.BackColor = Color.Transparent;
            LbPasahitza.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LbPasahitza.ForeColor = SystemColors.ControlLightLight;
            LbPasahitza.Location = new Point(260, 216);
            LbPasahitza.Name = "LbPasahitza";
            LbPasahitza.Size = new Size(117, 25);
            LbPasahitza.TabIndex = 2;
            LbPasahitza.Text = "Pasahitza:";
            LbPasahitza.Click += label3_Click;
            // 
            // TbErabiltzailea
            // 
            TbErabiltzailea.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TbErabiltzailea.Location = new Point(383, 158);
            TbErabiltzailea.Name = "TbErabiltzailea";
            TbErabiltzailea.Size = new Size(164, 26);
            TbErabiltzailea.TabIndex = 3;
            TbErabiltzailea.TextChanged += TbErabiltzailea_TextChanged;
            // 
            // TbPasahitza
            // 
            TbPasahitza.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TbPasahitza.Location = new Point(383, 217);
            TbPasahitza.Name = "TbPasahitza";
            TbPasahitza.PasswordChar = '*';
            TbPasahitza.Size = new Size(164, 26);
            TbPasahitza.TabIndex = 4;
            TbPasahitza.TextChanged += textBox2_TextChanged;
            // 
            // LbIzarraitz
            // 
            LbIzarraitz.AutoSize = true;
            LbIzarraitz.BackColor = Color.Transparent;
            LbIzarraitz.Font = new Font("Verdana", 40.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LbIzarraitz.ForeColor = SystemColors.ControlLightLight;
            LbIzarraitz.Location = new Point(245, 36);
            LbIzarraitz.Name = "LbIzarraitz";
            LbIzarraitz.Size = new Size(454, 80);
            LbIzarraitz.TabIndex = 5;
            LbIzarraitz.Text = "IZARRAITZ";
            // 
            // PB1JB
            // 
            PB1JB.BackColor = Color.Transparent;
            PB1JB.Image = (Image)resources.GetObject("PB1JB.Image");
            PB1JB.Location = new Point(107, 12);
            PB1JB.Name = "PB1JB";
            PB1JB.Size = new Size(132, 126);
            PB1JB.SizeMode = PictureBoxSizeMode.StretchImage;
            PB1JB.TabIndex = 6;
            PB1JB.TabStop = false;
            // 
            // BtBidali
            // 
            BtBidali.Anchor = AnchorStyles.None;
            BtBidali.BackColor = Color.Transparent;
            BtBidali.BackgroundImage = (Image)resources.GetObject("BtBidali.BackgroundImage");
            BtBidali.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtBidali.ForeColor = Color.Transparent;
            BtBidali.Location = new Point(330, 300);
            BtBidali.Name = "BtBidali";
            BtBidali.Size = new Size(163, 81);
            BtBidali.TabIndex = 7;
            BtBidali.UseVisualStyleBackColor = false;
            BtBidali.Click += button1_Click;
            // 
            // Sarrera
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BtBidali);
            Controls.Add(PB1JB);
            Controls.Add(LbIzarraitz);
            Controls.Add(TbPasahitza);
            Controls.Add(TbErabiltzailea);
            Controls.Add(LbPasahitza);
            Controls.Add(label2);
            Controls.Add(LbErabiltzailea);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "Sarrera";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Izarraitz";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)PB1JB).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LbErabiltzailea;
        private Label label2;
        private Label LbPasahitza;
        private TextBox TbErabiltzailea;
        private TextBox TbPasahitza;
        private Label LbIzarraitz;
        private PictureBox PB1JB;
        private Button BtBidali;
    }
}
