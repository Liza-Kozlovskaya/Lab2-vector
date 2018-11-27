using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private static void PutPixel(Graphics g, Color col, int x, int y)
        {
            g.FillRectangle(new SolidBrush(col), x, y, 1, 1);
        }

        static public void BresenhamLine(Graphics g, Color clr1, Color clr2, int x1, int y1, int x2, int y2)
        {
            //изменение координат
            int dx = (x2 > x1) ? (x2 - x1) : (x1 - x2);
            int dy = (y2 > y1) ? (y2 - y1) : (y1 - y2);
            //направление приращения
            int sx = (x2 >= x1) ? (1) : (-1);
            int sy = (y2 >= y1) ? (1) : (-1);

            if (dy < dx)
            {
                int d = (dy << 1) - dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                var clr = ColorInterpolator.InterpolateBetween(clr1, clr2, 1);
                PutPixel(g, clr, x1, y1);
                int x = x1 + sx;
                int y = y1;

                for (double i = 1.0; i <= dx; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    double j = i / dx;
                    clr = ColorInterpolator.InterpolateBetween(clr1, clr2, i/dx);
                    PutPixel(g, clr, x, y);
                    x++;
                }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;
                var clr = ColorInterpolator.InterpolateBetween(clr1, clr2, 1);
                PutPixel(g, clr, x1, y1);
                int x = x1;
                int y = y1 + sy;

                for (double i = 1.0; i <= dy; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    double z = i / dy;
                    clr = ColorInterpolator.InterpolateBetween(clr1, clr2, i/dy);
                    PutPixel(g, clr, x, y);
                    y++;
                }
            }
        }

       public class ColorInterpolator
        {

            public static Color InterpolateBetween( Color endPoint1, Color endPoint2, double lambda)
            {
                if (lambda < 0 || lambda > 1)
                {
                    throw new ArgumentOutOfRangeException("lambda");
                }

                byte A = Clip(endPoint1.A * lambda + endPoint2.A * (1 - lambda));
                byte R = Clip(endPoint1.R * lambda + endPoint2.R * (1 - lambda));
                byte G = Clip(endPoint1.G * lambda + endPoint2.G * (1 - lambda));
                byte B = Clip(endPoint1.B * lambda + endPoint2.B * (1 - lambda));

                return Color.FromArgb(A, R, G, B);

            }

            private static byte Clip(double value)
            {
                return (byte)(value < 0 ? 0 : (value > 255 ? 255 : value));
            }
        }


            private void Timer1_Tick(object sender, EventArgs e)
        {
            //рандомные координаты точек
            Random r = new Random();
            int x1 = r.Next(750);
            int y1 = r.Next(450);
            int x2 = r.Next(750);
            int y2 = r.Next(450);

            //точки
            Point point1 = new Point(x1, y1);
            Point point2 = new Point(x2, y2);

            //рандомные цвета точек
            Color c = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
            Color n = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
            Graphics g = pictureBox1.CreateGraphics();
            BresenhamLine(g, c, n, x1, y1, x2, y2);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 150;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
    }
}
