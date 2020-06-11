using DevExpress.XtraEditors;
using DoAnCoSo.Table;
using DoAnCoSo.ViewTable;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DoAnCoSo.Modules
{
    public partial class mdlLop : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();

        public string searchString { get; private set; }

        public mdlLop()
        {
            InitializeComponent();
        }

        private void mdlLop_Load(object sender, EventArgs e)
        {
            loaddatalop();
            //gridControl1.DataSource = db.Lops.ToList();
        }
        void loaddatalop()
        {
            var model = from a in db.Lops
                        join b in db.DonVis on a.DonViID equals b.DonViID
                        select new lopview()
                        {
                            LopID = a.LopID,
                            TenLop = a.TenLop,
                            TenDonVi = b.TenDonVi,

                        };
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenLop.Contains(searchString) || x.TenDonVi.Contains(searchString));
            }
            gridControl1.DataSource = model.ToList();//OrderBy(x => x.DanhMucID);


            //gridControl1.DataSource = db.Lops.ToList();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmLop lop = new frmLop();
            lop.ShowDialog();
            loaddatalop();
        }
        
        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn muốn xóa dòng dữ liệu đang chọn không?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int row_index = gridView1.FocusedRowHandle;
                string col_field_name = "LopID";
                object value = gridView1.GetRowCellValue(row_index, col_field_name);
                if (value != null)
                {
                    var lop = db.Lops.Where(p => p.LopID == (int)value).SingleOrDefault();
                    if (lop != null)
                    {
                        db.Lops.Remove(lop);
                        db.SaveChanges();
                        XtraMessageBox.Show("Đã xóa thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loaddatalop();
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
            string col_field_name = "LopID";
            object value = gridView1.GetRowCellValue(row_index, col_field_name);
            if (value != null)
            {
                frmLop lop = new frmLop();

                lop.ID = (int)value;
                lop.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn đối tượng để chỉnh sửa ", "cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            loaddatalop();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialogExcel = new SaveFileDialog();
            saveFileDialogExcel.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (saveFileDialogExcel.ShowDialog() == DialogResult.OK)
            {
                string exportFilePath = saveFileDialogExcel.FileName;
                loaddatalop();
                gridControl1.ExportToXlsx(exportFilePath);
            }
        }
    }
}