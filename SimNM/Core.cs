using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SimNM
{
    public static class Core
    {
        public static Size FieldSize { get; set; } = new Size(500, 500);

        public static void Initialize()
        {
            Environment.Background.Initialize(FieldSize);
        }
    }
}
