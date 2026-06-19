using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeSpan = System.TimeSpan;

namespace MissionPlanner.Controls
{
    public partial class Status : UserControl
    {
        private System.Threading.Timer _hidetimer;
        private double _percent = 50;
        public double Percent
        {
            get { return _percent;}
            set
            {
                if (value < 0 || value > 100)
                    return;

                _percent = value;
                this.BeginInvoke((Action) delegate { this.Visible = true; });
                _hidetimer.Change(TimeSpan.FromSeconds(10), TimeSpan.Zero);
                this.Invalidate();
            }
        }
        public Status()
        {
            InitializeComponent();

            CreateHandle();

            _hidetimer = new System.Threading.Timer(state =>
            {
                this.BeginInvoke((Action) delegate { this.Visible = false; });
            }, null, 1, -1);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_percent > 100)
                _percent = 50;

            try
            {
                // DIMP Professional Aviation Theme - Blue progress bar
                using (Brush bgBrush = new SolidBrush(Color.FromArgb(0x2A, 0x2A, 0x40)))
                {
                    e.Graphics.FillRectangle(bgBrush, 0, 0, Width, Height);
                }
                
                using (Brush progressBrush = new SolidBrush(Color.FromArgb(0x00, 0x7A, 0xCC)))
                {
                    e.Graphics.FillRectangle(progressBrush, 0, 0, (float)(Width * (_percent / 100.0)), Height);
                }
                
                // Subtle border
                using (Pen borderPen = new Pen(Color.FromArgb(0x3A, 0x3A, 0x56), 1))
                {
                    e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
                }
            }
            catch (OverflowException)
            {

            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // DIMP Theme - Dark background
            using (Brush bgBrush = new SolidBrush(Color.FromArgb(0x20, 0x20, 0x36)))
            {
                e.Graphics.FillRectangle(bgBrush, e.ClipRectangle);
            }
        }
    }
}
