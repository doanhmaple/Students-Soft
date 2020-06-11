using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DoAnCoSo.Controls
{
    public partial class mdlRole : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();
        public mdlRole()
        {
            InitializeComponent();
        }
        void loaddatarole()
        {
            gridControl1.DataSource = db.Roles.ToList();
        }
        private void btnthemrole_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmRole role = new frmRole();
            role.ShowDialog();
            loaddatarole();
        }

        private void btnsuarole_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row_index = gridView1.FocusedRowHandle;
            string col_field_name = "RolesID";
            object value = gridView1.GetRowCellValue(row_index, col_field_name);
            if (value != null)
            {
                frmRole rl = new frmRole();
                rl.ID = (int)value;
                rl.ShowDialog();
                loaddatarole();

            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn đối tượng để chỉnh sửa ", "cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            loaddatarole();
        }

        private void Role_Load(object sender, EventArgs e)
        {
            loaddatarole();
        }

        private void btnxoarole_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn muốn xóa dòng dữ liệu đang chọn không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int row_index = gridView1.FocusedRowHandle;
                string col_field_name = "RolesID";
                object value = gridView1.GetRowCellValue(row_index, col_field_name);
                if (value != null)
                {
                    var rl = db.Roles.Where(p => p.RolesID == (int)value).SingleOrDefault();
                    if (rl != null)
                    {
                        db.Roles.Remove(rl);
                        db.SaveChanges();
                        XtraMessageBox.Show("Đã xóa thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loaddatarole();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn đối tượng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialogExcel = new SaveFileDialog();
            saveFileDialogExcel.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (saveFileDialogExcel.ShowDialog() == DialogResult.OK)
            {
                string exportFilePath = saveFileDialogExcel.FileName;
                loaddatarole();
                gridControl1.ExportToXlsx(exportFilePath);
            }
        }
    }
}