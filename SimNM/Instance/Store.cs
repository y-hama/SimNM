using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimNM.Instance
{
    static class Store
    {
        public static List<Body> UnitList { get; set; } = new List<Body>();

        public static List<Body> SearchOtherThanTarget(Body removetarget)
        {
            return UnitList.FindAll(x => x != removetarget);
        }
        public static List<Body> SearchOtherThanTarget(double x, double y)
        {
            return UnitList.FindAll(u => u.X != x && u.Y != y);
        }
    }
}
