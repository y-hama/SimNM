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

        public static void AddUnit(Instance.Body body)
        {
            Instance.Store.UnitList.Add(body);
        }

        public static void AddUnit(Type type, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Instance.Store.UnitList.Add((Instance.Body)Activator.CreateInstance(type));
            }
        }

        public static void DoStep()
        {
            foreach (var item in Instance.Store.UnitList)
            {
                item.Observation();
            }
            foreach (var item in Instance.Store.UnitList)
            {
                item.Update();
            }
        }

        public static Instance.Body HeadUnit()
        {
            return Instance.Store.UnitList[0];
        }

        public static Bitmap FieldImage()
        {
            var bitmap = SimNM.Environment.Background.Vision;
            var g = Graphics.FromImage(bitmap);

            foreach (var item in Instance.Store.UnitList)
            {
                item.DrawIcon(g);
                if (item == HeadUnit())
                {
                    g.DrawEllipse(new Pen(Color.FromArgb((byte)~item.IconColor.R, (byte)~item.IconColor.G, (byte)~item.IconColor.B), 3),
                        new Rectangle((int)(item.X - item.Size / 2), (int)(item.Y - item.Size / 2), (int)(item.Size / 1), (int)(item.Size / 1)));
                }
            }

            return bitmap;
        }
    }
}
