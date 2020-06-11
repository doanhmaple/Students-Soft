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
    public partial class mdlFunction : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();
        public mdlFunction()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmFunction function = new frmFunction();
            function.ShowDialog();
            loaddatafunction();
        }
        void loaddatafunction()
        {
            gridControl1.DataSource = db.Functions.ToList();
            
        }

        private void Function_Load(object sender, EventArgs e)
        {
            loaddatafunction();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row_index = gridView1.FocusedRowHandle;
            string col_field_name = "FunctionID";
            object value = gridView1.GetRowCellValue(row_index, col_field_name);
            if (value != null)
            {
                frmFunction f = new frmFunction();
                f.ID = (int)value;
                f.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn đối tượng để chỉnh sửa ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            loaddatafunction();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn muốn xóa dòng dữ liệu đang chọn không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int row_index = gridView1.FocusedRowHandle;
                string col_field_name = "FunctionID";
                object value = gridView1.GetRowCellValue(row_index, col_field_name);
                if (value != null)
                {
                    var rl = db.Functions.Where(p => p.FunctionID == (int)value).SingleOrDefault();
                    if (rl != null)
                    {
                        db.Functions.Remove(rl);
                        db.SaveChanges();
                        XtraMessageBox.Show("Đã xóa thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loaddatafunction();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn đối tượng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}