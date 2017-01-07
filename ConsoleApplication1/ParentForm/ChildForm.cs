using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ParentForm
{
    public partial class ChildForm : ParentForm
    {
        public ChildForm()
        {
            InitializeComponent();
        }

        private void ChildTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ToggleMenuItem_Click(object sender, EventArgs e)
        {
            if (ToggleMenuItem.Checked)
            {
                ToggleMenuItem.Checked = false;
                ChildTextBox.ForeColor = Color.Black;
            }
            else
            {
                ToggleMenuItem.Checked = true;
            ChildTextBox.ForeColor = Color.Blue;
            }
        }
    }
}
