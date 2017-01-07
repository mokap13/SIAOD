using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAOD_Routing
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DeikstraAlgorytm.Execute();
            //srcMatrix = new int[][]{
            //    new int[]{ 0  ,12 ,inf,28 ,inf,inf,inf },
            //    new int[]{ 12 ,0  ,10 ,43 ,inf,inf,inf },
            //    new int[]{ inf,10 ,0  ,inf,10 ,inf,inf },
            //    new int[]{ 28 ,43 ,17 ,0  ,inf,inf,inf },
            //    new int[]{ inf,31 ,10 ,inf,0  ,8  ,inf },
            //    new int[]{ inf,inf,inf,inf,14 ,0  ,6   },
            //    new int[]{ inf,inf,inf,inf,inf,6  ,0   }
            //};
        }
    }
}
