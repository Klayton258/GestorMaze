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
    public partial class PaymentMethod : Form
    {
        public PaymentMethod()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string method = "";

            if (rbtnCash.Checked)
            {
                method = "CASH";
            }
            else if (rbtnCard.Checked)
            {
                method = "CARD";
            }else if (rbtnMpesa.Checked)
            {
                method = "M-PESA";
            }else if (rbtnContaMovel.Checked)
            {
                method = "CONTA MOVEL";
            }


            MessageBox.Show(method);
        }

        private void lblCash_Click(object sender, EventArgs e)
        {
            rbtnCash.Checked = true;

            rbtnContaMovel.Checked = false;
            rbtnCard.Checked = false;
            rbtnMpesa.Checked = false;
        }

        private void lblMpasa_Click(object sender, EventArgs e)
        {
            rbtnMpesa.Checked = true;

            rbtnContaMovel.Checked = false;
            rbtnCard.Checked = false;
            rbtnCash.Checked = false;
        }

        private void lblContaMovel_Click(object sender, EventArgs e)
        {
            rbtnContaMovel.Checked = true;

            rbtnCard.Checked = false;
            rbtnCash.Checked = false;
            rbtnMpesa.Checked = false;
        }

        private void lblCard_Click(object sender, EventArgs e)
        {
            rbtnCard.Checked = true;

            rbtnContaMovel.Checked = false;
            rbtnCash.Checked = false;
            rbtnMpesa.Checked = false;
        }
    }
}
