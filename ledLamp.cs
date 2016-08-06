using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace zap
{
    class LedLamp : UserControl
    {
        private Color black = Color.Black;
        public Color Black
        {
            get { return black; }
        }
        private Color light = Color.YellowGreen;
        //private System.ComponentModel.IContainer components;

        public Color Light
        {
            get { return light; }
        }
        private int status = 0;
        public int Status
        {
            get { return status; }
            set { status = value;
                Invalidate();
            }
        }
        public Color MyColor
        {
            get {
                if (status == 0) return black;
                else return light;
            }
        }
        public LedLamp()
        {
            //Transparent
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            //SetStyle(ControlStyles.Opaque, true);

            Size = new Size(40,45);
        }
        
        public void ToogleColor()
        {
            if (status == 0)
                status = 1;
            else
                status = 0;
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            //int penWidth = 3;
            Pen pen = new Pen(Color.Gray, 3);

            int fontHeight = 10;
            Font font = new Font("Arial", fontHeight);

            SolidBrush brush = new SolidBrush(MyColor);
            graphics.FillEllipse(brush, 5, 20, 20, 20);
            SolidBrush textBrush = new SolidBrush(Color.Blue);
            graphics.DrawEllipse(pen, 5, 20, 20, 20);
            graphics.DrawString(Text, font, textBrush, 0, 0);
        }
        protected override void OnClick(EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                ContextMenu deleteMenu = new ContextMenu();
                MenuItem deleteItem = new MenuItem("Xoá...", new System.EventHandler(this.onDeleteMenuItem_Click));
                deleteMenu.MenuItems.Add(deleteItem);
                deleteMenu.Show(this, new Point(me.X, me.Y));
            }
            else
            {
                this.ToogleColor();
            }
            
            base.OnClick(e);
        }
        private void onDeleteMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose(false);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LedLamp
            // 
            this.Name = "LedLamp";
            this.Size = new System.Drawing.Size(20, 20);
            this.ResumeLayout(false);

        }
    }
}
