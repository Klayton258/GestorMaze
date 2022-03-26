using Bunifu.UI.WinForms.BunifuButton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_Maze.Forms
{
    public partial class Keyboard : Form
    {
        public Keyboard()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= 0x08000000;
                return param;
            }
        }
        private void Keyboard_Load(object sender, EventArgs e)
        {

        }

        private void btnSpace_Click(object sender, EventArgs e)
        {
            SendKeys.Send(" ");
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{ENTER}");
        }

        private void Alphabet(object sender, EventArgs e)
        {
            BunifuButton btn = (BunifuButton)sender;

            if (btnCapslock.Checked == true || btnShift.Checked == true)
            {
                SendKeys.Send((btn.Text).ToUpper());
                btnShift.Checked = false;
            }
            else
            {
                SendKeys.Send((btn.Text).ToLower());

            }
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BS}");
        }

        private void NumPad(object sender, EventArgs e)
        {
            BunifuButton btn = (BunifuButton)sender;

            SendKeys.Send((btn.Text));
        }

        private void btnF1_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F1}");
        }

        private void btnF2_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F2}");
        }

        private void btnF3_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F3}");
        }

        private void btnF4_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }

        private void btnF5_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F5}");
        }

        private void btnF6_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F6}");
        }

        private void btnF7_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F7}");
        }

        private void btnF8_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F8}");
        }

        private void btnF9_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F9}");
        }

        private void btnF10_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F10}");
        }

        private void btnF11_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F11}");
        }

        private void btnF12_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F12}");
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{UP}");
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{RIGHT}");
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{LEFT}");
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{DOWN}");
        }

        private void btnTab_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
        }

        private void Parentes(object sender, EventArgs e)
        {
            BunifuButton btn = (BunifuButton)sender;

            if (btnShift.Checked == true)
            {
                SendKeys.Send(")");
                btnShift.Checked = false;
            }
            else
            {
                SendKeys.Send("(");

            }
        }

        private void btnVirgula_Click(object sender, EventArgs e)
        {

            BunifuButton btn = (BunifuButton)sender;

            SendKeys.Send((btn.Text));
        }

        private void btnCtrl_Click(object sender, EventArgs e)
        {
            SendKeys.Send("(");
        }
    }
}
