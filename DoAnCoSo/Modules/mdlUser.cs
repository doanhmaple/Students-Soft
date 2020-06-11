using DevExpress.XtraEditors;
using DoAnCoSo.ViewTable;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DoAnCoSo.Modules
{
    public partial class mdlUser : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();

        public string searchString { get; private set; }

        public mdlUser()
        {
            InitializeComponent();
        }

        private void mdlUser_Load(object sender, EventArgs e)
        {
            loaddatauser();
            //gridControl1.DataSource = db.Users.ToList();
        }
        void loaddatauser()
        {
            var model = from a in db.Users
                        join b in db.DonVis on a.DonViID equals b.DonViID
                        join c in db.Lops on a.LopID equals c.LopID
                        join d in db.Roles on a.RolesID equals d.RolesID 
                        select new Userview()
                        {
                            UserID = a.UserID,
                            UserName = a.UserName,
                            Email = a.Email,
                            TenLop = c.TenLop,
                            TenDonVi = b.TenDonVi,
                            TenRole = d.TenRole,
                        };
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.TenLop.Contains(searchString) || x.TenDonVi.Contains(searchString) || x.TenRole.Contains(searchString));
            }
            gridControl1.DataSource = model.ToList();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmUser user = new frmUser();
            user.ShowDialog();
            loaddatauser();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn muốn xóa dòng dữ liệu đang chọn không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int row_index = gridView1.FocusedRowHandle;
                string col_field_name = "UserID";
                object value = gridView1.GetRowCellValue(row_index, col_field_name);
                if (value != null)
                {
                    var user = db.Users.Where(p => p.UserID == (int)value).SingleOrDefault();
                    if (user != null)
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                        XtraMessageBox.Show("Đã xóa thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loaddatauser();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn đối tượng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row_index = gridView1.FocusedRowHandle;
            string col_field_name = "UserID";
            object value = gridView1.GetRowCellValue(row_index, col_field_name);
            if (value != null)
            {
                frmUser us = new frmUser();

                us.ID = (int)value;
                us.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn đối tượng để chỉnh sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            loaddatauser();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialogExcel = new SaveFileDialog();
            saveFileDialogExcel.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (saveFileDialogExcel.ShowDialog() == DialogResult.OK)
            {
                string exportFilePath = saveFileDialogExcel.FileName;
                loaddatauser();
                gridControl1.ExportToXlsx(exportFilePath);
            }
        }
    }
}