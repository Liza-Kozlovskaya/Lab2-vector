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
    public partial class Form2 : Form
    {
        private int f = 90;
        private int a = 90;
        private Timer timer = new Timer();
        private Timer tm = new Timer();

        int x = 250;
        int y = 200;
        int r = 120;
        int r1 = 30;

        public Form2()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer1_Tick);
            tm.Tick += new EventHandler(Timer2_Tick);
            timer1.Interval = 50;
            timer1.Enabled = true;
            timer2.Interval = 100;
            timer2.Enabled = true;
            this.DoubleBuffered = true;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ///////////////////////Круги///////////////////////////////
        /// </summary>
        private static void PutPixel(Graphics g, Color col, int x, int y, int alpha)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, col)), x, y, 1, 1);
        }

        public static void BresenhamCircle(Graphics g, Color clr, int _x, int _y, int radius)
        {
            int x = 0, y = radius, gap = 0, delta = (2 - 2 * radius);
            while (y >= 0)
            {
                PutPixel(g, clr, _x + x, _y + y, 255);
                PutPixel(g, clr, _x + x, _y - y, 255);
                PutPixel(g, clr, _x - x, _y - y, 255);
                PutPixel(g, clr, _x - x, _y + y, 255);
                gap = 2 * (delta + y) - 1;
                if (delta < 0 && gap <= 0)
                {
                    x++;
                    delta += 2 * x + 1;
                    continue;
                }
                if (delta > 0 && gap > 0)
                {
                    y--;
                    delta -= 2 * y + 1;
                    continue;
                }
                x++;
                delta += 2 * (x - y);
                y--;
            }
        }



        ///// <summary>
        ///// ///////////////////////Линия///////////////////////////////
        ///// </summary>
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

        public static void DrawWuLine(Graphics g, Color clr, int x0, int y0, int x1, int y1)
        {
            //Вычисление изменения координат
            int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
            int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
            //Если линия параллельна одной из осей, рисуем обычную линию - заполняем все пикселы в ряд
            if (dx == 0 || dy == 0)
            {
                g.DrawLine(new Pen(clr), x0, y0, x1, y1);
                return;
            }

            //Для Х-линии (коэффициент наклона < 1)
            if (dy < dx)
            {
                //Первая точка должна иметь меньшую координату Х
                if (x1 < x0)
                {
                    x1 += x0; x0 = x1 - x0; x1 -= x0;
                    y1 += y0; y0 = y1 - y0; y1 -= y0;
                }
                //Относительное изменение координаты Y
                float grad = (float)dy / dx;
                if (y1 < y0)
                {
                    grad = -grad;
                }
                //Промежуточная переменная для Y
                float intery = y0 + grad;
                //Первая точка
                PutPixel(g, clr, x0, y0, 255);

                for (int x = x0 + 1; x < x1; x++)
                {
                    //Верхняя точка
                    PutPixel(g, clr, x, IPart(intery), (int)(255 - FPart(intery) * 255));
                    //Нижняя точка
                    PutPixel(g, clr, x, IPart(intery) + 1, (int)(FPart(intery) * 255));
                    //Изменение координаты Y
                    intery += grad;
                }
                //Последняя точка
                PutPixel(g, clr, x1, y1, 255);
            }
            //Для Y-линии (коэффициент наклона > 1)
            else
            {
                //Первая точка должна иметь меньшую координату Y
                if (y1 < y0)
                {
                    x1 += x0; x0 = x1 - x0; x1 -= x0;
                    y1 += y0; y0 = y1 - y0; y1 -= y0;
                }
                //Относительное изменение координаты X
                float grad = (float)dx / dy;
                if (x1 < x0)
                {
                    grad = -grad;
                }
                //Промежуточная переменная для X
                float interx = x0 + grad;
                //Первая точка
                PutPixel(g, clr, x0, y0, 255);

                for (int y = y0 + 1; y < y1; y++)
                {
                    //Верхняя точка
                    PutPixel(g, clr, IPart(interx), y, 255 - (int)(FPart(interx) * 255));
                    //Нижняя точка
                    PutPixel(g, clr, IPart(interx) + 1, y, (int)(FPart(interx) * 255));
                    //Изменение координаты X
                    interx += grad;
                }
                //Последняя точка
                PutPixel(g, clr, x1, y1, 255);
            }
        }
        //Целая часть числа
        private static int IPart(float x)
        {
            return (int)x;
        }
        //дробная часть числа
        private static float FPart(float x)
        {
            while (x >= 0)
                x--;
            x++;
            return x;
        }

        /// <summary>
        /// ///////////////////////Таймер для круга///////////////////////////////
        /// </summary>
        public void Timer1_Tick(object sender, EventArgs e)
        {
            f++;
            this.Refresh();
        }

        /// <summary>
        /// ///////////////////////Таймер для линии///////////////////////////////
        /// </summary>
        public void Timer2_Tick(object sender, EventArgs e)
        {
            a--;
            this.Refresh();
        }


        static double ToRadians(int degrees)
        {
            return degrees / 180.0 * Math.PI;
        }

        /// <summary>
        /// ///////////////////////Старт///////////////////////////////
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();

            timer2.Enabled = true;
            timer2.Start();
        }


        /// <summary>
        /// ///////////////////////Стоп///////////////////////////////
        /// </summary>
        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();

            timer2.Enabled = false;
            timer2.Stop();

        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Point center = new Point
            {
                X = (int)(x + r * Math.Sin(ToRadians(f))),
                Y = (int)(y + r * Math.Cos(ToRadians(f)))
            };

            Point point1 = new Point
            {
                X = (int)(center.X + (r1 * Math.Sin(ToRadians(a)))),
                Y = (int)(center.Y + (r1 * Math.Cos(ToRadians(a))))
            };

            Point point2 = new Point
            {
                X = (int)(center.X + r1 * Math.Sin(ToRadians(a + 180))),
                Y = (int)(center.Y + r1 * Math.Cos(ToRadians(a + 180)))
            };

            BresenhamCircle(e.Graphics, Color.NavajoWhite, x, y, r);
            BresenhamCircle(e.Graphics, Color.NavajoWhite, center.X, center.Y, r1);
            //BresenhamLine(e.Graphics, Color.Red, point1.X, point1.Y, point2.X, point2.Y);
            DrawWuLine(e.Graphics, Color.Red, point1.X, point1.Y, point2.X, point2.Y);
            DrawWuLine(e.Graphics, Color.Red, x, y, center.X, center.Y);
            //e.Graphics.DrawLine(Pens.Black, point1.X, point1.Y, point2.X, point2.Y);
        }
    }
}
