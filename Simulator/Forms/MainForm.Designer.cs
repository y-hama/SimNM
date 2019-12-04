namespace Simulator.Forms
{
    partial class MainForm
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
            this.Canvus = new System.Windows.Forms.PictureBox();
            this.DrawTimer = new System.Windows.Forms.Timer(this.components);
            this.View = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Canvus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).BeginInit();
            this.SuspendLayout();
            // 
            // Canvus
            // 
            this.Canvus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvus.Location = new System.Drawing.Point(0, 0);
            this.Canvus.Name = "Canvus";
            this.Canvus.Size = new System.Drawing.Size(560, 560);
            this.Canvus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Canvus.TabIndex = 0;
            this.Canvus.TabStop = false;
            this.Canvus.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvus_MouseClick);
            this.Canvus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvus_MouseDown);
            this.Canvus.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvus_MouseMove);
            this.Canvus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvus_MouseUp);
            // 
            // DrawTimer
            // 
            this.DrawTimer.Enabled = true;
            this.DrawTimer.Interval = 10;
            this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
            // 
            // View
            // 
            this.View.BackColor = System.Drawing.Color.Black;
            this.View.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.View.Location = new System.Drawing.Point(0, 560);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(560, 19);
            this.View.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.View.TabIndex = 1;
            this.View.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 579);
            this.Controls.Add(this.Canvus);
            this.Controls.Add(this.View);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Canvus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Canvus;
        private System.Windows.Forms.Timer DrawTimer;
        private System.Windows.Forms.PictureBox View;
    }
}