namespace FriedGameFolderInstaller
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bttnUninstall = new System.Windows.Forms.Button();
            this.bttnInstall = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bttnInstall);
            this.groupBox1.Controls.Add(this.bttnUninstall);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 344);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "(un)install";
            // 
            // bttnUninstall
            // 
            this.bttnUninstall.Location = new System.Drawing.Point(6, 230);
            this.bttnUninstall.Name = "bttnUninstall";
            this.bttnUninstall.Size = new System.Drawing.Size(250, 108);
            this.bttnUninstall.TabIndex = 0;
            this.bttnUninstall.Text = "Uninstall";
            this.bttnUninstall.UseVisualStyleBackColor = true;
            this.bttnUninstall.Click += new System.EventHandler(this.bttnUninstall_Click);
            // 
            // bttnInstall
            // 
            this.bttnInstall.Location = new System.Drawing.Point(6, 19);
            this.bttnInstall.Name = "bttnInstall";
            this.bttnInstall.Size = new System.Drawing.Size(250, 108);
            this.bttnInstall.TabIndex = 1;
            this.bttnInstall.Text = "Install";
            this.bttnInstall.UseVisualStyleBackColor = true;
            this.bttnInstall.Click += new System.EventHandler(this.bttnInstall_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 369);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(301, 408);
            this.MinimumSize = new System.Drawing.Size(301, 408);
            this.Name = "Form1";
            this.Text = "FriedGamesFolder installer";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bttnInstall;
        private System.Windows.Forms.Button bttnUninstall;
    }
}

