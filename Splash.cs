using System;
using System.Reflection;
using System.Windows.Forms;

namespace MissionPlanner
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();

            string strVersion = typeof(Splash).GetType().Assembly.GetName().Version.ToString();

            TXT_version.Text = "Version: " + Application.ProductVersion;

            Console.WriteLine(strVersion);

            // JIAC&DI branding - show Jordan flag and JIAC&DI logo
            try
            {
                if (pictureBox_logo != null)
                {
                    pictureBox_logo.Image = MissionPlanner.Properties.Resources.jiacdi_splash_logo;
                    pictureBox_logo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load splash logo: " + ex.Message);
            }

            try
            {
                if (pictureBox1 != null)
                {
                    pictureBox1.Image = MissionPlanner.Properties.Resources.jordan_flag;
                    pictureBox1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load Jordan flag: " + ex.Message);
            }

            Console.WriteLine("Splash .ctor - JIAC&DI branding loaded");
        }
    }
}