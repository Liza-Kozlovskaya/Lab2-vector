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
    public partial class Form4 : Form
    {
        private Timer timer = new Timer();

        public Form4()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Interval = 5000;
            timer.Enabled = true;

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();

            Random rnd = new Random();


            int angleCount = rnd.Next(3, 20);
            List<int> xs = new List<int>();
            for (int i = 0; i < angleCount; i++)
            {
                xs.Add(rnd.Next(0, pictureBox1.Width - 1));
            }

            xs = xs.OrderBy(x => x).ToList();

            int firstX = xs[0];
            int firstY = rnd.Next(0, pictureBox1.Height - 1);
            int previousX = firstX;
            int previousY = firstY;

            for (int i = 1; i < xs.Count; i++)
            {

                int y = rnd.Next(0, firstY);
                //DrawSegment(previousX, xs[i], previousY, y, Color.Black);
                BresenhamLine(g, Color.Black, previousX, xs[i], previousY, y);
                previousX = xs[i];
                previousY = y;
            }
            var lastY = previousY;
            previousX = firstX;
            previousY = firstY;
            for (int i = 0; i < xs.Count; i++)
            {

                int y = rnd.Next(firstY, pictureBox1.Height - 1);
                //DrawSegment(previousX, xs[i], previousY, y, Color.Black);
                BresenhamLine(g, Color.Black, previousX, xs[i], previousY, y);
                previousX = xs[i];
                previousY = y;
            }
            //DrawSegment(xs[xs.Count - 1], xs[xs.Count - 1], lastY, previousY, Color.Black);
            BresenhamLine(g, Color.Black, xs[xs.Count - 1], xs[xs.Count - 1], lastY, previousY);

        }



        private static void PuPixel(Graphics g, Color col, int x, int y, int alpha)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, col)), x, y, 1, 1);
        }

        static public void BresenhamLine(Graphics g, Color clr, int x1, int y1, int x2, int y2)
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
                PuPixel(g, clr, x1, y1, 255);
                int x = x1 + sx;
                int y = y1;

                for (int i = 1; i <= dx; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    PuPixel(g, clr, x, y, 255);
                    x++;
                }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;
                PuPixel(g, clr, x1, y1, 255);
                int x = x1;
                int y = y1 + sy;

                for (int i = 1; i <= dy; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    PuPixel(g, clr, x, y, 255);
                    y++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer.Stop();
        }
    }
 }
