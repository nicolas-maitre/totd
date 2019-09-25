using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOTDMenu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Jouer_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TOTD.exe");
            Application.Exit();
        }

        private void btn_Quitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Options_Click(object sender, EventArgs e)
        {
            Options frm = new Options();
            frm.Show();
        }
    }
}
