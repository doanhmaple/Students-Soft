using DevExpress.XtraEditors;
using DoAnCoSo.Table;
using DoAnCoSo.ViewTable;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DoAnCoSo.Modules
{
    public partial class mdlDonvi : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();
        public mdlDonvi()
        {
            InitializeComponent();
        }

        private void mdlDonvi_Load(object sender, EventArgs e)
        {
            loaddatadonvi();            
        }
        void loaddatadonvi()
        {
            var result = from a in db.DonVis
                         select new donviView()
                         {
                             TenDonVi = a.TenDonVi,
                             DonViID = a.DonViID,
                         };
            gridControl1.DataSource = result.ToList();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDonVi dv = new frmDonVi();
            dv.ShowDialog();
            loaddatadonvi();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn muốn xóa dòng dữ liệu đang chọn không?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int row_index = gridView1.FocusedRowHandle;
                string col_field_name = "DonViID";
                object value = gridView1.GetRowCellValue(row_index, col_field_name);
                if (value != null)
                {
                    var donvi = db.DonVis.Where(p => p.DonViID == (int)value).SingleOrDefault();
                    if (donvi != null)
                    {
                        db.DonVis.Remove(donvi);
                        db.SaveChanges();
                        XtraMessageBox.Show("Đã xóa thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loaddatadonvi();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn đối tượng để xóa ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row_index = gridView1.FocusedRowHandle;
            string col_field_name = "DonViID";
            object value = gridView1.GetRowCellValue(row_index, col_field_name);
            if (value != null)
            {
                frmDonVi dm = new frmDonVi();
                dm.ID = (int)value;
                dm.ShowDialog();
                
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn đối tượng để chỉnh sửa ", "cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            loaddatadonvi();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialogExcel = new SaveFileDialog();
            saveFileDialogExcel.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (saveFileDialogExcel.ShowDialog() == DialogResult.OK)
            {
                string exportFilePath = saveFileDialogExcel.FileName;
                loaddatadonvi();
                gridControl1.ExportToXlsx(exportFilePath);
            }
        }
    }
}