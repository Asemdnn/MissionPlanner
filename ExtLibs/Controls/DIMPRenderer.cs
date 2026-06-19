using System;
using System.Drawing;
using System.Windows.Forms;

namespace MissionPlanner.Controls
{
    /// <summary>
    /// Professional DIMP Aviation Theme Renderer
    /// Provides modern, clean styling for all toolstrip and menu controls
    /// </summary>
    public class DIMPRenderer : ToolStripProfessionalRenderer
    {
        // DIMP Professional Aviation Color Palette
        private static readonly Color BackgroundColor = Color.FromArgb(30, 30, 46);
        private static readonly Color AccentColor = Color.FromArgb(0, 122, 204);
        private static readonly Color AccentHoverColor = Color.FromArgb(0, 142, 232);
        private static readonly Color TextColor = Color.FromArgb(220, 220, 230);
        private static readonly Color TextSecondaryColor = Color.FromArgb(180, 180, 190);
        private static readonly Color BorderColor = Color.FromArgb(50, 50, 70);
        private static readonly Color SeparatorColor = Color.FromArgb(60, 60, 80);

        public DIMPRenderer()
        {
            // Enable rounded borders
            this.RoundedEdges = true;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            // Use solid background color
            using (SolidBrush brush = new SolidBrush(BackgroundColor))
            {
                e.Graphics.FillRectangle(brush, e.ToolStrip.ClientRectangle);
            }
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripButton button = e.Item as ToolStripButton;
            if (button == null)
            {
                base.OnRenderButtonBackground(e);
                return;
            }

            Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);

            // Determine button state
            bool isSelected = button.Selected || button.Checked;
            bool isPressed = button.Pressed;

            // Choose colors based on state
            Color backColor;
            if (isPressed)
            {
                backColor = AccentHoverColor;
            }
            else if (isSelected)
            {
                backColor = Color.FromArgb(0, 100, 170);
            }
            else
            {
                backColor = Color.Transparent;
            }

            // Draw background
            if (backColor != Color.Transparent)
            {
                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(brush, bounds);
                }

                // Draw subtle border for selected items
                if (isSelected && !isPressed)
                {
                    using (Pen pen = new Pen(AccentColor, 1))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 0, bounds.Width - 1, bounds.Height - 1);
                    }
                }
            }

            // Draw underline for active tab
            if (button.Selected)
            {
                using (Pen pen = new Pen(AccentColor, 2))
                {
                    e.Graphics.DrawLine(pen, 4, bounds.Height - 2, bounds.Width - 4, bounds.Height - 2);
                }
            }
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            // Custom checkbox rendering - only for ToolStripMenuItem which has Checked property
            if (e.Item is ToolStripMenuItem menuItem && menuItem.Checked)
            {
                Rectangle bounds = e.ImageRectangle;
                using (SolidBrush brush = new SolidBrush(AccentColor))
                {
                    e.Graphics.FillRectangle(brush, bounds);
                }
                // Draw checkmark
                using (Pen pen = new Pen(Color.White, 2))
                {
                    e.Graphics.DrawLine(pen, bounds.X + 3, bounds.Y + bounds.Height / 2, 
                        bounds.X + bounds.Width / 2 - 1, bounds.Y + bounds.Height - 3);
                    e.Graphics.DrawLine(pen, bounds.X + bounds.Width / 2 - 1, bounds.Y + bounds.Height - 3,
                        bounds.X + bounds.Width - 3, bounds.Y + 3);
                }
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            // Draw custom separator
            using (Pen pen = new Pen(SeparatorColor, 1))
            {
                e.Graphics.DrawLine(pen, 5, e.Item.Height / 2, e.Item.Width - 5, e.Item.Height / 2);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // Draw subtle bottom border for menu strip
            if (e.ToolStrip is MenuStrip)
            {
                using (Pen pen = new Pen(BorderColor, 1))
                {
                    e.Graphics.DrawLine(pen, 0, e.ToolStrip.Height - 1, e.ToolStrip.Width, e.ToolStrip.Height - 1);
                }
            }
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripDropDownButton button = e.Item as ToolStripDropDownButton;
            if (button == null)
            {
                base.OnRenderDropDownButtonBackground(e);
                return;
            }

            Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
            Color backColor = button.Selected ? Color.FromArgb(0, 100, 170) : Color.Transparent;

            if (backColor != Color.Transparent)
            {
                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(brush, bounds);
                }
            }
        }

        protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(BackgroundColor))
            {
                e.Graphics.FillRectangle(brush, e.ToolStripPanel.ClientRectangle);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            // Use custom text rendering with better visibility
            Color textColor;
            
            if (e.Item.Enabled)
            {
                if (e.Item.Selected || e.Item.Pressed)
                {
                    textColor = Color.White;
                }
                else
                {
                    textColor = TextColor;
                }
            }
            else
            {
                textColor = TextSecondaryColor;
            }

            TextRenderer.DrawText(e.Graphics, e.Item.Text, e.Item.Font, 
                e.TextRectangle, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}