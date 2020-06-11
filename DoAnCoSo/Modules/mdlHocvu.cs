using DevExpress.XtraEditors;
using DoAnCoSo.ViewTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DoAnCoSo
{
    public partial class frmSS : DevExpress.XtraEditors.XtraForm
    {
        public database db = new database();

        public frmSS()
        {
            InitializeComponent();
        }

        public string searchString { get; private set; }

        private void frmSS_Load(object sender, EventArgs e)
        {
            
            loaddataHocVu();
        }
        void loaddataHocVu()
        {
            var model = from a in db.HocVus
                        join b in db.DonVis on a.DonViID equals b.DonViID
                        join c in db.Users on a.UserID equals c.UserID
                        join d in db.DanhMucs on a.DanhMucID equals d.DanhMucID
                        select new HocVuview()
                        {
                            HocVuID = a.HocVuID,
                            NgayTao = a.NgayTao,
                            NoiDung = a.NoiDung,
                            TinhTrang = a.TinhTrang,
                            ParentID = a.ParentID,
                            NgayHen = a.NgayHen,

                            UserName = c.UserName,
                            TenDanhMuc = d.TenDanhMuc,

                            TenDonVi = b.TenDonVi,

                        };
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.TenDonVi.Contains(searchString));
            }
            gridControl1.DataSource = model.ToList();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialogExcel = new SaveFileDialog();
            saveFileDialogExcel.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (saveFileDialogExcel.ShowDialog() == DialogResult.OK)
            {
                string exportFilePath = saveFileDialogExcel.FileName;
                loaddataHocVu();
                gridControl1.ExportToXlsx(exportFilePath);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row_index = gridView1.FocusedRowHandle;
            string col_field_name = "HocVuID";
            object value = gridView1.GetRowCellValue(row_index, col_field_name);
            if (value != null)
            {
                frmHocVu hocvu = new frmHocVu();

                hocvu.ID = (int)value;
                hocvu.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn đối tượng để chỉnh sửa ", "cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            loaddataHocVu();
        }
    }
}