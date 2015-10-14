﻿namespace LSS_Host_Module.UI
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageIO = new System.Windows.Forms.TabPage();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbAO = new System.Windows.Forms.GroupBox();
            this.trackBarAO = new System.Windows.Forms.TrackBar();
            this.labelAOValue = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabPageIO.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.gbAO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAO)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageIO);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 24);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(923, 453);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageIO
            // 
            this.tabPageIO.Controls.Add(this.gbAO);
            this.tabPageIO.Location = new System.Drawing.Point(4, 22);
            this.tabPageIO.Name = "tabPageIO";
            this.tabPageIO.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIO.Size = new System.Drawing.Size(915, 427);
            this.tabPageIO.TabIndex = 0;
            this.tabPageIO.Text = "IO";
            this.tabPageIO.UseVisualStyleBackColor = true;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(923, 24);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gbAO
            // 
            this.gbAO.Controls.Add(this.labelAOValue);
            this.gbAO.Controls.Add(this.trackBarAO);
            this.gbAO.Location = new System.Drawing.Point(7, 7);
            this.gbAO.Name = "gbAO";
            this.gbAO.Size = new System.Drawing.Size(321, 208);
            this.gbAO.TabIndex = 0;
            this.gbAO.TabStop = false;
            this.gbAO.Text = "Analog Output";
            // 
            // trackBarAO
            // 
            this.trackBarAO.Location = new System.Drawing.Point(7, 20);
            this.trackBarAO.Maximum = 50;
            this.trackBarAO.Name = "trackBarAO";
            this.trackBarAO.Size = new System.Drawing.Size(247, 45);
            this.trackBarAO.TabIndex = 0;
            // 
            // labelAOValue
            // 
            this.labelAOValue.AutoSize = true;
            this.labelAOValue.Location = new System.Drawing.Point(260, 36);
            this.labelAOValue.Name = "labelAOValue";
            this.labelAOValue.Size = new System.Drawing.Size(13, 13);
            this.labelAOValue.TabIndex = 1;
            this.labelAOValue.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 477);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.Text = "LSS Host Controller";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControlMain.ResumeLayout(false);
            this.tabPageIO.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.gbAO.ResumeLayout(false);
            this.gbAO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageIO;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbAO;
        private System.Windows.Forms.Label labelAOValue;
        private System.Windows.Forms.TrackBar trackBarAO;
    }
}
