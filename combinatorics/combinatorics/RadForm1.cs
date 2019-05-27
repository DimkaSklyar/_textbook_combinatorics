using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace combinatorics
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        string path = Application.StartupPath;
        List<string> list = new List<string>();
        public RadForm1()
        {
            InitializeComponent();
        }

        private void radTreeView1_SelectedNodeChanged(object sender, Telerik.WinControls.UI.RadTreeViewEventArgs e)
        {
            Uri uri = new Uri(path + "\\files\\" + e.Node.Text + ".html");
            webBrowser1.Url = uri;
        }

        private void radMenuItem5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radMenuItem3_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "*.tst";
            try
            {
                openFileDialog1.ShowDialog();
                string[] lines = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                for (int i = 0; i < lines.Length; i++)
                {
                    list.Add(Crypto.DecryptStringAES(lines[i], "test"));
                }
                testing testing = new testing(list, this);
                testing.Show();
                this.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка открытия теста!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void radMenuItem6_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm(this);
            aboutForm.Show();
            this.Enabled = false;
        }
    }
}
