using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DoAnCoSo.Table;

namespace DoAnCoSo.Controls
{
    public partial class mdlFunctionRole : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();
        public mdlFunctionRole()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Role role = comboBox1.SelectedItem as Role;
            if (role != null)
            {
                functionRoleBindingSource.DataSource = db.FunctionRoles.Where(r => r.RolesID == role.RolesID).ToList();
            }
        }

        private void FunctionRole_Load(object sender, EventArgs e)
        {
            roleBindingSource.DataSource = db.Roles.ToList();
            functionBindingSource1.DataSource = db.Functions.ToList();
            Role role = comboBox1.SelectedItem as Role;
            if ( role != null)
            {
                functionRoleBindingSource.DataSource = db.FunctionRoles.Where(r => r.RolesID == role.RolesID).ToList();
            }
        }

        private async void Remove_Click(object sender, EventArgs e)
        {
            if ( functionRoleBindingSource.DataSource != null)
            {
                Role role = comboBox1.SelectedItem as Role;
                if ( role != null)
                {
                    foreach(DataGridViewRow row in dataGridView2.SelectedRows)
                    {
                        FunctionRole fr = row.DataBoundItem as FunctionRole;
                        if (fr != null)
                        {
                            db.FunctionRoles.Remove(fr);
                            functionRoleBindingSource.Remove(fr);

                        }
                    }
                    await db.SaveChangesAsync();
                }
            }
        }

        private async void Add_Click(object sender, EventArgs e)
        {
            if (functionRoleBindingSource.DataSource != null)
            {
                Role role = comboBox1.SelectedItem as Role;
                if (role != null)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        Function f = row.DataBoundItem as Function;
                        if (f != null)
                        {
                            FunctionRole fr = db.FunctionRoles.Where(p => p.FunctionID == f.FunctionID && p.RolesID == role.RolesID).SingleOrDefault();
                            if (fr == null)
                            {
                                FunctionRole obj = new FunctionRole() { RolesID = role.RolesID, FunctionID = f.FunctionID };
                                db.FunctionRoles.Add(obj);
                            }
                        }
                    }
                    await db.SaveChangesAsync();
                    functionRoleBindingSource.DataSource = db.FunctionRoles.Where(r => r.RolesID == role.RolesID).ToList();
                }
            }
        }
    }
}