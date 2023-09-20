namespace Whack_a_Mouse
{
    partial class Game
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tmrCountdown = new System.Windows.Forms.Timer(this.components);
            this.lblVrijeme = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblEsc = new System.Windows.Forms.Label();
            this.lblExit = new System.Windows.Forms.Label();
            this.lblAbout = new System.Windows.Forms.Label();
            this.lblScores = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.tmrSlide = new System.Windows.Forms.Timer(this.components);
            this.lblPoints = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 17;
            this.timer1.Tick += new System.EventHandler(this.Update);
            // 
            // tmrCountdown
            // 
            this.tmrCountdown.Interval = 1000;
            this.tmrCountdown.Tick += new System.EventHandler(this.tmrCountdown_Tick);
            // 
            // lblVrijeme
            // 
            this.lblVrijeme.AutoSize = true;
            this.lblVrijeme.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVrijeme.ForeColor = System.Drawing.Color.Black;
            this.lblVrijeme.Location = new System.Drawing.Point(14, 9);
            this.lblVrijeme.Name = "lblVrijeme";
            this.lblVrijeme.Size = new System.Drawing.Size(0, 31);
            this.lblVrijeme.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblEsc);
            this.panel1.Controls.Add(this.lblExit);
            this.panel1.Controls.Add(this.lblAbout);
            this.panel1.Controls.Add(this.lblScores);
            this.panel1.Controls.Add(this.lblHelp);
            this.panel1.Controls.Add(this.lblStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 599);
            this.panel1.TabIndex = 4;
            // 
            // lblEsc
            // 
            this.lblEsc.AutoSize = true;
            this.lblEsc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblEsc.ForeColor = System.Drawing.Color.Black;
            this.lblEsc.Location = new System.Drawing.Point(601, 13);
            this.lblEsc.Name = "lblEsc";
            this.lblEsc.Size = new System.Drawing.Size(194, 25);
            this.lblEsc.TabIndex = 14;
            this.lblEsc.Text = "Press ESC  to Quit";
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Font = new System.Drawing.Font("Arial Rounded MT Bold", 24.75F);
            this.lblExit.ForeColor = System.Drawing.Color.Black;
            this.lblExit.Location = new System.Drawing.Point(11, 541);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(111, 48);
            this.lblExit.TabIndex = 10;
            this.lblExit.Text = "EXIT";
            this.lblExit.Click += new System.EventHandler(this.label1_Click_1);
            this.lblExit.MouseEnter += new System.EventHandler(this.lblExit_MouseEnter);
            this.lblExit.MouseLeave += new System.EventHandler(this.lblExit_MouseLeave);
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.Font = new System.Drawing.Font("Algerian", 44.75F, System.Drawing.FontStyle.Bold);
            this.lblAbout.ForeColor = System.Drawing.Color.Black;
            this.lblAbout.Location = new System.Drawing.Point(422, 469);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(278, 83);
            this.lblAbout.TabIndex = 9;
            this.lblAbout.Text = "ABOUT";
            this.lblAbout.Click += new System.EventHandler(this.lblAbout_Click);
            this.lblAbout.MouseEnter += new System.EventHandler(this.lblAbout_MouseEnter);
            this.lblAbout.MouseLeave += new System.EventHandler(this.lblAbout_MouseLeave);
            // 
            // lblScores
            // 
            this.lblScores.AutoSize = true;
            this.lblScores.Font = new System.Drawing.Font("Algerian", 44.75F, System.Drawing.FontStyle.Bold);
            this.lblScores.ForeColor = System.Drawing.Color.Black;
            this.lblScores.Location = new System.Drawing.Point(422, 283);
            this.lblScores.Name = "lblScores";
            this.lblScores.Size = new System.Drawing.Size(305, 83);
            this.lblScores.TabIndex = 8;
            this.lblScores.Text = "SCORES";
            this.lblScores.Click += new System.EventHandler(this.lblScores_Click);
            this.lblScores.MouseEnter += new System.EventHandler(this.lblScores_MouseEnter);
            this.lblScores.MouseLeave += new System.EventHandler(this.lblScores_MouseLeave);
            // 
            // lblHelp
            // 
            this.lblHelp.AutoSize = true;
            this.lblHelp.Font = new System.Drawing.Font("Algerian", 44.75F, System.Drawing.FontStyle.Bold);
            this.lblHelp.ForeColor = System.Drawing.Color.Black;
            this.lblHelp.Location = new System.Drawing.Point(422, 375);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(216, 83);
            this.lblHelp.TabIndex = 7;
            this.lblHelp.Text = "HELP";
            this.lblHelp.Click += new System.EventHandler(this.lblHelp_Click);
            this.lblHelp.MouseEnter += new System.EventHandler(this.lblHelp_MouseEnter);
            this.lblHelp.MouseLeave += new System.EventHandler(this.lblHelp_MouseLeave);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblStart.Font = new System.Drawing.Font("Algerian", 44.75F, System.Drawing.FontStyle.Bold);
            this.lblStart.ForeColor = System.Drawing.Color.Black;
            this.lblStart.Location = new System.Drawing.Point(422, 191);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(270, 83);
            this.lblStart.TabIndex = 6;
            this.lblStart.Text = "START";
            this.lblStart.Click += new System.EventHandler(this.lblStart_Click);
            this.lblStart.MouseEnter += new System.EventHandler(this.lblStart_MouseEnter_1);
            this.lblStart.MouseLeave += new System.EventHandler(this.lblStart_MouseLeave);
            // 
            // tmrSlide
            // 
            this.tmrSlide.Interval = 1;
            this.tmrSlide.Tick += new System.EventHandler(this.tmrSlide_Tick);
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints.ForeColor = System.Drawing.Color.Black;
            this.lblPoints.Location = new System.Drawing.Point(25, 56);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(0, 31);
            this.lblPoints.TabIndex = 5;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(25F, 51F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 599);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblVrijeme);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.MaximizeBox = false;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Whack-a-Mouse";
            this.Load += new System.EventHandler(this.LoadForm);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Draw);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mouseClicked);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer tmrCountdown;
        private System.Windows.Forms.Label lblVrijeme;
        private System.Windows.Forms.Timer tmrSlide;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Label lblScores;
        private System.Windows.Forms.Label lblHelp;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.Label lblEsc;
    }
}

