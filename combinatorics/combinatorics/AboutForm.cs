using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace combinatorics
{
    public partial class AboutForm : Telerik.WinControls.UI.RadForm
    {
        RadForm1 radForm;
        public AboutForm(RadForm1 radForm)
        {
            InitializeComponent();
            this.radForm = radForm;
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            radForm.Enabled = true;
        }

        private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            radForm.Enabled = true;
        }
    }
}
