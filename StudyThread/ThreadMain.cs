using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudyThread
{
    public partial class ThreadMain : Form
    {
        public ThreadMain()
        {
            InitializeComponent();
        }

        private void btn_Thread_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.ShowDialog();
        }

        private void btn_MutipleCompare_Click(object sender, EventArgs e)
        {
            MutipleCompare mutipleCompare = new MutipleCompare();
            mutipleCompare.ShowDialog();
        }
    }
}
