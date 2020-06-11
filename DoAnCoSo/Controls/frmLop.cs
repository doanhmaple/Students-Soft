using DevExpress.XtraEditors;
using DoAnCoSo.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace DoAnCoSo
{
    public partial class frmLop : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();
        public frmLop()
        {
            InitializeComponent();
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //btnLuu
            ThemLop();
            XtraMessageBox.Show("Đã Lưu!");
            this.Close();
        }

        void ThemLop()
        {
            Lop Lop = new Lop()
            {
                TenLop = textEdit14.Text,
                DonViID = Convert.ToInt32(boxMaDV.SelectedValue.ToString())
                //Convert.ToInt32(cbbMaDV)
            };
            db.Lops.Add(Lop);
            db.SaveChanges();
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditLop();
            XtraMessageBox.Show("Đã Cập nhật");
            this.Close();
        }
        public int ID = -1;
        private void laydata()
        {
            if (ID > 0)
            {
                barButtonItem1.Enabled = false;
                var lop = db.Lops.Where(p => p.LopID == ID).SingleOrDefault();

                if (lop != null)
                {
                    textEdit14.Text = lop.TenLop;
                    
                    
                    boxMaDV.SelectedValue = lop.DonViID.Value;
                }
            }
        }
        private void boxMaDV_SelectedValueChanged(object sender, EventArgs e)
        {
            //ComboBox cb = sender as ComboBox;
            //if (cb.SelectedValue != null)
            //{
            //    DonVi donVi = cb.SelectedValue as DonVi;
            //    txtDonViID.Text = donVi.DonViID.ToString();
            //}
        }
        void EditLop()
        {
            var lop = db.Lops.Where(p => p.LopID == ID).SingleOrDefault();
            //int id = Convert.ToInt32(DanhMucData.SelectedCells[0].OwningRow.Cells["DanhMucID"].Value.ToString());
            //DanhMuc danhMuc = db.DanhMucs.Find(id);
            lop.DonViID = Convert.ToInt32(boxMaDV.SelectedValue.ToString());
            lop.TenLop = textEdit14.Text;
            ////danhMuc.DonViID = Convert.ToInt32(txtDonViID.Text);
            db.SaveChanges();
        }
        private void DanhSachDonVi()
        {
            List<DonVi> lstdonvi = db.DonVis.ToList();
            //truyxuatdata.Intance.DonVis.ToList();
            //lstdonvi.Insert(0, new DonVi() { TenDonVi = "--- Chọn đơn vị---" });
            boxMaDV.DisplayMember = "TenDonVi";
            boxMaDV.ValueMember = "DonViID";
            boxMaDV.DataSource = lstdonvi;
        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            DanhSachDonVi();
            
            laydata();
        }
    }
}