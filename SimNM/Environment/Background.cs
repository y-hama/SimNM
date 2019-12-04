using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SimNM.Environment
{
    public static class Background
    {
        private static Random random = new Random();

        private static object __lockobject = new object();
        private static Bitmap source { get; set; }
        public static Bitmap Vision
        {
            get
            {
                Bitmap obj;
                lock (__lockobject)
                {
                    obj = (Bitmap)source.Clone();
                }
                return obj;
            }
        }

        public static Size Size { get; private set; }
        public static double DiagonalSize
        {
            get
            {
                return Math.Sqrt(Size.Width * Size.Width + Size.Height * Size.Height);
            }
        }

        public static void Initialize(Size size, double rho = 0.1)
        {
            Size = size;
            lock (__lockobject)
            {
                source = new Bitmap(size.Width, size.Height);
                Graphics g = Graphics.FromImage(source);
                g.FillRectangle(Brushes.Blue, new Rectangle(new Point(), size));
                g.FillEllipse(Brushes.Black, new Rectangle(new Point(2, 2), new Size(size.Width - 4, size.Height - 4)));

                int s = (size.Width + size.Height) / 40;
                g.FillEllipse(Brushes.Blue, new Rectangle(new Point(size.Width / 2 - s / 2, size.Width / 2 - s / 2), new Size(s, s)));

                int counter = 0;
                do
                {
                    int x, y, width, height, mode;
                    x = (int)(size.Width * random.NextDouble());
                    y = (int)(size.Height * random.NextDouble());
                    width = (int)((size.Width / 8) * (random.NextDouble() + 1) / 2);
                    height = (int)((size.Height / 8) * (random.NextDouble() + 1) / 2);
                    mode = random.Next(1);
                    if (mode == 0)
                    {
                        g.FillEllipse(Brushes.Blue, new Rectangle(new Point(x, y), new Size(width, height)));
                    }
                    else
                    {
                        g.FillRectangle(Brushes.Blue, new Rectangle(new Point(x, y), new Size(width, height)));
                    }
                    counter++;
                } while (counter < 1 || random.NextDouble() > rho);
            }
        }

        public static bool IsWall(int x, int y)
        {
            bool ret = false;
            if (x >= 0 && x < Size.Width && y >= 0 && y < Size.Height)
            {
                Color color;
                lock (__lockobject)
                {
                    color = source.GetPixel(x, y);
                }
                if (color.B > 100) { ret = true; }
            }
            else
            {
                ret = true;
            }
            return ret;
        }

        public static void GetEmptyLocation(ref PointF loc, double size)
        {
            Sensor.Sonar sonar = new Sensor.Sonar(360, 360);
            Point tmp = new Point();
            bool check = false;
            while (!check)
            {
                check = true;
                tmp.X = (int)(Size.Width * random.NextDouble());
                tmp.Y = (int)(Size.Height * random.NextDouble());
                sonar.Update(tmp);
                foreach (var item in sonar.Distance)
                {
                    if (item.Distance < size)
                    {
                        check = false;
                    }
                }
            }
            loc.X = tmp.X;
            loc.Y = tmp.Y;
        }
    }
}
