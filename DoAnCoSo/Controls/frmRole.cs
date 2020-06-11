using DevExpress.XtraEditors;
using DoAnCoSo.Table;
using System;
using System.Linq;

namespace DoAnCoSo
{
    public partial class frmRole : DevExpress.XtraEditors.XtraForm
    {
        public int ID = -1;
        database db = new database();
        public frmRole()
        {
            InitializeComponent();
        }
        private void laydata()
        {
            if (ID > 0)
            {
                themdonvi.Enabled = false;
                var role = db.Roles.Where(p => p.RolesID == ID).SingleOrDefault();

                if (role != null)
                {
                    txtvaitro.Text = role.TenRole;                    
                }
            }
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LuuRole();
            XtraMessageBox.Show("Đã Lưu!");
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditRole();
            XtraMessageBox.Show("Đã Cập nhật!");
            this.Close();
        }
        void LuuRole()
        {
            Role rl = new Role()
            {
                TenRole = txtvaitro.Text,
            };
            db.Roles.Add(rl);
            db.SaveChanges();
        }
        void EditRole()
        {
            var role = db.Roles.Where(p => p.RolesID == ID).SingleOrDefault();
            role.TenRole = txtvaitro.Text;
            db.SaveChanges();
        }

        private void frmDonVi_Load(object sender, EventArgs e)
        {
            laydata();            
        }
    }
}