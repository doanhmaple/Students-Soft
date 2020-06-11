using DevExpress.XtraEditors;
using DoAnCoSo.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCoSo
{
    public partial class frmHocVu : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();
        public frmHocVu()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //LuuHV();
            XtraMessageBox.Show("Đã Lưu!");
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edithocvu();
            XtraMessageBox.Show("Đã Cập nhật!");
            this.Close();
        }

        private void textTinhtrang_CheckedChanged(object sender, EventArgs e)
        {

        }
        public int ID = -1;
        private void laydata()
        {
            if (ID > 0)
            {
                barButtonItem1.Enabled = false;
                var hocvu = db.HocVus.Where(p => p.HocVuID == ID).SingleOrDefault();

                if (hocvu != null)
                {
                    dateTimePicker1.Value = hocvu.NgayTao;
                    dateTimePicker2.Value = hocvu.NgayHen;
                    textNoidung.Text = hocvu.NoiDung;
                    checkEdit1.EditValue = hocvu.TinhTrang;
                    comboBox2.SelectedValue = hocvu.DonViID.Value;
                    comboBox1.SelectedValue = hocvu.DanhMucID.Value;
                }
            }
        }
        private void DanhSachDonVi()
        {
            List<DonVi> lstdonvi = db.DonVis.ToList();
            //truyxuatdata.Intance.DonVis.ToList();
            //lstdonvi.Insert(0, new DonVi() { TenDonVi = "--- Chọn đơn vị---" });
            comboBox2.DisplayMember = "TenDonVi";
            comboBox2.ValueMember = "DonViID";
            comboBox2.DataSource = lstdonvi;
        }

        private void frmHocVu_Load(object sender, EventArgs e)
        {
            DanhSachDonVi();
            laydata();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int DonViID;
            bool chuyen = Int32.TryParse(comboBox2.SelectedValue.ToString(), out DonViID);
            var danhmuc = from dm in db.DanhMucs where dm.DonViID.Value.Equals(DonViID) select new { dm.DanhMucID, dm.TenDanhMuc };
            var donvi = danhmuc.ToList();
            if (donvi.Count > 0)
            {
                comboBox1.DataSource = donvi;
                comboBox1.DisplayMember = "TenDanhMuc";
                comboBox1.ValueMember = "DanhmucID";
            }
        }
        void Edithocvu()
        {
            var hocvu = db.HocVus.Where(p => p.HocVuID == ID).SingleOrDefault();
            hocvu.NgayHen = dateTimePicker2.Value;
            hocvu.NoiDung = textNoidung.Text;
            hocvu.TinhTrang = Convert.ToBoolean(checkEdit1.EditValue.ToString());
            hocvu.DonViID = Convert.ToInt32(comboBox2.SelectedValue.ToString());
            hocvu.DanhMucID = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            
            db.SaveChanges();
        }
    }
}