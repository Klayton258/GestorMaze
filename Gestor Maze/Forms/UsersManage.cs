using Gestor_Maze.Controllers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_Maze.Forms
{
    public partial class UsersManage : Form
    {
        public UsersManage()
        {
            InitializeComponent();
        }

        private void ListUsers()
        {
            try
            {
                var users = Task.Run(() => UserController.AllUsers());
                users.Wait();
                if (users.Result.data != null)
                {
                    string permission = "";
                    for (int i = 0; i < users.Result.data.Count; i++)
                    {
                        if (!users.Result.data[i].permission.Equals("100"))
                        {

                            if (users.Result.data[i].permission.Equals("1"))
                            {
                                permission = "Colaborator";
                            }
                            else if (users.Result.data[i].permission.Equals("10"))
                            {
                                permission = "Administrator";
                            }

                            usersTable.Rows.Add(
                                users.Result.data[i].id,
                                users.Result.data[i].name,
                                users.Result.data[i].username,
                                users.Result.data[i].pass,
                                users.Result.data[i].age,
                                users.Result.data[i].gener,
                                users.Result.data[i].phone,
                                users.Result.data[i].email,
                                users.Result.data[i].document,
                                users.Result.data[i].address,
                                users.Result.data[i].bank_account,
                                users.Result.data[i].salary,
                                permission
                                );
                        }
                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Printing users list, please check your connection or contact the admin.",
                              "List user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
            }
        }

        private void UsersManage_Load(object sender, EventArgs e)
        {
            ListUsers();

        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                object user = usersTable.CurrentRow.Cells[0].Value;

                Task<int> active = Task.Run(() => UserController.ActiveUser(user));
                active.Wait();

            }
            catch (Exception)
            {

                MessageBox.Show(" User Activated.",
                               "Ative user", MessageBoxButtons.OK, MessageBoxIcon.Information); // Return the error message
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                object user = usersTable.CurrentRow.Cells[0].Value;
                Console.WriteLine("User===> " + user);
                Task<int> active = Task.Run(() => UserController.DeleteUser(user));
                active.Wait();

                if (active.Result == 201)
                {
                    ListUsers();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error deleting user, please check your connection or contact the admin.",
                               "Delete user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
            }
        }
    }
}
