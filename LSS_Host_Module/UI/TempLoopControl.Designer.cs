namespace LSS_Host_Module.UI
{
    partial class TempLoopControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.buttonStartTempControl = new System.Windows.Forms.Button();
            this.chartTempLoopProfile = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.propertyGridSettings = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.chartTempLoopProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStartTempControl
            // 
            this.buttonStartTempControl.Location = new System.Drawing.Point(3, 3);
            this.buttonStartTempControl.Name = "buttonStartTempControl";
            this.buttonStartTempControl.Size = new System.Drawing.Size(75, 23);
            this.buttonStartTempControl.TabIndex = 2;
            this.buttonStartTempControl.Text = "Start";
            this.buttonStartTempControl.UseVisualStyleBackColor = true;
            this.buttonStartTempControl.Click += new System.EventHandler(this.buttonStartTempControl_Click);
            // 
            // chartTempLoopProfile
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTempLoopProfile.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartTempLoopProfile.Legends.Add(legend1);
            this.chartTempLoopProfile.Location = new System.Drawing.Point(237, 30);
            this.chartTempLoopProfile.Name = "chartTempLoopProfile";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTempLoopProfile.Series.Add(series1);
            this.chartTempLoopProfile.Size = new System.Drawing.Size(601, 319);
            this.chartTempLoopProfile.TabIndex = 4;
            this.chartTempLoopProfile.Text = "chartTempLoopControl";
            // 
            // propertyGridSettings
            // 
            this.propertyGridSettings.Location = new System.Drawing.Point(4, 30);
            this.propertyGridSettings.Name = "propertyGridSettings";
            this.propertyGridSettings.Size = new System.Drawing.Size(227, 319);
            this.propertyGridSettings.TabIndex = 5;
            // 
            // TempLoopControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.propertyGridSettings);
            this.Controls.Add(this.chartTempLoopProfile);
            this.Controls.Add(this.buttonStartTempControl);
            this.Name = "TempLoopControl";
            this.Size = new System.Drawing.Size(853, 365);
            this.Load += new System.EventHandler(this.TempLoopControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartTempLoopProfile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStartTempControl;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTempLoopProfile;
        private System.Windows.Forms.PropertyGrid propertyGridSettings;
    }
}
