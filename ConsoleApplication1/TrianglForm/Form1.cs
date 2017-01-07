using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Drawing = System.Drawing.Drawing2D;

namespace TrianglForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Drawing.GraphicsPath myPath = new Drawing.GraphicsPath();
            myPath.AddPolygon(new Point[] { new Point(0, 0), new Point(0, this.Height),
new Point(this.Width, 0) });
            Region myRegion = new Region(myPath);
            this.Region = myRegion;
        }

        protected void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
