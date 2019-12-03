using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SimNM.Instance
{
    public class WheelCar : Body
    {
        protected Sensor.Sonar Sonar { get; set; }

        private double setL { get; set; }
        private double setR { get; set; }
        private double rotl { get; set; }
        private double rotr { get; set; }

        public double Rotmax { get { return Parameter.Rotmax; } protected set { Parameter.Rotmax = value; } }
        public double RotL { get { return Parameter.RotL; } protected set { Parameter.RotL = value; } }
        public double RotR { get { return Parameter.RotR; } protected set { Parameter.RotR = value; } }
        private double RotNorm { get { return Math.Sqrt(RotL * RotL + RotR * RotR); } }
        public double WheelRadius { get { return Parameter.WheelSize; } protected set { Parameter.WheelSize = value; } }

        public Bitmap View(Size framesize)
        {
            return Sonar.View(framesize);
        }

        protected override void Initialize()
        {
            base.Initialize();
            IconColor = Color.Yellow;
            WheelRadius = Size / 2;
            Rotmax = 1;
            Sonar = new Sensor.Sonar(180, 355, Angle, X, Y);
        }

        public override void SetParam(int mode, params double[] param)
        {
            if (param.Length >= 1) { setL = param[0]; }
            if (param.Length >= 2) { setR = param[1]; }
        }

        public override void Observation()
        {
            Sonar.RelativeAngle = Angle;
            Sonar.Update(new PointF((float)X, (float)Y));

            double l = 0, r = 0;
            WheelRotationAmountCalculation(ref l, ref r);
            rotl = (Inertia) * rotl + (1 - Inertia) * l;
            rotr = (Inertia) * rotr + (1 - Inertia) * r;
            double n = Math.Sqrt(rotl * rotl + rotr * rotr);
            if (n > Rotmax) { rotl /= n; rotr /= n; }
            UpdateRotation(rotl, rotr);
        }

        protected virtual void WheelRotationAmountCalculation(ref double l, ref double r)
        {
            l += setL;
            r += setR;
            setL = setR = 0;
        }

        private void UpdateRotation(double l, double r)
        {
            RotL = (Inertia) * RotL + (1 - Inertia) * (l + Error * (Parameter.Random.NextDouble() * 2 - 1));
            RotR = (Inertia) * RotR + (1 - Inertia) * (r + Error * (Parameter.Random.NextDouble() * 2 - 1));
        }

        public override void Update()
        {
            Estimation();
        }

        private void Estimation()
        {
            double cX = X, cY = Y;
            double cVx = Vx, cVy = Vy;
            if (RotL == RotR)
            {
                double v = (RotL + RotR) / 2;
                X += WheelRadius * v * Math.Cos(Digree);
                Y += WheelRadius * v * Math.Sin(Digree);
            }
            else
            {
                double wa, wb, ol, cx, cy, th, rh = 1;
                double nx, ny;
                if (RotL > RotR)
                {
                    rh = 1;
                    wa = RotL; wb = RotR;
                }
                else
                {
                    rh = -1;
                    wa = RotR; wb = RotL;
                }

                nx = rh * (Math.Sin(Digree));
                ny = rh * (-Math.Cos(Digree));

                th = rh * WheelRadius * (wa - wb) / Size;
                ol = ((wa + wb) / (wa - wb)) * (Size / 2);
                cx = X + nx * ol;
                cy = Y + ny * ol;

                double c, s;
                c = Math.Cos(th); s = Math.Sin(th);
                X = c * X + s * Y + (+(1 - c) * cx - s * cy);
                Y = -s * X + c * Y + (+s * cx + (1 - c) * cy);

                Angle += th * 180 / Math.PI;

                if (Angle < 0)
                {
                    Angle += 360;
                }
                else if (Angle > 360)
                {
                    Angle -= 360;
                }

            }
            Vx = (X - cX); Vy = (Y - cY);
            Ax = (Vx - cVx); Ay = (Vy - cVy);
        }

        public override void DrawIcon(Graphics g)
        {
            Sonar.Update(new PointF((float)X, (float)Y));
            base.DrawIcon(g);
            foreach (var item in Sonar.Distance)
            {
                float x = (float)(X + item.Distance * Math.Cos(item.Digree + Sonar.RelativeDigree));
                float y = (float)(Y + item.Distance * Math.Sin(item.Digree + Sonar.RelativeDigree));
                g.DrawLine(new Pen(Color.FromArgb(100, Color.Gray), 1), new PointF((float)X, (float)Y), new PointF(x, y));
                g.FillEllipse(new SolidBrush(Color.FromArgb(50, Color.Gray)), new RectangleF((float)(x - 2), (float)(y - 2), 4, 4));
            }

            g.DrawLine(new Pen(IconColor, 1), new PointF((float)X, (float)Y), new PointF((float)(X + Size * Math.Cos(Digree)), (float)(Y + Size * Math.Sin(Digree))));
            g.DrawEllipse(new Pen(IconColor, 1), new RectangleF((float)(X - Size / 2), (float)(Y - Size / 2), (float)(Size), (float)(Size)));
            g.FillEllipse(new SolidBrush(IconColor), new RectangleF((float)(X - Size / 4), (float)(Y - Size / 4), (float)(Size / 2), (float)(Size / 2)));

            g.DrawLine(new Pen(Color.Red, 1), new PointF((float)X, (float)Y), new PointF((float)(X + Size * Vx), (float)(Y + Size * Vy)));
            g.DrawLine(new Pen(Color.Cyan, 1), new PointF((float)X, (float)Y), new PointF((float)(X + Size * Ax), (float)(Y + Size * Ay)));
        }
    }
}
