namespace Fall2020_CSC403_Project
{
    partial class PauseMenu
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
            this.Paused = new System.Windows.Forms.Label();
            this.Resume = new System.Windows.Forms.Button();
            this.Quit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Paused
            // 
            this.Paused.AutoSize = true;
            this.Paused.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Paused.Font = new System.Drawing.Font("Microsoft YaHei UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Paused.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Paused.Location = new System.Drawing.Point(257, 42);
            this.Paused.Name = "Paused";
            this.Paused.Size = new System.Drawing.Size(304, 86);
            this.Paused.TabIndex = 0;
            this.Paused.Text = "PAUSED";
            this.Paused.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Resume
            // 
            this.Resume.Location = new System.Drawing.Point(334, 186);
            this.Resume.Name = "Resume";
            this.Resume.Size = new System.Drawing.Size(148, 45);
            this.Resume.TabIndex = 1;
            this.Resume.Text = "Resume";
            this.Resume.UseVisualStyleBackColor = true;
            this.Resume.Click += new System.EventHandler(this.ResumeGame);
            // 
            // Quit
            // 
            this.Quit.Location = new System.Drawing.Point(334, 287);
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(148, 45);
            this.Quit.TabIndex = 2;
            this.Quit.Text = "Quit";
            this.Quit.UseVisualStyleBackColor = true;
            this.Quit.Click += new System.EventHandler(this.QuiteGame);
            // 
            // PauseMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.wall;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.Resume);
            this.Controls.Add(this.Paused);
            this.Name = "PauseMenu";
            this.Text = "PauseMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Paused;
        private System.Windows.Forms.Button Resume;
        private System.Windows.Forms.Button Quit;
    }
}