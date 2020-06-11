using DevExpress.XtraEditors;
using DoAnCoSo.Table;
using System;
using System.Linq;

namespace DoAnCoSo
{
    public partial class frmDonVi : DevExpress.XtraEditors.XtraForm
    {
        public int ID = -1;
        database db = new database();
        public frmDonVi()
        {
            InitializeComponent();
        }
        private void laydata()
        {
            if (ID > 0)
            {
                themdonvi.Enabled = false;
                var donvi = db.DonVis.Where(p => p.DonViID == ID).SingleOrDefault();

                if (donvi != null)
                {
                    textEdit14.Text = donvi.TenDonVi;                    
                }
            }
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LuuDonVi();
            XtraMessageBox.Show("Đã Lưu!");
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditDonVi();
            XtraMessageBox.Show("Đã Cập nhật!");
            this.Close();
        }
        void LuuDonVi()
        {
            DonVi dv = new DonVi()
            {
                TenDonVi = textEdit14.Text,
            };
            db.DonVis.Add(dv);
            db.SaveChanges();
        }
        void EditDonVi()
        {
            var donvi = db.DonVis.Where(p => p.DonViID == ID).SingleOrDefault();
            donvi.TenDonVi = textEdit14.Text;
            db.SaveChanges();
        }

        private void frmDonVi_Load(object sender, EventArgs e)
        {
            laydata();            
        }
    }
}