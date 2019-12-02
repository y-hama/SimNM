using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SimNM.Sensor
{
    public class Sonar
    {
        public class SignalSet
        {
            public double Angle { get; set; }
            public double Distance { get; set; }
        }

        public double RelativeAngle { get; set; }

        private int Resolution { get; set; }
        private double step { get; set; }

        public SignalSet[] Distance { get; private set; }

        public Point Location { get; private set; }

        public Sonar(int resolution, double angle = 0)
        {
            RelativeAngle = angle;
            Resolution = resolution;
            step = 360.0 / resolution;
        }

        public SignalSet[] Update(Point location)
        {
            Location = location;
            Distance = new SignalSet[Resolution + 1];

            double area = Resolution / 2;
            double ang = 150;
            for (double i = -area; i <= area; i++)
            {
                double angle = (ang * Math.PI / 180) * ((double)i / (Resolution / 2));
                double nx, ny, dist;
                nx = Math.Cos(angle + (RelativeAngle + 45) * Math.PI / 180);
                ny = Math.Sin(angle + (RelativeAngle + 45) * Math.PI / 180);
                double framedist = FrameCollision(nx, ny);
                dist = Math.Min(framedist, WallCollision(nx, ny, framedist));
                Distance[(int)(i + area)] = new SignalSet() { Angle = angle, Distance = dist };
            }

            return Distance;
        }

        private double FrameCollision(double nx, double ny)
        {
            double ret = 0;
            double s1, s2, s3, s4;
            s1 = (0 - Location.X) / nx;
            s2 = (Environment.Background.Size.Width - Location.X) / nx;
            s3 = (0 - Location.Y) / ny;
            s4 = (Environment.Background.Size.Height - Location.Y) / ny;
            List<double> spp = new List<double>();
            if (s1 >= 0) { spp.Add(s1); }
            if (s2 >= 0) { spp.Add(s2); }
            if (s3 >= 0) { spp.Add(s3); }
            if (s4 >= 0) { spp.Add(s4); }
            if (spp.Count > 0)
            {
                ret = spp.Min();
            }
            return ret;
        }
        private double WallCollision(double nx, double ny, double maxdist)
        {
            double ret = 0;
            while (ret < maxdist)
            {
                if (Environment.Background.IsWall((int)(Location.X + ret * nx), (int)(Location.Y + ret * ny)))
                {
                    break;
                }
                ret++;
            }
            return ret;
        }

        public Bitmap View(Size framesize)
        {
            double max = Environment.Background.DiagonalSize / 2;
            Bitmap bitmap = new Bitmap(framesize.Width, framesize.Height);
            Graphics g = Graphics.FromImage(bitmap);
            int width = framesize.Width / Resolution + 1;
            for (int i = 0; i < Resolution; i++)
            {
                byte p = (byte)(byte.MaxValue * (1 - Math.Min(Distance[i].Distance, max) / max));
                g.FillRectangle(new SolidBrush(Color.FromArgb(p, p, p)), i * width, 0, width, framesize.Height);
            }
            return bitmap;
        }
    }
}
