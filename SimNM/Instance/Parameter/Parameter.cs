﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimNM.Instance.Parameter
{
    public class Parameter
    {
        private static Random random = new Random();
        public Random Random { get { return random; } }

        #region Base
        public System.Drawing.Color IconColor { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public double Vx { get; set; }
        public double Vy { get; set; }
        public double V { get { return Math.Sqrt(Vx * Vx + Vy * Vy); } }

        public double Angle { get; set; }

        public double Error { get; set; } = 0.1;
        public double Inertia { get; set; } = 0.85;
        #endregion

        #region WheelCar
        public double Size { get; set; }

        public double WheelSize { get; set; }

        public double Rotmax { get; set; }
        public double RotL { get; set; }
        public double RotR { get; set; }
        #endregion

        #region Neural
        #endregion
    }
}