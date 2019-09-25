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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void chk_Effets_CheckedChanged(object sender, EventArgs e)
        {
            Sauvegarde();
        }

        private void chk_Musique_CheckedChanged(object sender, EventArgs e)
        {
            Sauvegarde();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            Lecture();
        }

        public void Sauvegarde()
        {
            int Musique;
            int Effets;

            if (chk_Musique.Checked) Musique = 1;
            else Musique = 0;

            if (chk_Effets.Checked) Effets = 1;
            else Effets = 0;

            System.IO.File.WriteAllText(@"Options.cfg", Musique.ToString()+Effets.ToString());
        }
        public void Lecture()
        {
            bool Musique;
            bool Effets;
            //string txt1="00",txt2="01",txt3="10",txt
            string textOptions = System.IO.File.ReadAllText(@"Options.cfg");
            switch (textOptions)
            {
                case "00": Musique = false; Effets = false; break;
                case "01": Musique = false; Effets = true;  break;
                case "10": Musique = true;  Effets = false; break;
                case "11": Musique = true;  Effets = true;  break;
                default: Musique = true;    Effets = true;  break;
            }
            chk_Musique.Checked = Musique;
            chk_Effets.Checked = Effets;

            //Debug
            /*
            label1.Text = textOptions;
            label2.Text = Musique.ToString() + Effets.ToString();
            */
        }

    }
}
