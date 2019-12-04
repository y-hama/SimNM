using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimNM.Instance
{
    public class Tracker : WheelCar
    {
        private const double ep = 1E-3;

        public double LimitArea { get { return Parameter.LimitArea; } protected set { Parameter.LimitArea = value; } }

        protected override void Initialize()
        {
            base.Initialize();
            LimitArea = Viewing / 2;
        }

        protected override void WheelRotationAmountCalculation(ref double l, ref double r)
        {
            l = r = 1;
            var lims = Sonar.Distance.FindAll(x => (Math.Abs(x.Angle) < LimitArea / 2)).FindAll(y => ((y.Distance <= Size * Rotmax * 1.5)));

            double a = 2 * (1 - ep);
            double b = Math.Log(((2 * (1 - ep)) / ep) - 1) / (LimitArea / 2);

            if (lims.Count > 0)
            {
                double angleave = lims.Average(x => x.Angle);
                if (Math.Abs(angleave) < Math.Min(20, LimitArea / 5))
                {
                    l = -1 + (Parameter.Random.NextDouble() * 2 - 1);
                    r = -1 + (Parameter.Random.NextDouble() * 2 - 1);
                }
                else
                {
                    double dan = lims.Sum(x => x.Angle) / lims.Count;
                    double ratio = 1 - (Math.Abs(angleave)) / (LimitArea / 2);
                    l += ratio * (a / (1 + Math.Exp(b * dan)));
                    r += ratio * (a / (1 + Math.Exp(-b * dan)));
                }
            }

            base.WheelRotationAmountCalculation(ref l, ref r);
        }
    }
}
