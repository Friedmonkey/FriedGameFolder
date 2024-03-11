namespace FriedGameFolder
{
    partial class GameForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrLoad = new System.Windows.Forms.Timer(this.components);
            this.flowFolder = new System.Windows.Forms.FlowLayoutPanel();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPath = new System.Windows.Forms.Label();
            this.makeTransparentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowFolder.SuspendLayout();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrLoad
            // 
            this.tmrLoad.Tick += new System.EventHandler(this.tmrLoad_Tick);
            // 
            // flowFolder
            // 
            this.flowFolder.AllowDrop = true;
            this.flowFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowFolder.AutoScroll = true;
            this.flowFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flowFolder.ContextMenuStrip = this.ctxMenu;
            this.flowFolder.Controls.Add(this.lblPath);
            this.flowFolder.Location = new System.Drawing.Point(5, 5);
            this.flowFolder.Name = "flowFolder";
            this.flowFolder.Size = new System.Drawing.Size(392, 392);
            this.flowFolder.TabIndex = 0;
            this.flowFolder.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowFolder_DragDrop);
            this.flowFolder.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowFolder_DragEnter);
            this.flowFolder.DragLeave += new System.EventHandler(this.flowFolder_DragLeave);
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeBackgroundToolStripMenuItem,
            this.makeTransparentToolStripMenuItem});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(183, 70);
            // 
            // changeBackgroundToolStripMenuItem
            // 
            this.changeBackgroundToolStripMenuItem.Name = "changeBackgroundToolStripMenuItem";
            this.changeBackgroundToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.changeBackgroundToolStripMenuItem.Text = "Change Background";
            this.changeBackgroundToolStripMenuItem.Click += new System.EventHandler(this.changeBackgroundToolStripMenuItem_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.BackColor = System.Drawing.Color.White;
            this.lblPath.Location = new System.Drawing.Point(3, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(37, 13);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "path ?";
            // 
            // makeTransparentToolStripMenuItem
            // 
            this.makeTransparentToolStripMenuItem.Name = "makeTransparentToolStripMenuItem";
            this.makeTransparentToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.makeTransparentToolStripMenuItem.Text = "Make transparent";
            this.makeTransparentToolStripMenuItem.Click += new System.EventHandler(this.makeTransparentToolStripMenuItem_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.flowFolder);
            this.Name = "GameForm";
            this.Size = new System.Drawing.Size(400, 400);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.flowFolder.ResumeLayout(false);
            this.flowFolder.PerformLayout();
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrLoad;
        private System.Windows.Forms.FlowLayoutPanel flowFolder;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem changeBackgroundToolStripMenuItem;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.ToolStripMenuItem makeTransparentToolStripMenuItem;
    }
}
