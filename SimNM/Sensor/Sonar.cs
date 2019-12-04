using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SimNM.Sensor
{
    public class Sonar : Base
    {
        public class SignalSet
        {
            public int ID { get; set; }
            public double Angle { get; set; }
            public double Digree { get { return (Angle) * Math.PI / 180; } }
            public double Distance { get; set; }
            public new string ToString()
            {
                return Distance.ToString();
            }
        }

        public double RelativeAngle { get; set; }
        public double Viewing { get; set; }
        public double RelativeDigree { get { return (RelativeAngle) * Math.PI / 180; } }

        private int Resolution { get; set; }
        private double step { get; set; }

        public int IDArea { get { return Resolution / 2; } }
        public List<SignalSet> Distance { get; private set; }
        public SignalSet this[int id]
        {
            get
            {
                if (id < -IDArea || IDArea < id)
                {
                    return null;
                }
                return (Distance).Find(x => x.ID == id);
            }
        }

        public PointF Location { get; private set; }

        public Sonar(int resolution, double viewing, double angle = 0, double locx = 0, double locy = 0)
        {
            RelativeAngle = angle;
            Viewing = viewing / 2;
            Resolution = resolution;
            step = 360.0 / resolution;
            Location = new PointF((float)locx, (float)locy);
            Distance = new List<SignalSet>(new SignalSet[Resolution + 1]);
            for (int i = 0; i < Distance.Count; i++)
            {
                Distance[i] = new SignalSet();
            }
            Update(Location);
        }

        public List<SignalSet> Update(PointF location)
        {
            Location = location;

            int area = Resolution / 2;
            double ang = Viewing;
            for (int i = -area; i <= area; i++)
            {
                double angle = (ang) * ((double)i / (area));
                double nx, ny, dist;
                nx = Math.Cos((angle + RelativeAngle) * Math.PI / 180);
                ny = Math.Sin((angle + RelativeAngle) * Math.PI / 180);
                double framedist = FrameCollision(nx, ny);
                dist = Math.Min(framedist, WallCollision(nx, ny, framedist));
                dist = Math.Min(dist, UnitCollision(nx, ny, dist));
                Distance[(i + area)].ID = i;
                Distance[(i + area)].Angle = angle;
                Distance[(i + area)].Distance = dist;
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

        private double UnitCollision(double nx, double ny, double maxdist)
        {
            var units = Instance.Store.SearchOtherThanTarget(Location.X, Location.Y);
            double ret = 0;
            while (ret < maxdist)
            {
                double nnx, nny;
                nnx = Location.X + ret * nx; nny = Location.Y + ret * ny;
                bool check = false;
                foreach (var item in units)
                {
                    if (item.DistanceTo(nnx, nny) < item.Size / 2)
                    {
                        check = true; break;
                    }
                }
                if (check)
                {
                    break;
                }
                ret++;
            }
            return ret;
        }

        public Bitmap View(Size framesize)
        {
            int cnt = Distance.Count;
            int width = 1;// framesize.Width / cnt + 1;
            double max = Environment.Background.DiagonalSize / 2;
            Bitmap bitmap = new Bitmap(cnt, framesize.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
            for (int i = 0; i < cnt; i++)
            {
                byte p = (byte)(byte.MaxValue * (1 - Math.Min(Distance[i].Distance, max) / max));
                g.FillRectangle(new SolidBrush(Color.FromArgb(p, p, p)), i * width, framesize.Height / 4, width, framesize.Height);
                if (i == cnt / 2)
                {
                    g.FillRectangle(Brushes.Red, i * width, 0, width, framesize.Height / 4);
                }
            }
            Bitmap ret = new Bitmap(framesize.Width, framesize.Height);
            Graphics gg = Graphics.FromImage(ret);
            gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            gg.DrawImage(bitmap, new RectangleF(0, 0, ret.Width, ret.Height));
            return ret;
        }
    }
}
