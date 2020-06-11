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

namespace DoAnCoSo.Main
{
    public partial class Main1 : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();
        public Main1()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblogin.Text = $"Login: {Login.Instance.UserInfo.UserName}";
            lslop.Text = $"Lớp: {Login.Instance.UserInfo.Lop.TenLop}";
            var sql = from ur in db.Users
                      join fr in db.FunctionRoles on ur.RolesID equals fr.RolesID
                      join f in db.Functions on fr.FunctionID equals f.FunctionID
                      where ur.UserName == Login.Instance.UserInfo.UserName
                      select new
                      {
                          FunctionName = f.FunctionName,
                          FromName = f.Formname
                      };
            foreach (var obj in sql.ToList())
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = "item" + obj.FromName;
                item.Tag = obj.FromName;
                item.Text = obj.FunctionName;
                item.Click += Item_Click;
                mainToolStripMenuItem.DropDownItems.Add(item);

            }
            ToolStripMenuItem itemlogout = new ToolStripMenuItem();
            itemlogout.Name = "itemLogout";
            itemlogout.Text = "Đăng xuất ";
            itemlogout.Click += Itemlogout_Click;
            mainToolStripMenuItem.DropDownItems.Add(itemlogout);
        }
        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                Type t = Type.GetType(item.Tag.ToString());
                Form frm = Activator.CreateInstance(t) as Form;
                if (frm != null)
                {
                    frm.MdiParent = this;
                    frm.Show();
                }
            }

        }
        private void Itemlogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            //_islogout = true;
            //Login.Instance.Show();
            this.Close();
        }
        private Form kiemtraform(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == ftype)
                {
                    return f;
                }
            }
            return null;
        }
    }
}