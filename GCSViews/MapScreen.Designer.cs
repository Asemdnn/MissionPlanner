namespace MissionPlanner.GCSViews
{
    partial class MapScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapScreen));
            this.mapPanel = new System.Windows.Forms.Panel();
            this.mapOverlay = new GMap.NET.WindowsForms.GMapOverlay("mapoverlay");
            this.mapControl = new MissionPlanner.Controls.myGMAP();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.lblLat = new System.Windows.Forms.Label();
            this.lblLng = new System.Windows.Forms.Label();
            this.lblAlt = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.mapPanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapPanel - Map Container Panel
            // 
            this.mapPanel.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
            this.mapPanel.Controls.Add(this.mapControl);
            resources.ApplyResources(this.mapPanel, "mapPanel");
            this.mapPanel.Name = "mapPanel";
            // 
            // mapControl - GMap Control
            // 
            this.mapControl.CanDragMap = true;
            this.mapControl.GrayScaleMode = false;
            this.mapControl.Location = new System.Drawing.Point(0, 0);
            this.mapControl.MarkersEnabled = true;
            this.mapControl.MaxZoom = 18;
            this.mapControl.MinZoom = 2;
            this.mapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mapControl.Name = "mapControl";
            this.mapControl.NegativeMode = false;
            this.mapControl.PolygonsEnabled = true;
            this.mapControl.RetryLoadTile = 0;
            this.mapControl.RoutesEnabled = true;
            this.mapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(33, 65, 105, 225);
            this.mapControl.ShowTileGridLines = false;
            this.mapControl.Size = new System.Drawing.Size(1200, 700);
            this.mapControl.TabIndex = 0;
            this.mapControl.Zoom = 10D;
            this.mapControl.OnPositionChanged += new GMap.NET.PositionChanged(this.mapControl_OnPositionChanged);
            // 
            // statusPanel - Status Bar Panel
            // 
            this.statusPanel.BackColor = System.Drawing.Color.FromArgb(20, 20, 36);
            this.statusPanel.Controls.Add(this.lblStatus);
            this.statusPanel.Controls.Add(this.lblHeading);
            this.statusPanel.Controls.Add(this.lblSpeed);
            this.statusPanel.Controls.Add(this.lblAlt);
            this.statusPanel.Controls.Add(this.lblLng);
            this.statusPanel.Controls.Add(this.lblLat);
            resources.ApplyResources(this.statusPanel, "statusPanel");
            this.statusPanel.Name = "statusPanel";
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.lblLat, "lblLat");
            this.lblLat.Name = "lblLat";
            // 
            // lblLng
            // 
            this.lblLng.AutoSize = true;
            this.lblLng.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.lblLng, "lblLng");
            this.lblLng.Name = "lblLng";
            // 
            // lblAlt
            // 
            this.lblAlt.AutoSize = true;
            this.lblAlt.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.lblAlt, "lblAlt");
            this.lblAlt.Name = "lblAlt";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.lblSpeed, "lblSpeed");
            this.lblSpeed.Name = "lblSpeed";
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.lblHeading, "lblHeading");
            this.lblHeading.Name = "lblHeading";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(0, 200, 100);
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Name = "lblStatus";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 100;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // MapScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
            this.Controls.Add(this.statusPanel);
            this.Controls.Add(this.mapPanel);
            resources.ApplyResources(this, "$this");
            this.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.Name = "MapScreen";
            this.mapPanel.ResumeLayout(false);
            this.statusPanel.ResumeLayout(false);
            this.statusPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mapPanel;
        private MissionPlanner.Controls.myGMAP mapControl;
        private GMap.NET.WindowsForms.GMapOverlay mapOverlay;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.Label lblLng;
        private System.Windows.Forms.Label lblAlt;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer refreshTimer;
    }
}
