using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form5 : Form
    {
        private int f = 90;
        private Timer timer = new Timer();

        public Form5()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 50;
            timer1.Enabled = true;

        }

        //Метод, устанавливающий пиксел на форме с заданными цветом и прозрачностью
        private static void PutPixel(Graphics g, Color col, int x, int y, int alpha)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, col)), x, y, 1, 1);
        }

        //Статический метод, реализующий отрисовку линии
        static public void BresenhamLine(Graphics g, Color clr, int x0, int y0,
                                                                 int x1, int y1)
        {
            //Изменения координат
            int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
            int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
            //Направление приращения
            int sx = (x1 >= x0) ? (1) : (-1);
            int sy = (y1 >= y0) ? (1) : (-1);

            if (dy < dx)
            {
                int d = (dy << 1) - dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                PutPixel(g, clr, x0, y0, 255);
                int x = x0 + sx;
                int y = y0;
                for (int i = 1; i <= dx; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 255);
                    x += sx;
                }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;
                PutPixel(g, clr, x0, y0, 255);
                int x = x0;
                int y = y0 + sy;
                for (int i = 1; i <= dy; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 255);
                    y += sy;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            f++;
            this.Refresh();
        }

        static double ToRadians(int degrees)
        {
            return degrees / 180.0 * Math.PI;
        }

        private void Form5_Paint(object sender, PaintEventArgs e)
        {
            int r1 = 100;
            Point point1 = new Point
            {
                X = (int)(200 + (r1 * Math.Sin(ToRadians(f)))),
                Y = (int)(200 + (r1 * Math.Cos(ToRadians(f))))
            };

            Point point2 = new Point
            {
                X = (int)(200 + r1 * Math.Sin(ToRadians(f + 180))),
                Y = (int)(200 + r1 * Math.Cos(ToRadians(f + 180)))
            };

            BresenhamLine(e.Graphics, Color.Red, point1.X, point1.Y, point2.X, point2.Y);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
        }
    }
}
