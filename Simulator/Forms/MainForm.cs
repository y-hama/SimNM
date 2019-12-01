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
        SimNM.Sensor.Sonar sonar = new SimNM.Sensor.Sonar(500, -90);
        Point mloc = new Point();

        public MainForm()
        {
            InitializeComponent();
        }

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            sonar.Update(mloc);
            Canvus.Image = SimNM.Environment.Background.Vision;
            View.Image = sonar.View(View.Size);

            GC.Collect();
        }

        private void Canvus_MouseMove(object sender, MouseEventArgs e)
        {
            mloc = e.Location;
        }
    }
}
