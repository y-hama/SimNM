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
            this.Canvus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Canvus.Location = new System.Drawing.Point(0, 97);
            this.Canvus.Name = "Canvus";
            this.Canvus.Size = new System.Drawing.Size(500, 500);
            this.Canvus.TabIndex = 0;
            this.Canvus.TabStop = false;
            this.Canvus.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvus_MouseMove);
            // 
            // DrawTimer
            // 
            this.DrawTimer.Enabled = true;
            this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
            // 
            // View
            // 
            this.View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.View.Location = new System.Drawing.Point(0, 0);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(500, 97);
            this.View.TabIndex = 1;
            this.View.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 597);
            this.Controls.Add(this.View);
            this.Controls.Add(this.Canvus);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "MainForm";
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