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
    public partial class gradient : Form
    {
        public static Bitmap image;

        public gradient()
        {
            InitializeComponent();

            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Rasterization();
            pictureBox1.Image = image;
        }

        private void gradient_Load(object sender, EventArgs e)
        {

        }

        public void Rasterization()
        {
            for (int y = 0; y < pictureBox1.Height; y++)
                for (int x = 0; x < pictureBox1.Width; x++)
                    image.SetPixel(x, y, Color.FromArgb((int)ShadeBackgroundPixel(x, y)));
        }

        public UInt32 ShadeBackgroundPixel(int x, int y)
        {
            UInt32 pixelValue;

            double l1, l2, l3;
            int i;
            pixelValue = 0xFFFFFFFF;
            for (i = 0; i < data.n; i++)
            {
                l1 = ((data.y2[i] - data.y3[i]) * ((double)(x) - data.x3[i]) + (data.x3[i] - data.x2[i]) * ((double)(y) - data.y3[i])) /
                    ((data.y2[i] - data.y3[i]) * (data.x1[i] - data.x3[i]) + (data.x3[i] - data.x2[i]) * (data.y1[i] - data.y3[i]));
                l2 = ((data.y3[i] - data.y1[i]) * ((double)(x) - data.x3[i]) + (data.x1[i] - data.x3[i]) * ((double)(y) - data.y3[i])) /
                    ((data.y2[i] - data.y3[i]) * (data.x1[i] - data.x3[i]) + (data.x3[i] - data.x2[i]) * (data.y1[i] - data.y3[i]));
                l3 = 1 - l1 - l2;
                if (l1 >= 0 && l1 <= 1 && l2 >= 0 && l2 <= 1 && l3 >= 0 && l3 <= 1)
                {
                    pixelValue = (UInt32)0xFF000000 |
                        ((UInt32)(l1 * ((data.colorA[i] & 0x00FF0000) >> 16) + l2 * ((data.colorB[i] & 0x00FF0000) >> 16) + l3 * ((data.colorC[i] & 0x00FF0000) >> 16)) << 16) |
                        ((UInt32)(l1 * ((data.colorA[i] & 0x0000FF00) >> 8) + l2 * ((data.colorB[i] & 0x0000FF00) >> 8) + l3 * ((data.colorC[i] & 0x0000FF00) >> 8)) << 8) |
                        (UInt32)(l1 * (data.colorA[i] & 0x000000FF) + l2 * (data.colorB[i] & 0x000000FF) + l3 * (data.colorC[i] & 0x000000FF));
                    break;
                }
            }

            return pixelValue;
        }
    }
    class data
    {
        
        public static int n = 3;

        public static double[] x1 = { 100, 400, 550 };
        public static double[] y1 = { 250, 250, 100 };

        public static double[] x2 = { 250, 250, 250 };
        public static double[] y2 = { 100, 100, 100 };

        public static double[] x3 = { 400, 550, 500};
        public static double[] y3 = { 250, 100, 0 };

        public static UInt32[] colorA = { 0xFF7a50c0, 0xFFea8208, 0xFF7a50c0 };
        public static UInt32[] colorB = { 0xFFbe1fb8, 0xFFbe1fb8, 0xFFbe1fb8 };
        public static UInt32[] colorC = { 0xFFea8208, 0xFF7a50c0, 0xFFea8208 };
    }
}
