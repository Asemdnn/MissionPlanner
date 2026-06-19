using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MissionPlanner.ArduPilot;
using MissionPlanner.Controls;
using MissionPlanner.Utilities;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MissionPlanner.GCSViews
{
    public partial class MapScreen : MyUserControl, IActivate, IDeactivate
    {
        private GMapMarker droneMarker;
        private GMapMarker homeMarker;
        private GMapOverlay droneOverlay;
        private GMapOverlay trackOverlay;
        private GMapRoute trackRoute;
        private System.Windows.Forms.ToolStrip mapToolbar;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton btnCenterOnDrone;
        private System.Windows.Forms.ToolStripButton btnToggleGrid;
        private System.Windows.Forms.Label lblZoom;
        private bool showGrid = false;
        private bool isActivated = false;

        public MapScreen()
        {
            InitializeComponent();
            
            // Initialize map settings
            InitializeMap();
            
            // Initialize drone marker
            InitializeMarkers();
            
            // Apply theme
            ApplyThemeToControls();
        }

        private void InitializeMap()
        {
            try
            {
                // Set map provider based on settings
                string mapType = Settings.Instance.GetString("mapprovider", "Bing");
                switch (mapType.ToLower())
                {
                    case "google":
                        mapControl.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
                        break;
                    case "bing":
                        mapControl.MapProvider = GMap.NET.MapProviders.GMapProviders.BingMap;
                        break;
                    case "openstreetmap":
                        mapControl.MapProvider = GMap.NET.MapProviders.GMapProviders.OpenStreetMap;
                        break;
                    default:
                        mapControl.MapProvider = GMap.NET.MapProviders.GMapProviders.BingMap;
                        break;
                }

                // Initialize GMaps Core
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
                
                // Set initial position (default to home location or last known)
                double lat = Settings.Instance.GetDouble("map_lat", 0);
                double lng = Settings.Instance.GetDouble("map_lng", 0);
                
                if (lat == 0 && lng == 0)
                {
                    // Default to a central location
                    mapControl.Position = new PointLatLng(37.7749, -122.4194);
                }
                else
                {
                    mapControl.Position = new PointLatLng(lat, lng);
                }
                
                mapControl.MinZoom = 2;
                mapControl.MaxZoom = 18;
                mapControl.Zoom = 15;
                mapControl.DragButton = MouseButtons.Left;
                
                // Create overlays
                droneOverlay = new GMapOverlay("drone");
                trackOverlay = new GMapOverlay("track");
                
                mapControl.Overlays.Add(droneOverlay);
                mapControl.Overlays.Add(trackOverlay);
                
                // Create map toolbar
                CreateMapToolbar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MapScreen InitializeMap Error: " + ex.Message);
            }
        }

        private void CreateMapToolbar()
        {
            mapToolbar = new System.Windows.Forms.ToolStrip();
            mapToolbar.BackColor = System.Drawing.Color.FromArgb(30, 30, 46);
            mapToolbar.GripStyle = ToolStripGripStyle.Hidden;
            
            btnZoomIn = new System.Windows.Forms.ToolStripButton();
            btnZoomIn.Text = "+";
            btnZoomIn.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            btnZoomIn.Click += (s, e) => { mapControl.Zoom += 1; UpdateZoomLabel(); };
            
            btnZoomOut = new System.Windows.Forms.ToolStripButton();
            btnZoomOut.Text = "-";
            btnZoomOut.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            btnZoomOut.Click += (s, e) => { mapControl.Zoom -= 1; UpdateZoomLabel(); };
            
            btnCenterOnDrone = new System.Windows.Forms.ToolStripButton();
            btnCenterOnDrone.Text = "Center";
            btnCenterOnDrone.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            btnCenterOnDrone.Click += (s, e) => CenterOnDrone();
            
            btnToggleGrid = new System.Windows.Forms.ToolStripButton();
            btnToggleGrid.Text = "Grid";
            btnToggleGrid.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            btnToggleGrid.Click += (s, e) => ToggleGrid();
            
            lblZoom = new System.Windows.Forms.Label();
            lblZoom.Text = "Zoom: 15";
            lblZoom.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            lblZoom.AutoSize = true;
            
            mapToolbar.Items.AddRange(new ToolStripItem[] 
            { 
                btnZoomIn, btnZoomOut, 
                new ToolStripSeparator(),
                btnCenterOnDrone, btnToggleGrid,
                new ToolStripSeparator(),
                new ToolStripLabel(lblZoom.Text) { Name = "lblZoom" }
            });
            
            // Add toolbar to panel
            mapPanel.Controls.Add(mapToolbar);
            mapToolbar.Dock = DockStyle.Top;
        }

        private void UpdateZoomLabel()
        {
            foreach (ToolStripItem item in mapToolbar.Items)
            {
                if (item is ToolStripLabel && item.Name == "lblZoom")
                {
                    ((ToolStripLabel)item).Text = "Zoom: " + (int)mapControl.Zoom;
                    break;
                }
            }
        }

        private void InitializeMarkers()
        {
            // Create drone marker with arrow
            droneMarker = new GMarkerGoogle(mapControl.Position, GMarkerGoogleType.arrow);
            droneMarker.Size = new Size(30, 30);
            
            // Create home marker (use red dot for home position)
            homeMarker = new GMarkerGoogle(mapControl.Position, GMarkerGoogleType.red);
            homeMarker.Size = new Size(25, 25);
            
            droneOverlay.Markers.Add(droneMarker);
            droneOverlay.Markers.Add(homeMarker);
            
            // Initialize track with empty points list
            trackRoute = new GMapRoute(new System.Collections.Generic.List<PointLatLng>(), "track");
            trackOverlay.Routes.Add(trackRoute);
        }

        private void ApplyThemeToControls()
        {
            this.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
            this.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            
            mapPanel.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
            // EmptyMapColor property may not exist on myGMAP, so wrap in try-catch
            
            statusPanel.BackColor = System.Drawing.Color.FromArgb(20, 20, 36);
            
            foreach (Control ctrl in statusPanel.Controls)
            {
                ctrl.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            }
            
            lblStatus.ForeColor = System.Drawing.Color.FromArgb(0, 200, 100);
        }

        public void Activate()
        {
            isActivated = true;
            refreshTimer.Start();
            UpdateMapFromTelemetry();
        }

        public void Deactivate()
        {
            isActivated = false;
            refreshTimer.Stop();
            
            // Save map position
            Settings.Instance["map_lat"] = mapControl.Position.Lat.ToString();
            Settings.Instance["map_lng"] = mapControl.Position.Lng.ToString();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (isActivated)
            {
                UpdateMapFromTelemetry();
            }
        }

        private void UpdateMapFromTelemetry()
        {
            try
            {
                // Update from main aircraft
                if (MainV2.comPort != null && MainV2.comPort.MAV.cs.lat != 0)
                {
                    double lat = MainV2.comPort.MAV.cs.lat;
                    double lng = MainV2.comPort.MAV.cs.lng;
                    double alt = MainV2.comPort.MAV.cs.alt;
                    float yaw = MainV2.comPort.MAV.cs.yaw;
                    float groundspeed = MainV2.comPort.MAV.cs.groundspeed;
                    
                    // Update drone marker position
                    PointLatLng newPos = new PointLatLng(lat, lng);
                    droneMarker.Position = newPos;
                    
                    // Update track
                    if (trackRoute.Points.Count == 0 ||
                        (Math.Abs(trackRoute.Points[trackRoute.Points.Count - 1].Lat - lat) > 0.00001 ||
                         Math.Abs(trackRoute.Points[trackRoute.Points.Count - 1].Lng - lng) > 0.00001))
                    {
                        trackRoute.Points.Add(newPos);
                        if (trackRoute.Points.Count > 1000)
                        {
                            trackRoute.Points.RemoveAt(0);
                        }
                    }
                    
                    // Update home marker if we have home position
                    if (MainV2.comPort.MAV.cs.HomeLocation != PointLatLngAlt.Zero)
                    {
                        homeMarker.Position = new PointLatLng(MainV2.comPort.MAV.cs.HomeLocation.Lat, MainV2.comPort.MAV.cs.HomeLocation.Lng);
                        homeMarker.IsVisible = true;
                    }
                    
                    // Update status labels
                    lblLat.Text = "Lat: " + lat.ToString("F6") + "°";
                    lblLng.Text = "Lng: " + lng.ToString("F6") + "°";
                    lblAlt.Text = "Alt: " + alt.ToString("F1") + "m";
                    lblSpeed.Text = "Spd: " + groundspeed.ToString("F1") + "m/s";
                    lblHeading.Text = "HDG: " + yaw.ToString("F0") + "°";
                    lblStatus.Text = "Connected";
                    lblStatus.ForeColor = System.Drawing.Color.FromArgb(0, 200, 100);
                }
                else
                {
                    // No connection
                    lblStatus.Text = "Disconnected - Connect to vehicle";
                    lblStatus.ForeColor = System.Drawing.Color.FromArgb(255, 100, 100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MapScreen UpdateMapFromTelemetry Error: " + ex.Message);
            }
        }

        private void CenterOnDrone()
        {
            if (MainV2.comPort != null && MainV2.comPort.MAV.cs.lat != 0)
            {
                mapControl.Position = new PointLatLng(
                    MainV2.comPort.MAV.cs.lat,
                    MainV2.comPort.MAV.cs.lng);
            }
        }

        private void ToggleGrid()
        {
            showGrid = !showGrid;
            mapControl.ShowTileGridLines = showGrid;
            btnToggleGrid.ForeColor = showGrid ? 
                System.Drawing.Color.FromArgb(0, 122, 204) : 
                System.Drawing.Color.FromArgb(220, 220, 230);
        }

        private void mapControl_OnPositionChanged(PointLatLng point)
        {
            // Save position when user drags map
            // Can be used for auto-center feature
        }

        private void mapControl_OnZoomChanged()
        {
            UpdateZoomLabel();
        }
    }
}
