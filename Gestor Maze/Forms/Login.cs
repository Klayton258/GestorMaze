using Gestor_Maze.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_Maze.Forms
{
    public partial class Login : Bunifu.Framework.UI.BunifuForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var log = Task.Run(() => UserController.Login(txtUsername.Text.Trim(), txtPassword.Text.Trim()));
                log.Wait();

                if (log.Result.data != null)
                {
                    new FormPrincipal(log.Result.data[0].name, log.Result.data[0].permission).Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Make sure the username and the password are correct.",
                                   "Log in Fail", MessageBoxButtons.OK, MessageBoxIcon.Error); // Return the error message
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Make sure the username and the password are correct.",
                                  "Log in Fail", MessageBoxButtons.OK, MessageBoxIcon.Error); // Return the error message
            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (txtPassword.isPassword)
            {
                txtPassword.isPassword = false;
            }
            else
            {
                txtPassword.isPassword = true;
            }
        }

        private void bntClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo("https://mazedeve.com");
            Process.Start(info);
        }
    }
}
