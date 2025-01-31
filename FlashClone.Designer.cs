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
            this.TimeLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonBackUp
            // 
            this.buttonBackUp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonBackUp.Location = new System.Drawing.Point(370, 206);
            this.buttonBackUp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBackUp.Name = "buttonBackUp";
            this.buttonBackUp.Size = new System.Drawing.Size(139, 51);
            this.buttonBackUp.TabIndex = 3;
            this.buttonBackUp.Text = "Select folder for back ups";
            this.buttonBackUp.UseVisualStyleBackColor = true;
            this.buttonBackUp.Click += new System.EventHandler(this.buttonBackUp_Click);
            // 
            // FlashName
            // 
            this.FlashName.AutoSize = true;
            this.FlashName.Location = new System.Drawing.Point(16, 11);
            this.FlashName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FlashName.Name = "FlashName";
            this.FlashName.Size = new System.Drawing.Size(101, 16);
            this.FlashName.TabIndex = 5;
            this.FlashName.Text = "NAME OF DISC";
            // 
            // FIDLabel
            // 
            this.FIDLabel.AutoSize = true;
            this.FIDLabel.Location = new System.Drawing.Point(16, 47);
            this.FIDLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FIDLabel.Name = "FIDLabel";
            this.FIDLabel.Size = new System.Drawing.Size(75, 16);
            this.FIDLabel.TabIndex = 6;
            this.FIDLabel.Text = "ID OF DISC";
            // 
            // Opath
            // 
            this.Opath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Opath.AutoSize = true;
            this.Opath.Location = new System.Drawing.Point(44, 100);
            this.Opath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Opath.Name = "Opath";
            this.Opath.Size = new System.Drawing.Size(161, 16);
            this.Opath.TabIndex = 7;
            this.Opath.Text = "PATH TO ORIGINAL FILE";
            // 
            // Spath
            // 
            this.Spath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Spath.AutoSize = true;
            this.Spath.Location = new System.Drawing.Point(44, 223);
            this.Spath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Spath.Name = "Spath";
            this.Spath.Size = new System.Drawing.Size(155, 16);
            this.Spath.TabIndex = 8;
            this.Spath.Text = "PATH TO RESERV FILE";
            // 
            // ButtonOriginal
            // 
            this.ButtonOriginal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ButtonOriginal.Location = new System.Drawing.Point(370, 87);
            this.ButtonOriginal.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonOriginal.Name = "ButtonOriginal";
            this.ButtonOriginal.Size = new System.Drawing.Size(139, 42);
            this.ButtonOriginal.TabIndex = 9;
            this.ButtonOriginal.Text = "Select Original File";
            this.ButtonOriginal.UseVisualStyleBackColor = true;
            this.ButtonOriginal.Click += new System.EventHandler(this.ButtonOriginal_Click);
            // 
            // buttonForCopy
            // 
            this.buttonForCopy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonForCopy.Location = new System.Drawing.Point(370, 317);
            this.buttonForCopy.Margin = new System.Windows.Forms.Padding(4);
            this.buttonForCopy.Name = "buttonForCopy";
            this.buttonForCopy.Size = new System.Drawing.Size(139, 28);
            this.buttonForCopy.TabIndex = 10;
            this.buttonForCopy.Text = "Copy";
            this.buttonForCopy.UseVisualStyleBackColor = true;
            this.buttonForCopy.Click += new System.EventHandler(this.buttonForCopy_Click);
            // 
            // TimeLable
            // 
            this.TimeLable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLable.AutoSize = true;
            this.TimeLable.Location = new System.Drawing.Point(470, 550);
            this.TimeLable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TimeLable.Name = "TimeLable";
            this.TimeLable.Size = new System.Drawing.Size(39, 16);
            this.TimeLable.TabIndex = 11;
            this.TimeLable.Text = "TIME";
            // 
            // FlashClone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 575);
            this.Controls.Add(this.TimeLable);
            this.Controls.Add(this.buttonForCopy);
            this.Controls.Add(this.ButtonOriginal);
            this.Controls.Add(this.Spath);
            this.Controls.Add(this.Opath);
            this.Controls.Add(this.FIDLabel);
            this.Controls.Add(this.FlashName);
            this.Controls.Add(this.buttonBackUp);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Label TimeLable;
    }
}