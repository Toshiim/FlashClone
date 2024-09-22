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
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.FlashName = new System.Windows.Forms.Label();
            this.FIDLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(325, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 56);
            this.button1.TabIndex = 3;
            this.button1.Text = "Select folder for back ups";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(325, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 63);
            this.button2.TabIndex = 4;
            this.button2.Text = "Select original file";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FlashName
            // 
            this.FlashName.AutoSize = true;
            this.FlashName.Location = new System.Drawing.Point(375, 9);
            this.FlashName.Name = "FlashName";
            this.FlashName.Size = new System.Drawing.Size(0, 13);
            this.FlashName.TabIndex = 5;
            // 
            // FIDLabel
            // 
            this.FIDLabel.AutoSize = true;
            this.FIDLabel.Location = new System.Drawing.Point(375, 50);
            this.FIDLabel.Name = "FIDLabel";
            this.FIDLabel.Size = new System.Drawing.Size(0, 13);
            this.FIDLabel.TabIndex = 6;
            // 
            // FlashClone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 574);
            this.Controls.Add(this.FIDLabel);
            this.Controls.Add(this.FlashName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FlashClone";
            this.Text = "FleshClone";
            this.Load += new System.EventHandler(this.FleshClone_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label FlashName;
        private System.Windows.Forms.Label FIDLabel;
    }
}