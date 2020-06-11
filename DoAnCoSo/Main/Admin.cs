using DoAnCoSo.Controls;
using DoAnCoSo.Modules;
using DoAnCoSo.Table;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DoAnCoSo
{
    public partial class Admin : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            //lblogin.Text = $"Login: {Login.Instance.UserInfo.UserName}";
            //lschucvu.Text = $"Chức vụ: {Login.Instance.UserInfo.RolesID}";
            
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
        private void bbtXemHV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form FormHocVu = kiemtraform(typeof(frmSS));
            if (FormHocVu == null)
            {
                frmSS frm = new frmSS();
                frm.MdiParent = this;
                frm.Show();
                frm.Text = "Học vụ";
            }
            else
            {
                FormHocVu.Activate();
            }
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmHocVu hv = new frmHocVu();
            hv.Show();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDanhMuc dm = new frmDanhMuc();
            
            dm.Show();
        }
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form FromDanhMuc = kiemtraform(typeof(mdlDanhmuc));
            if (FromDanhMuc == null)
            {
                mdlDanhmuc dmuc = new mdlDanhmuc();
                dmuc.MdiParent = this;
                dmuc.Show();
                dmuc.Text = "Danh mục";
                
            }
            else
            {
                FromDanhMuc.Activate();
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDonVi dv = new frmDonVi();
            dv.Show();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form FormDonVi = kiemtraform(typeof(mdlDonvi));
            if (FormDonVi == null)
            {
                mdlDonvi donviCtrl = new mdlDonvi();
                donviCtrl.MdiParent = this;
                donviCtrl.Show();
                donviCtrl.Text = "Đơn vị";
            }
            else
            {
                FormDonVi.Activate();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmUser us = new frmUser();
            //us.Show();
        }
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form FormUser = kiemtraform(typeof(mdlUser));
            if (FormUser == null)
            {
                mdlUser usctrl = new mdlUser();
                usctrl.MdiParent = this;
                usctrl.Show();
                usctrl.Text = "Người dùng";
            }
            else
            {
                FormUser.Activate();
            }
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form FormLop = kiemtraform(typeof(mdlLop));
            if (FormLop == null)
            {
                mdlLop lopctrl = new mdlLop();
                lopctrl.MdiParent = this;
                lopctrl.Show();
                lopctrl.Text = "Lớp";
            }
            else
            {
                FormLop.Activate();
            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmLop lp = new frmLop();
            lp.Show();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form role = kiemtraform(typeof(mdlRole));
            if (role == null)
            {
                mdlRole rolectrl = new mdlRole();
                rolectrl.MdiParent = this;
                rolectrl.Show();
                rolectrl.Text = "Roles";
            }
            else
            {
                role.Activate();
            }
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form function = kiemtraform(typeof(mdlFunction));
            if (function == null)
            {
                mdlFunction fctrl = new mdlFunction();
                fctrl.MdiParent = this;
                fctrl.Show();
                fctrl.Text = "Chức năng";
            }
            else
            {
                function.Activate();
            }
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form fr = kiemtraform(typeof(mdlFunctionRole));
            if (fr == null)
            {
                mdlFunctionRole frctrl = new mdlFunctionRole();
                frctrl.MdiParent = this;
                frctrl.Show();
                frctrl.Text = "Chức năng của từng role";
            }
            else
            {
                fr.Activate();
            }
        }
    }
}