using System;

namespace MissionPlanner
{
    partial class MainV2
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
            Console.WriteLine("mainv2_Dispose");
            if (PluginThreadrunner != null)
                PluginThreadrunner.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainV2));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.CTX_mainmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.autoHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readonlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFlightData = new System.Windows.Forms.ToolStripButton();
            this.MenuFlightPlanner = new System.Windows.Forms.ToolStripButton();
            this.MenuInitConfig = new System.Windows.Forms.ToolStripButton();
            this.MenuConfigTune = new System.Windows.Forms.ToolStripButton();
            this.MenuSimulation = new System.Windows.Forms.ToolStripButton();
            this.MenuMap = new System.Windows.Forms.ToolStripButton();
            this.MenuTabletMirror = new System.Windows.Forms.ToolStripButton();
            this.MenuHelp = new System.Windows.Forms.ToolStripButton();
            this.MenuConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripConnectionControl = new MissionPlanner.Controls.ToolStripConnectionControl();
            this.MenuArduPilot = new System.Windows.Forms.ToolStripButton();
            this.menu = new MissionPlanner.Controls.MyButton();
            this.menu_AdvanceLock = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.status1 = new MissionPlanner.Controls.Status();
            this.MainMenu.SuspendLayout();
            this.CTX_mainmenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu - Modern Professional Aviation Style
            // 
            resources.ApplyResources(this.MainMenu, "MainMenu");
            this.MainMenu.ContextMenuStrip = this.CTX_mainmenu;
            this.MainMenu.GripMargin = new System.Windows.Forms.Padding(0);
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(45, 39);
            this.MainMenu.BackColor = System.Drawing.Color.FromArgb(30, 30, 46);
            this.MainMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.MainMenu.Renderer = new MissionPlanner.Controls.DIMPRenderer();
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFlightData,
            this.MenuFlightPlanner,
            this.MenuInitConfig,
            this.MenuConfigTune,
            this.MenuSimulation,
            this.MenuMap,
            this.MenuTabletMirror,
            this.MenuHelp,
            this.MenuConnect,
            this.toolStripConnectionControl,
            this.MenuArduPilot,
            this.menu_AdvanceLock});
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.ShowItemToolTips = true;
            this.MainMenu.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.MainMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MainMenu_ItemClicked);
            this.MainMenu.MouseLeave += new System.EventHandler(this.MainMenu_MouseLeave);
            // 
            // CTX_mainmenu
            // 
            this.CTX_mainmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoHideToolStripMenuItem,
            this.fullScreenToolStripMenuItem,
            this.readonlyToolStripMenuItem,
            this.connectionOptionsToolStripMenuItem,
            this.connectionListToolStripMenuItem});
            this.CTX_mainmenu.Name = "CTX_mainmenu";
            this.CTX_mainmenu.BackColor = System.Drawing.Color.FromArgb(30, 30, 46);
            this.CTX_mainmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.CTX_mainmenu, "CTX_mainmenu");
            // 
            // autoHideToolStripMenuItem
            // 
            this.autoHideToolStripMenuItem.CheckOnClick = true;
            this.autoHideToolStripMenuItem.Name = "autoHideToolStripMenuItem";
            this.autoHideToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.autoHideToolStripMenuItem, "autoHideToolStripMenuItem");
            this.autoHideToolStripMenuItem.Click += new System.EventHandler(this.autoHideToolStripMenuItem_Click);
            // 
            // fullScreenToolStripMenuItem
            // 
            this.fullScreenToolStripMenuItem.CheckOnClick = true;
            this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
            this.fullScreenToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.fullScreenToolStripMenuItem, "fullScreenToolStripMenuItem");
            this.fullScreenToolStripMenuItem.Click += new System.EventHandler(this.fullScreenToolStripMenuItem_Click);
            // 
            // readonlyToolStripMenuItem
            // 
            this.readonlyToolStripMenuItem.CheckOnClick = true;
            this.readonlyToolStripMenuItem.Name = "readonlyToolStripMenuItem";
            this.readonlyToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.readonlyToolStripMenuItem, "readonlyToolStripMenuItem");
            this.readonlyToolStripMenuItem.Click += new System.EventHandler(this.readonlyToolStripMenuItem_Click);
            // 
            // connectionOptionsToolStripMenuItem
            // 
            this.connectionOptionsToolStripMenuItem.Name = "connectionOptionsToolStripMenuItem";
            this.connectionOptionsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.connectionOptionsToolStripMenuItem, "connectionOptionsToolStripMenuItem");
            this.connectionOptionsToolStripMenuItem.Click += new System.EventHandler(this.connectionOptionsToolStripMenuItem_Click);
            // 
            // connectionListToolStripMenuItem
            // 
            this.connectionListToolStripMenuItem.Name = "connectionListToolStripMenuItem";
            this.connectionListToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            resources.ApplyResources(this.connectionListToolStripMenuItem, "connectionListToolStripMenuItem");
            this.connectionListToolStripMenuItem.Click += new System.EventHandler(this.connectionListToolStripMenuItem_Click);
            // 
            // MenuFlightData - Text Only Tab
            // 
            this.MenuFlightData.BackColor = System.Drawing.Color.Transparent;
            this.MenuFlightData.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.MenuFlightData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuFlightData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.MenuFlightData, "MenuFlightData");
            this.MenuFlightData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MenuFlightData.Name = "MenuFlightData";
            this.MenuFlightData.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.MenuFlightData.Click += new System.EventHandler(this.MenuFlightData_Click);
            // 
            // MenuFlightPlanner - Text Only Tab
            // 
            this.MenuFlightPlanner.BackColor = System.Drawing.Color.Transparent;
            this.MenuFlightPlanner.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.MenuFlightPlanner.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuFlightPlanner.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.MenuFlightPlanner, "MenuFlightPlanner");
            this.MenuFlightPlanner.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MenuFlightPlanner.Name = "MenuFlightPlanner";
            this.MenuFlightPlanner.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.MenuFlightPlanner.Click += new System.EventHandler(this.MenuFlightPlanner_Click);
            // 
            // MenuInitConfig - Text Only Tab
            // 
            this.MenuInitConfig.BackColor = System.Drawing.Color.Transparent;
            this.MenuInitConfig.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.MenuInitConfig.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuInitConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.MenuInitConfig, "MenuInitConfig");
            this.MenuInitConfig.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MenuInitConfig.Name = "MenuInitConfig";
            this.MenuInitConfig.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.MenuInitConfig.Click += new System.EventHandler(this.MenuSetup_Click);
            // 
            // MenuConfigTune - Text Only Tab
            // 
            this.MenuConfigTune.BackColor = System.Drawing.Color.Transparent;
            this.MenuConfigTune.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.MenuConfigTune.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuConfigTune.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.MenuConfigTune, "MenuConfigTune");
            this.MenuConfigTune.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MenuConfigTune.Name = "MenuConfigTune";
            this.MenuConfigTune.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.MenuConfigTune.Click += new System.EventHandler(this.MenuTuning_Click);
            // 
            // MenuSimulation - Text Only Tab
            // 
            this.MenuSimulation.BackColor = System.Drawing.Color.Transparent;
            this.MenuSimulation.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.MenuSimulation.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuSimulation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.MenuSimulation, "MenuSimulation");
            this.MenuSimulation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MenuSimulation.Name = "MenuSimulation";
            this.MenuSimulation.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.MenuSimulation.Click += new System.EventHandler(this.MenuSimulation_Click);
            // 
            // MenuMap - Real-time Map Button
            // 
            this.MenuMap.BackColor = System.Drawing.Color.Transparent;
            this.MenuMap.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.MenuMap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.MenuMap, "MenuMap");
            this.MenuMap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MenuMap.Name = "MenuMap";
            this.MenuMap.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.MenuMap.Click += new System.EventHandler(this.MenuMap_Click);
            // 
            // MenuTabletMirror - Android Tablet Mirror Button
            // 
            this.MenuTabletMirror.BackColor = System.Drawing.Color.Transparent;
            this.MenuTabletMirror.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.MenuTabletMirror.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuTabletMirror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.MenuTabletMirror, "MenuTabletMirror");
            this.MenuTabletMirror.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MenuTabletMirror.Name = "MenuTabletMirror";
            this.MenuTabletMirror.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.MenuTabletMirror.Click += new System.EventHandler(this.MenuTabletMirror_Click);
            // 
            // MenuHelp - Text Only Tab
            // 
            this.MenuHelp.BackColor = System.Drawing.Color.Transparent;
            this.MenuHelp.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.MenuHelp.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.MenuHelp, "MenuHelp");
            this.MenuHelp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.MenuHelp.Click += new System.EventHandler(this.MenuHelp_Click);
            // 
            // MenuConnect - Modern Connect Button
            // 
            this.MenuConnect.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MenuConnect.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.MenuConnect.ForeColor = System.Drawing.Color.White;
            this.MenuConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            resources.ApplyResources(this.MenuConnect, "MenuConnect");
            this.MenuConnect.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.MenuConnect.Name = "MenuConnect";
            this.MenuConnect.Padding = new System.Windows.Forms.Padding(16, 6, 16, 6);
            this.MenuConnect.Click += new System.EventHandler(this.MenuConnect_Click);
            // 
            // toolStripConnectionControl
            // 
            this.toolStripConnectionControl.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolStripConnectionControl, "toolStripConnectionControl");
            this.toolStripConnectionControl.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.toolStripConnectionControl.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
            this.toolStripConnectionControl.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.toolStripConnectionControl.Name = "toolStripConnectionControl";
            this.toolStripConnectionControl.MouseLeave += new System.EventHandler(this.MainMenu_MouseLeave);
            // 
            // MenuArduPilot - Logo Button
            // 
            this.MenuArduPilot.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.MenuArduPilot, "MenuArduPilot");
            this.MenuArduPilot.BackColor = System.Drawing.Color.Transparent;
            this.MenuArduPilot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuArduPilot.ForeColor = System.Drawing.Color.White;
            this.MenuArduPilot.Image = global::MissionPlanner.Properties.Resources.jiacdi_logo_toolbar;
            this.MenuArduPilot.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this.MenuArduPilot.Name = "MenuArduPilot";
            this.MenuArduPilot.Padding = new System.Windows.Forms.Padding(4);
            this.MenuArduPilot.ToolTipText = "Visit JIAC&DI Website";
            this.MenuArduPilot.Click += new System.EventHandler(this.MenuArduPilot_Click);
            // 
            // menu_AdvanceLock - Lock Button
            // 
            this.menu_AdvanceLock.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.menu_AdvanceLock, "menu_AdvanceLock");
            this.menu_AdvanceLock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menu_AdvanceLock.Image = global::MissionPlanner.Properties.Resources.lock_icon;
            this.menu_AdvanceLock.Name = "menu_AdvanceLock";
            this.menu_AdvanceLock.Padding = new System.Windows.Forms.Padding(4);
            this.menu_AdvanceLock.ToolTipText = "Lock/Unlock Setup and Config (Ctrl+L)";
            this.menu_AdvanceLock.Click += new System.EventHandler(this.menu_AdvanceLock_Click);
            // 
            // menu - Hamburger Menu Button
            // 
            resources.ApplyResources(this.menu, "menu");
            this.menu.Name = "menu";
            this.menu.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
            this.menu.FlatAppearance.BorderSize = 0;
            this.menu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.menu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.menu.UseVisualStyleBackColor = false;
            this.menu.MouseEnter += new System.EventHandler(this.menu_MouseEnter);
            // 
            // panel1 - Main Container Panel
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
            this.panel1.Controls.Add(this.status1);
            this.panel1.Controls.Add(this.MainMenu);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.panel1.MouseLeave += new System.EventHandler(this.MainMenu_MouseLeave);
            // 
            // status1 - Status Bar
            // 
            resources.ApplyResources(this.status1, "status1");
            this.status1.BackColor = System.Drawing.Color.FromArgb(20, 20, 36);
            this.status1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.status1.Name = "status1";
            this.status1.Percent = 0D;
            // 
            // MainV2 - Main Form
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);
            this.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menu);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainV2";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainV2_KeyDown);
            this.Resize += new System.EventHandler(this.MainV2_Resize);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.CTX_mainmenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ToolStripButton MenuFlightData;
        public System.Windows.Forms.ToolStripButton MenuFlightPlanner;
        public System.Windows.Forms.ToolStripButton MenuInitConfig;
        public System.Windows.Forms.ToolStripButton MenuSimulation;
        public System.Windows.Forms.ToolStripButton MenuMap;
        public System.Windows.Forms.ToolStripButton MenuConfigTune;
        public System.Windows.Forms.ToolStripButton MenuConnect;
        public System.Windows.Forms.ToolStripButton MenuTabletMirror;
        private Controls.ToolStripConnectionControl toolStripConnectionControl;
        private Controls.MyButton menu;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip CTX_mainmenu;
        private System.Windows.Forms.ToolStripMenuItem autoHideToolStripMenuItem;
        public System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fullScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readonlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionListToolStripMenuItem;
        public System.Windows.Forms.ToolStripButton MenuHelp;
        public System.Windows.Forms.ToolStripButton MenuArduPilot;
        public System.Windows.Forms.ToolStripButton menu_AdvanceLock;
        public Controls.Status status1;
    }
}