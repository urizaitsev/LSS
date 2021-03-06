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
        private void 
            InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageIO = new System.Windows.Forms.TabPage();
            this.tabPageTempControl = new System.Windows.Forms.TabPage();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tempSensorArrayControl = new LSS_Host_Module.UI.TempSensorArrayControl();
            this.signalGeneratorControl = new LSS_Host_Module.UI.SignalGeneratorControl();
            this.tempLoopControl = new LSS_Host_Module.UI.TempLoopControl();
            this.tabControlMain.SuspendLayout();
            this.tabPageIO.SuspendLayout();
            this.tabPageTempControl.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageIO);
            this.tabControlMain.Controls.Add(this.tabPageTempControl);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 24);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(923, 510);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageIO
            // 
            this.tabPageIO.Controls.Add(this.tempSensorArrayControl);
            this.tabPageIO.Controls.Add(this.signalGeneratorControl);
            this.tabPageIO.Location = new System.Drawing.Point(4, 22);
            this.tabPageIO.Name = "tabPageIO";
            this.tabPageIO.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIO.Size = new System.Drawing.Size(915, 484);
            this.tabPageIO.TabIndex = 0;
            this.tabPageIO.Text = "IO";
            this.tabPageIO.UseVisualStyleBackColor = true;
            // 
            // tabPageTempControl
            // 
            this.tabPageTempControl.Controls.Add(this.tempLoopControl);
            this.tabPageTempControl.Location = new System.Drawing.Point(4, 22);
            this.tabPageTempControl.Name = "tabPageTempControl";
            this.tabPageTempControl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTempControl.Size = new System.Drawing.Size(915, 484);
            this.tabPageTempControl.TabIndex = 1;
            this.tabPageTempControl.Text = "Temp. Control";
            this.tabPageTempControl.UseVisualStyleBackColor = true;
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
            // tempSensorArrayControl
            // 
            this.tempSensorArrayControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tempSensorArrayControl.Location = new System.Drawing.Point(430, 7);
            this.tempSensorArrayControl.MaxTemp = 30D;
            this.tempSensorArrayControl.MinTemp = 10D;
            this.tempSensorArrayControl.Name = "tempSensorArrayControl";
            this.tempSensorArrayControl.Size = new System.Drawing.Size(100, 429);
            this.tempSensorArrayControl.TabIndex = 2;
            this.tempSensorArrayControl.Temperatures = new double[] {
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN,
        double.NaN};
            // 
            // signalGeneratorControl
            // 
            this.signalGeneratorControl.AO_Amplitude = 0D;
            this.signalGeneratorControl.AO_Iterations = 1;
            this.signalGeneratorControl.AO_ON = false;
            this.signalGeneratorControl.AO_Period = 0;
            this.signalGeneratorControl.AO_Type = LSS_Host_Module.UI.SignalGeneratorControl.AOTypeEnum.Voltage;
            this.signalGeneratorControl.AO_WaveType = -1;
            this.signalGeneratorControl.Location = new System.Drawing.Point(7, 7);
            this.signalGeneratorControl.Name = "signalGeneratorControl";
            this.signalGeneratorControl.Size = new System.Drawing.Size(400, 315);
            this.signalGeneratorControl.TabIndex = 1;
            // 
            // tempLoopControl
            // 
            this.tempLoopControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tempLoopControl.Location = new System.Drawing.Point(9, 7);
            this.tempLoopControl.Name = "tempLoopControl";
            this.tempLoopControl.Size = new System.Drawing.Size(853, 381);
            this.tempLoopControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 534);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.Text = "LSS Host Controller";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControlMain.ResumeLayout(false);
            this.tabPageIO.ResumeLayout(false);
            this.tabPageTempControl.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
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
        private SignalGeneratorControl signalGeneratorControl;
        private TempSensorArrayControl tempSensorArrayControl;
        private System.Windows.Forms.TabPage tabPageTempControl;
        private TempLoopControl tempLoopControl;
    }
}

