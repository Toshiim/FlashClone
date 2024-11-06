namespace FleshClone
{
    partial class FlashClone
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
            this.buttonBackUp = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.FlashName = new System.Windows.Forms.Label();
            this.FIDLabel = new System.Windows.Forms.Label();
            this.Opath = new System.Windows.Forms.Label();
            this.Spath = new System.Windows.Forms.Label();
            this.ButtonOriginal = new System.Windows.Forms.Button();
            this.buttonForCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBackUp
            // 
            this.buttonBackUp.Location = new System.Drawing.Point(329, 203);
            this.buttonBackUp.Name = "buttonBackUp";
            this.buttonBackUp.Size = new System.Drawing.Size(104, 56);
            this.buttonBackUp.TabIndex = 3;
            this.buttonBackUp.Text = "Select folder for back ups";
            this.buttonBackUp.UseVisualStyleBackColor = true;
            this.buttonBackUp.Click += new System.EventHandler(this.buttonBackUp_Click);
            // 
            // FlashName
            // 
            this.FlashName.AutoSize = true;
            this.FlashName.Location = new System.Drawing.Point(12, 9);
            this.FlashName.Name = "FlashName";
            this.FlashName.Size = new System.Drawing.Size(83, 13);
            this.FlashName.TabIndex = 5;
            this.FlashName.Text = "NAME OF DISC";
            // 
            // FIDLabel
            // 
            this.FIDLabel.AutoSize = true;
            this.FIDLabel.Location = new System.Drawing.Point(12, 38);
            this.FIDLabel.Name = "FIDLabel";
            this.FIDLabel.Size = new System.Drawing.Size(63, 13);
            this.FIDLabel.TabIndex = 6;
            this.FIDLabel.Text = "ID OF DISC";
            // 
            // Opath
            // 
            this.Opath.AutoSize = true;
            this.Opath.Location = new System.Drawing.Point(56, 125);
            this.Opath.Name = "Opath";
            this.Opath.Size = new System.Drawing.Size(133, 13);
            this.Opath.TabIndex = 7;
            this.Opath.Text = "PATH TO ORIGINAL FILE";
            // 
            // Spath
            // 
            this.Spath.AutoSize = true;
            this.Spath.Location = new System.Drawing.Point(56, 225);
            this.Spath.Name = "Spath";
            this.Spath.Size = new System.Drawing.Size(126, 13);
            this.Spath.TabIndex = 8;
            this.Spath.Text = "PATH TO RESERV FILE";
            // 
            // ButtonOriginal
            // 
            this.ButtonOriginal.Location = new System.Drawing.Point(329, 104);
            this.ButtonOriginal.Name = "ButtonOriginal";
            this.ButtonOriginal.Size = new System.Drawing.Size(104, 54);
            this.ButtonOriginal.TabIndex = 9;
            this.ButtonOriginal.Text = "Select Original File";
            this.ButtonOriginal.UseVisualStyleBackColor = true;
            this.ButtonOriginal.Click += new System.EventHandler(this.ButtonOriginal_Click);
            // 
            // buttonForCopy
            // 
            this.buttonForCopy.Location = new System.Drawing.Point(329, 304);
            this.buttonForCopy.Name = "buttonForCopy";
            this.buttonForCopy.Size = new System.Drawing.Size(104, 23);
            this.buttonForCopy.TabIndex = 10;
            this.buttonForCopy.Text = "Copy";
            this.buttonForCopy.UseVisualStyleBackColor = true;
            this.buttonForCopy.Click += new System.EventHandler(this.buttonForCopy_Click);
            // 
            // FlashClone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 574);
            this.Controls.Add(this.buttonForCopy);
            this.Controls.Add(this.ButtonOriginal);
            this.Controls.Add(this.Spath);
            this.Controls.Add(this.Opath);
            this.Controls.Add(this.FIDLabel);
            this.Controls.Add(this.FlashName);
            this.Controls.Add(this.buttonBackUp);
            this.Name = "FlashClone";
            this.Text = "FleshClone";
            this.Load += new System.EventHandler(this.FleshClone_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonBackUp;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label FlashName;
        private System.Windows.Forms.Label FIDLabel;
        private System.Windows.Forms.Label Opath;
        private System.Windows.Forms.Label Spath;
        private System.Windows.Forms.Button ButtonOriginal;
        private System.Windows.Forms.Button buttonForCopy;
    }
}