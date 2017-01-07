using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MouseUp += MyHandler;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Name = "Кнопка2";
            button2.BackColor = Color.Linen;
        }

        public void MyHandler(object sender, EventArgs e)
        {
            button1.BackColor = Color.Azure;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.DarkGray;
        }
    }

    public interface ICls
    {
        void Func();
    }
    public class MyClass : ICls
    {
        public void Func()
        {
            throw new NotImplementedException();
        }
    }
    public interface ISpecCls
    {
        void SpecialFunc();
    }

    public class MyClass2 : ISpecCls
    {
        public void SpecialFunc()
        {
            throw new NotImplementedException();
        }
    }

    public class Adapter : ICls
    {
        public void Func()
        {
            throw new NotImplementedException();
        }
    }
}
