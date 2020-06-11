using DevExpress.XtraEditors;
using DoAnCoSo.Modules;
using DoAnCoSo.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using ComboBox = System.Windows.Forms.ComboBox;

namespace DoAnCoSo
{
    public partial class frmUser : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();
        public frmUser()
        {
            InitializeComponent();
        }
        public int ID = -1;
        private void laydata()
        {
            if (ID > 0)
            {
                barButtonItem1.Enabled = false;
                var uss = db.Users.Where(p => p.UserID == ID).SingleOrDefault();

                if (uss != null)
                {
                    txtTenUs.Text = uss.UserName;
                    txtEml.Text = uss.Email;
                    

                    cbbDvi.SelectedValue = uss.DonViID.Value;
                    

                    cbbLop.SelectedValue = uss.LopID.Value;
                    

                    cbbrole.SelectedValue = uss.RolesID.Value;
                    
                }
            }
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LuuUser();
            XtraMessageBox.Show("Đã Lưu!");
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditUser();
            XtraMessageBox.Show("Đã Cập nhật!");
            this.Close();
        }
        void LuuUser()
        {
            User sv = new User()
            {
                //UserID = Convert.ToInt32(txtUserID.Text),
                UserName = txtTenUs.Text,
                Email = txtEml.Text,
                //Password = txtpassword.Text,
                LopID = Convert.ToInt32(cbbLop.SelectedValue.ToString()),
                DonViID = Convert.ToInt32(cbbDvi.SelectedValue.ToString()),
                RolesID = Convert.ToInt32(cbbrole.SelectedValue.ToString())
            };
            db.Users.Add(sv);
            db.SaveChanges();
        }
        void EditUser()
        {
            var usx = db.Users.Where(p => p.UserID == ID).SingleOrDefault();
            usx.UserName = txtTenUs.Text;
            //usx.Password = txtpassword.Text;
            usx.Email = txtEml.Text;
            usx.DonViID = Convert.ToInt32(cbbDvi.SelectedValue.ToString());
            usx.LopID = Convert.ToInt32(cbbLop.SelectedValue.ToString());
            usx.RolesID = Convert.ToInt32(cbbrole.SelectedValue.ToString());
            db.SaveChanges();
        }
        void XoaUser()
        {
            //string st = txtTenUs.Text;
            //User sv = db.Users.FirstOrDefault(p => p.UserName == Text);
            //db.Users.Remove(sv);
            //db.SaveChanges();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //XoaUser();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //ComboBox cb1 = sender as ComboBox;
            //if (cb1.SelectedValue != null)
            //{
            //    DonVi donVi = cb1.SelectedValue as DonVi;
            //    textdonvi.Text = donVi.DonViID.ToString();
            //}
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            //ComboBox cb2 = sender as ComboBox;
            //if (cb2.SelectedValue != null)
            //{
            //    Lop lop = cb2.SelectedValue as Lop;
            //    textLopId.Text = lop.LopID.ToString();
            //}
        }
        private void DanhSachDonVi()
        {
            List<DonVi> lstdonvi = db.DonVis.ToList();            
            cbbDvi.DisplayMember = "TenDonVi";
            cbbDvi.ValueMember = "DonViID";
            cbbDvi.DataSource = lstdonvi;
        }
        //private void DanhSachLop()
        //{
        //    List<Lop> lstlop = db.Lops.ToList();
        //    cbbLop.DisplayMember = "TenLop";
        //    cbbLop.ValueMember = "LopID";
        //    cbbLop.DataSource = lstlop;
        //}
        private void Danhsachvaitro()
        {
            List<Role> lstvaitro = db.Roles.ToList();
            cbbrole.DisplayMember = "TenRole";
            cbbrole.ValueMember = "RolesID";
            cbbrole.DataSource = lstvaitro;
        }

        private void frmUser_Load(object sender, EventArgs e)
        {            
            
            DanhSachDonVi();
            //DanhSachLop();
            Danhsachvaitro();
            laydata();
        }

        private void cbbrole_SelectedValueChanged(object sender, EventArgs e)
        {
            //ComboBox cb3 = sender as ComboBox;
            //if (cb3.SelectedValue != null)
            //{
            //    Role role = cb3.SelectedValue as Role;
            //    txtrole.Text = role.RolesID.ToString();
            //}
        }

        private void cbbDvi_SelectedIndexChanged(object sender, EventArgs e)
        {
            int DonViID;
            bool chuyen = Int32.TryParse(cbbDvi.SelectedValue.ToString(), out DonViID);
            var lop = from lp in db.Lops where lp.DonViID.Value.Equals(DonViID) select new { lp.LopID, lp.TenLop };
            var donvi = lop.ToList();
            if (donvi.Count > 0)
            {
                cbbLop.DataSource = donvi;
                cbbLop.DisplayMember = "TenLop";
                cbbLop.ValueMember = "LopID";
            }
        }
    }
}