using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SimNM.Instance
{
    public abstract class Body
    {

        public Parameter.Parameter Parameter { get; protected set; } = new Instance.Parameter.Parameter();

        public double Size { get { return Parameter.Size; } protected set { Parameter.Size = value; } }

        public System.Drawing.Color IconColor { get { return Parameter.IconColor; } protected set { Parameter.IconColor = value; } }

        public double X { get { return (float)Parameter.X; } protected set { Parameter.X = value; } }
        public double Y { get { return (float)Parameter.Y; } protected set { Parameter.Y = value; } }
        public double Angle { get { return Parameter.Angle; } protected set { Parameter.Angle = value; } }
        public double Digree { get { return (Angle) * Math.PI / 180; } }

        public double Vx { get { return Parameter.Vx; } protected set { Parameter.Vx = value; } }
        public double Vy { get { return Parameter.Vy; } protected set { Parameter.Vy = value; } }
        public double V { get { return Parameter.V; } }

        public double Inertia { get { return Parameter.Inertia; } protected set { Parameter.Inertia = value; } }
        public double Error { get { return Parameter.Error; } protected set { Parameter.Error = value; } }

        public Body()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            Size = 15;

            PointF loc = new PointF();
            Environment.Background.GetEmptyLocation(ref loc, Size);
            X = loc.X;
            Y = loc.Y;

            Angle = 360 * Parameter.Random.NextDouble();
            Inertia = 0.5;
            Error = 0.005;
        }

        public void SetLcoation(Point p)
        {
            X = p.X; Y = p.Y;
        }

        public void SetAngle(double angle)
        {
            if (angle < 0)
            {
                angle += 360;
            }
            else if (angle > 360)
            {
                angle -= 360;
            }
            Angle = angle;
        }

        public virtual void DrawIcon(Graphics g)
        {
            g.DrawEllipse(new Pen(IconColor, 1), new RectangleF((float)(X - Size / 2), (float)(Y - Size / 2), (float)(Size), (float)(Size)));
        }

        public abstract void SetParam(int mode, params double[] param);

        public abstract void Observation();
        public abstract void Update();
    }
}
