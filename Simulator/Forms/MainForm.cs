using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator.Forms
{
    public partial class MainForm : Form
    {
        private bool Terminate { get; set; }

        SimNM.Instance.WheelCar car { get; set; }

        private object __lockobject { get; set; } = new object();
        Bitmap CanvusImage { get; set; }
        Bitmap ViewImage { get; set; }

        private static class Config
        {
            public static bool LeftClick { get; set; }
            public static bool RightClick { get; set; }

            public static bool Up { get; set; }
            public static bool Down { get; set; }
            public static bool Left { get; set; }
            public static bool Right { get; set; }


            public static void ConvertMouseData(MouseButtons button, bool flag)
            {
                switch (button)
                {
                    case MouseButtons.Left:
                        LeftClick = flag;
                        break;
                    case MouseButtons.None:
                        break;
                    case MouseButtons.Right:
                        RightClick = flag;
                        break;
                    case MouseButtons.Middle:
                        break;
                    case MouseButtons.XButton1:
                        break;
                    case MouseButtons.XButton2:
                        break;
                    default:
                        break;
                }
            }

            public static void ConvertKeyData(Keys key, bool flag)
            {
                switch (key)
                {
                    case Keys.Up:
                        Up = flag;
                        break;
                    case Keys.Down:
                        Down = flag;
                        break;
                    case Keys.Left:
                        Left = flag;
                        break;
                    case Keys.Right:
                        Right = flag;
                        break;
                    default:
                        break;
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
            car = new SimNM.Instance.WheelCar();

            (new Task(() =>
            {
                double timespan = 0;
                while (!Terminate)
                {
                    double l = 0, r = 0;
                    if (Config.Up)
                    {
                        l += 1; r += 1;
                    }
                    if (Config.Down)
                    {
                        l -= 1; r -= 1;
                    }
                    if (Config.Left)
                    {
                        l += 0.25; r += 0.75;
                    }
                    if (Config.Right)
                    {
                        l += 0.75; r += 0.25;
                    }
                    if (Config.RightClick || Config.LeftClick)
                    {
                        if (Config.RightClick) { car.SetAngle(car.Angle + 1); }
                        else { car.SetAngle(car.Angle - 1); }
                    }
                    car.SetParam(0, l, r);
                    DateTime start = DateTime.Now;
                    car.Observation();
                    car.Update();
                    double span = (DateTime.Now - start).TotalMilliseconds;
                    timespan = Math.Max(timespan, span);

                    lock (__lockobject)
                    {
                        CanvusImage = SimNM.Environment.Background.Vision;
                        ViewImage = car.View(View.Size);
                        car.DrawIcon(Graphics.FromImage(CanvusImage));
                    }

                    System.Threading.Thread.Sleep((int)Math.Abs(timespan - span));
                }
            })).Start();
        }

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            lock (__lockobject)
            {
                Canvus.Image = CanvusImage;
                View.Image = ViewImage;
            }
            this.Text = car.V.ToString();
            GC.Collect();
        }

        private void Canvus_MouseMove(object sender, MouseEventArgs e)
        {
            this.Activate();
        }

        private void Canvus_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Canvus_MouseDown(object sender, MouseEventArgs e)
        {
            Config.ConvertMouseData(e.Button, true);
        }

        private void Canvus_MouseUp(object sender, MouseEventArgs e)
        {
            Config.ConvertMouseData(e.Button, false);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Config.ConvertKeyData(e.KeyData, true);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            Config.ConvertKeyData(e.KeyData, false);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Terminate = true;
        }
    }
}
