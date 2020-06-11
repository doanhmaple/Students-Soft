using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using DoAnCoSo.Table;


namespace DoAnCoSo
{
    public partial class frmDanhMuc : DevExpress.XtraEditors.XtraForm
    {
        
        database db = new database();

        public string searchString { get; private set; }

        public frmDanhMuc()
        {            
            InitializeComponent();
        }
        public int ID = -1;
        private void laydata()
        {
            if (ID > 0)
            {
                barButtonItem1.Enabled = false;
                var danhmuc = db.DanhMucs.Where(p => p.DanhMucID == ID).SingleOrDefault();
                
                if (danhmuc != null)
                {
                    txtdanhmuc.Text = danhmuc.TenDanhMuc;
                    
                    cbbMaDv.SelectedValue = danhmuc.DonViID.Value;
                }
            }
        }
        private void DanhMuc_Load(object sender, EventArgs e)
        {
            
            DanhSachDonVi();
            laydata();
        }
        private void DanhSachDonVi()
        {
            List<DonVi> lstdonvi = db.DonVis.ToList();
            
            cbbMaDv.DisplayMember = "TenDonVi";
            cbbMaDv.ValueMember = "DonViID";
            cbbMaDv.DataSource = lstdonvi;
            //cbbMaDv.SelectedIndex = 2;
        }
        void Luudanhmuc()
        {
            DanhMuc dm = new DanhMuc()
            {
                TenDanhMuc = txtdanhmuc.Text,
                DonViID = Convert.ToInt32(cbbMaDv.SelectedValue.ToString())
            };
            db.DanhMucs.Add(dm);
            db.SaveChanges();
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            Luudanhmuc();
            XtraMessageBox.Show("Đã Lưu!");
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            //EditDanhMuc();
            //DanhSachDanhMuc();
            //LoadDataDanhMuc();
        }
        void EditDanhMuc()
        {
            var danhmuc = db.DanhMucs.Where(p => p.DanhMucID == ID).SingleOrDefault();
            danhmuc.DonViID = Convert.ToInt32(cbbMaDv.SelectedValue.ToString());
            danhmuc.TenDanhMuc = txtdanhmuc.Text;
            //danhmuc.DonViID = Convert.ToInt32(txtdonviid.Text);
            db.SaveChanges();
        }

        private void cbbMaDV_SelectedValueChanged(object sender, EventArgs e)
        {
            //ComboBox cb = sender as ComboBox;
            //if (cb.SelectedValue != null)
            //{
            //    DonVi donVi = cb.SelectedValue as DonVi;
            //    txtdonviid.Text = donVi.DonViID.ToString();
            //}
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditDanhMuc();
            XtraMessageBox.Show("Đã Cập nhật Danh mục!");
            this.Close();
        }
    }
}