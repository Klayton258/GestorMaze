using Bunifu.Framework.UI;
using Gestor_Maze.Controllers;
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
    public partial class ListOfTables : BunifuForm
    {
        public ListOfTables()
        {
            InitializeComponent();
        }
        private void ListTables()
        {
            try
            {

                var p = Task.Run(() => TableController.AllTables());
                p.Wait();

                tblTables.Rows.Clear(); // Clean the table Products

                for (int i = 0; i < p.Result.data.Count; i++)
                {
                    tblTables.Rows.
                        Add(
                            p.Result.data[i].id,
                            p.Result.data[i].table_name,
                            p.Result.data[i].lot,
                            p.Result.data[i].state
                            ); // Add values in the table
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error print products list, please check your connection or contact the admin.",
                                "Products Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
            } //end try catch
        }

        private void ListOfTables_Load(object sender, EventArgs e)
        {
            ListTables();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void tblTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void activateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region Table variables
            int id = (int)tblTables.CurrentRow.Cells[0].Value;
            string name = (string)tblTables.CurrentRow.Cells[1].Value;
            int lot = (int)tblTables.CurrentRow.Cells[2].Value;
            int state = 2;
            #endregion

            var response = Task.Run(() => TableController.UpdateTable(id,name,lot,state));
            response.Wait();

            #region Message

            if (response.Result.code == 1010)
            {
                MessageBox.Show(response.Result.msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(response.Result.msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            #endregion

            ListTables();
        }

        private void desactiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region Table variables
            int id = (int)tblTables.CurrentRow.Cells[0].Value;
            string name = (string)tblTables.CurrentRow.Cells[1].Value;
            int lot = (int)tblTables.CurrentRow.Cells[2].Value;
            int state = 1;
            #endregion

            var response = Task.Run(() => TableController.UpdateTable(id, name, lot, state));
            response.Wait();

            #region Message

            if (response.Result.code == 1010)
            {
                MessageBox.Show(response.Result.msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(response.Result.msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            #endregion

            ListTables();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)tblTables.CurrentRow.Cells[0].Value;
            var response = Task.Run(() => TableController.DeleteProduct(id));
            response.Wait();

            ListTables();
        }

        private void tblTables_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            #region Table variables
            int id = (int)tblTables.CurrentRow.Cells[0].Value;
            string name = (string)tblTables.CurrentRow.Cells[1].Value;
            string lot = (string)tblTables.CurrentRow.Cells[2].Value;
            string state = (string)tblTables.CurrentRow.Cells[2].Value;
            #endregion

            int state_id;
            if (state.Equals("INACTIVE"))
            { state_id = 1; }else{ state_id = 2; }

            var response = Task.Run(() => TableController.UpdateTable(id,name,int.Parse(lot), state_id));
                response.Wait();

                #region Message

                if (response.Result.code == 1010)
                {
                    MessageBox.Show(response.Result.msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(response.Result.msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                #endregion

          
            
        }

       
    }
}
