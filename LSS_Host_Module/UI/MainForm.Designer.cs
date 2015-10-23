namespace LSS_Host_Module.UI
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
            this.gbAO = new System.Windows.Forms.GroupBox();
            this.checkBoxStartStop = new System.Windows.Forms.CheckBox();
            this.labelWaveType = new System.Windows.Forms.Label();
            this.radioButtonWaveType_Saw3 = new System.Windows.Forms.RadioButton();
            this.radioButtonWaveType_Saw2 = new System.Windows.Forms.RadioButton();
            this.radioButtonWaveType_Saw1 = new System.Windows.Forms.RadioButton();
            this.radioButtonWaveType_Square = new System.Windows.Forms.RadioButton();
            this.radioButtonWaveType_DC = new System.Windows.Forms.RadioButton();
            this.labelAOIterations = new System.Windows.Forms.Label();
            this.labelAOIterationsValue = new System.Windows.Forms.Label();
            this.trackBarAOIterations = new System.Windows.Forms.TrackBar();
            this.labelAOPeriod = new System.Windows.Forms.Label();
            this.labelAOPeriodValue = new System.Windows.Forms.Label();
            this.trackBarAOPeriod = new System.Windows.Forms.TrackBar();
            this.labelAOValueName = new System.Windows.Forms.Label();
            this.labelAOValue = new System.Windows.Forms.Label();
            this.trackBarAOAmplitude = new System.Windows.Forms.TrackBar();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain.SuspendLayout();
            this.tabPageIO.SuspendLayout();
            this.gbAO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAOIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAOPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAOAmplitude)).BeginInit();
            this.menuStripMain.SuspendLayout();
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
            // gbAO
            // 
            this.gbAO.Controls.Add(this.checkBoxStartStop);
            this.gbAO.Controls.Add(this.labelWaveType);
            this.gbAO.Controls.Add(this.radioButtonWaveType_Saw3);
            this.gbAO.Controls.Add(this.radioButtonWaveType_Saw2);
            this.gbAO.Controls.Add(this.radioButtonWaveType_Saw1);
            this.gbAO.Controls.Add(this.radioButtonWaveType_Square);
            this.gbAO.Controls.Add(this.radioButtonWaveType_DC);
            this.gbAO.Controls.Add(this.labelAOIterations);
            this.gbAO.Controls.Add(this.labelAOIterationsValue);
            this.gbAO.Controls.Add(this.trackBarAOIterations);
            this.gbAO.Controls.Add(this.labelAOPeriod);
            this.gbAO.Controls.Add(this.labelAOPeriodValue);
            this.gbAO.Controls.Add(this.trackBarAOPeriod);
            this.gbAO.Controls.Add(this.labelAOValueName);
            this.gbAO.Controls.Add(this.labelAOValue);
            this.gbAO.Controls.Add(this.trackBarAOAmplitude);
            this.gbAO.Location = new System.Drawing.Point(7, 7);
            this.gbAO.Name = "gbAO";
            this.gbAO.Size = new System.Drawing.Size(401, 263);
            this.gbAO.TabIndex = 0;
            this.gbAO.TabStop = false;
            this.gbAO.Text = "Analog Output";
            // 
            // checkBoxStartStop
            // 
            this.checkBoxStartStop.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxStartStop.AutoSize = true;
            this.checkBoxStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxStartStop.Location = new System.Drawing.Point(9, 210);
            this.checkBoxStartStop.MinimumSize = new System.Drawing.Size(60, 40);
            this.checkBoxStartStop.Name = "checkBoxStartStop";
            this.checkBoxStartStop.Size = new System.Drawing.Size(60, 40);
            this.checkBoxStartStop.TabIndex = 21;
            this.checkBoxStartStop.Text = "Start";
            this.checkBoxStartStop.UseVisualStyleBackColor = true;
            this.checkBoxStartStop.CheckedChanged += new System.EventHandler(this.checkBoxStartStop_CheckedChanged);
            // 
            // labelWaveType
            // 
            this.labelWaveType.AutoSize = true;
            this.labelWaveType.Location = new System.Drawing.Point(6, 182);
            this.labelWaveType.Name = "labelWaveType";
            this.labelWaveType.Size = new System.Drawing.Size(36, 13);
            this.labelWaveType.TabIndex = 20;
            this.labelWaveType.Text = "Wave";
            // 
            // radioButtonWaveType_Saw3
            // 
            this.radioButtonWaveType_Saw3.AutoSize = true;
            this.radioButtonWaveType_Saw3.Image = global::LSS_Host_Module.Properties.Resources.Saw3WaveImage;
            this.radioButtonWaveType_Saw3.Location = new System.Drawing.Point(266, 172);
            this.radioButtonWaveType_Saw3.Name = "radioButtonWaveType_Saw3";
            this.radioButtonWaveType_Saw3.Size = new System.Drawing.Size(46, 32);
            this.radioButtonWaveType_Saw3.TabIndex = 19;
            this.radioButtonWaveType_Saw3.TabStop = true;
            this.radioButtonWaveType_Saw3.UseVisualStyleBackColor = true;
            // 
            // radioButtonWaveType_Saw2
            // 
            this.radioButtonWaveType_Saw2.AutoSize = true;
            this.radioButtonWaveType_Saw2.Image = global::LSS_Host_Module.Properties.Resources.Saw2WaveImage;
            this.radioButtonWaveType_Saw2.Location = new System.Drawing.Point(221, 172);
            this.radioButtonWaveType_Saw2.Name = "radioButtonWaveType_Saw2";
            this.radioButtonWaveType_Saw2.Size = new System.Drawing.Size(46, 32);
            this.radioButtonWaveType_Saw2.TabIndex = 18;
            this.radioButtonWaveType_Saw2.TabStop = true;
            this.radioButtonWaveType_Saw2.UseVisualStyleBackColor = true;
            // 
            // radioButtonWaveType_Saw1
            // 
            this.radioButtonWaveType_Saw1.AutoSize = true;
            this.radioButtonWaveType_Saw1.Image = global::LSS_Host_Module.Properties.Resources.Saw1WaveImage;
            this.radioButtonWaveType_Saw1.Location = new System.Drawing.Point(169, 172);
            this.radioButtonWaveType_Saw1.Name = "radioButtonWaveType_Saw1";
            this.radioButtonWaveType_Saw1.Size = new System.Drawing.Size(46, 32);
            this.radioButtonWaveType_Saw1.TabIndex = 17;
            this.radioButtonWaveType_Saw1.TabStop = true;
            this.radioButtonWaveType_Saw1.UseVisualStyleBackColor = true;
            // 
            // radioButtonWaveType_Square
            // 
            this.radioButtonWaveType_Square.AutoSize = true;
            this.radioButtonWaveType_Square.Image = global::LSS_Host_Module.Properties.Resources.SquareWaveImage;
            this.radioButtonWaveType_Square.Location = new System.Drawing.Point(117, 172);
            this.radioButtonWaveType_Square.Name = "radioButtonWaveType_Square";
            this.radioButtonWaveType_Square.Size = new System.Drawing.Size(46, 32);
            this.radioButtonWaveType_Square.TabIndex = 16;
            this.radioButtonWaveType_Square.TabStop = true;
            this.radioButtonWaveType_Square.UseVisualStyleBackColor = true;
            // 
            // radioButtonWaveType_DC
            // 
            this.radioButtonWaveType_DC.AutoSize = true;
            this.radioButtonWaveType_DC.Image = global::LSS_Host_Module.Properties.Resources.DCWaveImage;
            this.radioButtonWaveType_DC.Location = new System.Drawing.Point(65, 172);
            this.radioButtonWaveType_DC.Name = "radioButtonWaveType_DC";
            this.radioButtonWaveType_DC.Size = new System.Drawing.Size(46, 32);
            this.radioButtonWaveType_DC.TabIndex = 15;
            this.radioButtonWaveType_DC.TabStop = true;
            this.radioButtonWaveType_DC.UseVisualStyleBackColor = true;
            // 
            // labelAOIterations
            // 
            this.labelAOIterations.AutoSize = true;
            this.labelAOIterations.Location = new System.Drawing.Point(6, 131);
            this.labelAOIterations.Name = "labelAOIterations";
            this.labelAOIterations.Size = new System.Drawing.Size(50, 13);
            this.labelAOIterations.TabIndex = 8;
            this.labelAOIterations.Text = "Iterations";
            // 
            // labelAOIterationsValue
            // 
            this.labelAOIterationsValue.AutoSize = true;
            this.labelAOIterationsValue.Location = new System.Drawing.Point(318, 131);
            this.labelAOIterationsValue.Name = "labelAOIterationsValue";
            this.labelAOIterationsValue.Size = new System.Drawing.Size(13, 13);
            this.labelAOIterationsValue.TabIndex = 7;
            this.labelAOIterationsValue.Text = "0";
            // 
            // trackBarAOIterations
            // 
            this.trackBarAOIterations.Location = new System.Drawing.Point(65, 121);
            this.trackBarAOIterations.Maximum = 50;
            this.trackBarAOIterations.Minimum = 1;
            this.trackBarAOIterations.Name = "trackBarAOIterations";
            this.trackBarAOIterations.Size = new System.Drawing.Size(247, 45);
            this.trackBarAOIterations.TabIndex = 6;
            this.trackBarAOIterations.Value = 1;
            this.trackBarAOIterations.ValueChanged += new System.EventHandler(this.trackBarAOIterations_ValueChanged);
            // 
            // labelAOPeriod
            // 
            this.labelAOPeriod.AutoSize = true;
            this.labelAOPeriod.Location = new System.Drawing.Point(6, 80);
            this.labelAOPeriod.Name = "labelAOPeriod";
            this.labelAOPeriod.Size = new System.Drawing.Size(37, 13);
            this.labelAOPeriod.TabIndex = 5;
            this.labelAOPeriod.Text = "Period";
            // 
            // labelAOPeriodValue
            // 
            this.labelAOPeriodValue.AutoSize = true;
            this.labelAOPeriodValue.Location = new System.Drawing.Point(318, 80);
            this.labelAOPeriodValue.Name = "labelAOPeriodValue";
            this.labelAOPeriodValue.Size = new System.Drawing.Size(13, 13);
            this.labelAOPeriodValue.TabIndex = 4;
            this.labelAOPeriodValue.Text = "0";
            // 
            // trackBarAOPeriod
            // 
            this.trackBarAOPeriod.Location = new System.Drawing.Point(65, 70);
            this.trackBarAOPeriod.Maximum = 500;
            this.trackBarAOPeriod.Name = "trackBarAOPeriod";
            this.trackBarAOPeriod.Size = new System.Drawing.Size(247, 45);
            this.trackBarAOPeriod.TabIndex = 3;
            this.trackBarAOPeriod.ValueChanged += new System.EventHandler(this.trackBarAOPeriod_ValueChanged);
            // 
            // labelAOValueName
            // 
            this.labelAOValueName.AutoSize = true;
            this.labelAOValueName.Location = new System.Drawing.Point(6, 29);
            this.labelAOValueName.Name = "labelAOValueName";
            this.labelAOValueName.Size = new System.Drawing.Size(53, 13);
            this.labelAOValueName.TabIndex = 2;
            this.labelAOValueName.Text = "Amplitude";
            // 
            // labelAOValue
            // 
            this.labelAOValue.AutoSize = true;
            this.labelAOValue.Location = new System.Drawing.Point(318, 29);
            this.labelAOValue.Name = "labelAOValue";
            this.labelAOValue.Size = new System.Drawing.Size(13, 13);
            this.labelAOValue.TabIndex = 1;
            this.labelAOValue.Text = "0";
            // 
            // trackBarAOAmplitude
            // 
            this.trackBarAOAmplitude.Location = new System.Drawing.Point(65, 19);
            this.trackBarAOAmplitude.Maximum = 500;
            this.trackBarAOAmplitude.Name = "trackBarAOAmplitude";
            this.trackBarAOAmplitude.Size = new System.Drawing.Size(247, 45);
            this.trackBarAOAmplitude.TabIndex = 0;
            this.trackBarAOAmplitude.ValueChanged += new System.EventHandler(this.trackBarAO_ValueChanged);
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
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageIO.ResumeLayout(false);
            this.gbAO.ResumeLayout(false);
            this.gbAO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAOIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAOPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAOAmplitude)).EndInit();
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
        private System.Windows.Forms.GroupBox gbAO;
        private System.Windows.Forms.Label labelAOValue;
        private System.Windows.Forms.TrackBar trackBarAOAmplitude;
        private System.Windows.Forms.Label labelAOValueName;
        private System.Windows.Forms.Label labelAOPeriod;
        private System.Windows.Forms.Label labelAOPeriodValue;
        private System.Windows.Forms.TrackBar trackBarAOPeriod;
        private System.Windows.Forms.Label labelAOIterations;
        private System.Windows.Forms.Label labelAOIterationsValue;
        private System.Windows.Forms.TrackBar trackBarAOIterations;
        private System.Windows.Forms.RadioButton radioButtonWaveType_Square;
        private System.Windows.Forms.RadioButton radioButtonWaveType_DC;
        private System.Windows.Forms.RadioButton radioButtonWaveType_Saw1;
        private System.Windows.Forms.RadioButton radioButtonWaveType_Saw3;
        private System.Windows.Forms.RadioButton radioButtonWaveType_Saw2;
        private System.Windows.Forms.Label labelWaveType;
        private System.Windows.Forms.CheckBox checkBoxStartStop;
    }
}

