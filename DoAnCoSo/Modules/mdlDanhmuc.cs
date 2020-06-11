using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using DoAnCoSo.ViewTable;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DoAnCoSo.Table;
using System.Windows.Controls;

namespace DoAnCoSo.Modules
{
    public partial class mdlDanhmuc : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();

        public string searchString { get; private set; }

        public mdlDanhmuc()
        {
            InitializeComponent();
        }

        
        private void loaddatadanhmuc()
        {
            //loaddatadanhmuc();
            var model = from a in db.DanhMucs
                        join b in db.DonVis on a.DonViID equals b.DonViID
                        select new danhmucview()
                        {
                            DanhMucID = a.DanhMucID,
                            TenDanhMuc = a.TenDanhMuc,
                            TenDonVi = b.TenDonVi,

                        };
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenDanhMuc.Contains(searchString) || x.TenDonVi.Contains(searchString));
            }
            gridControl.DataSource = model.ToList();//OrderBy(x => x.DanhMucID);
            
        }
        private void btnThemDanhMuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row_index = gridView1.FocusedRowHandle;
            string col_field_name = "DanhMucID";
            object value = gridView1.GetRowCellValue(row_index, col_field_name);
            if (value != null)
            {
                frmDanhMuc dm = new frmDanhMuc();
                
                dm.ID = (int)value;
                dm.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn đối tượng để chỉnh sửa ", "cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            loaddatadanhmuc();
        }

        

        private void mdlDanhmuc_Load(object sender, EventArgs e)
        {
            loaddatadanhmuc();
        }

        private void btnThemDanhMuc_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDanhMuc dm = new frmDanhMuc();
            dm.ShowDialog();
            loaddatadanhmuc();
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {
            //string ten = gridControl.SelectRows[0].Cells[1].Value.ToString();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(XtraMessageBox.Show("Bạn muốn xóa dòng dữ liệu đang chọn không?","Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int row_index = gridView1.FocusedRowHandle;
                string col_field_name = "DanhMucID";
                object value = gridView1.GetRowCellValue(row_index, col_field_name);
                if (value != null)
                {
                    var danhmuc = db.DanhMucs.Where(p => p.DanhMucID == (int)value).SingleOrDefault();
                    if (danhmuc != null)
                    {
                        db.DanhMucs.Remove(danhmuc);
                        db.SaveChanges();
                        XtraMessageBox.Show("Đã xóa thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loaddatadanhmuc();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn đối tượng để xóa ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void gridControl_Click(object sender, EventArgs e)
        {
            
        }

        private void barHeaderItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //txtten.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Tên Danh Mục").ToString();
            
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialogExcel = new SaveFileDialog();
            saveFileDialogExcel.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (saveFileDialogExcel.ShowDialog() == DialogResult.OK)
            {
                string exportFilePath = saveFileDialogExcel.FileName;
                loaddatadanhmuc();
                gridControl.ExportToXlsx(exportFilePath);
            }
        }
    }
}