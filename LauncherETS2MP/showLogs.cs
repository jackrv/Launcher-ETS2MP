using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LauncherETS2MP
{
    public partial class showLogs : Form
    {
        public showLogs()
        {
            InitializeComponent();
        }

        private void showLogs_Load(object sender, EventArgs e)
        {
            foreach (string logLine in Properties.Settings.Default.logs)
            {
                //MessageBox.Show(logLine);
                listView1.Items.Add(logLine);
            }
        }
    }
}
