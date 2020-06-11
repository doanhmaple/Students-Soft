using DevExpress.XtraEditors;
using DoAnCoSo.Table;
using DoAnCoSo.ViewTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace DoAnCoSo
{
    public partial class frmFunction : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();
        public frmFunction()
        {
            InitializeComponent();
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //btnLuu
            Themfuntios();
            XtraMessageBox.Show("Đã Lưu!");
            this.Close();
        }

        void Themfuntios()
        {
            Function f = new Function()
            {
                FunctionName = textEdit14.Text,
                Formname = boxMaDV.SelectedValue.ToString()

            };
            db.Functions.Add(f);
            db.SaveChanges();
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Editfunction();
            XtraMessageBox.Show("Đã Cập nhật");
            this.Close();
        }
        public int ID = -1;
        private void laydata()
        {
            if (ID > 0)
            {
                barButtonItem1.Enabled = false;
                var function = db.Functions.Where(p => p.FunctionID == ID).SingleOrDefault();

                if (function != null)
                {
                    textEdit14.Text = function.FunctionName;
                    boxMaDV.SelectedValue = function.Formname.ToString();
                    
                }
            }
        }
        private void boxMaDV_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }
        void Editfunction()
        {
            var function = db.Functions.Where(p => p.FunctionID == ID).SingleOrDefault();
            
            function.FunctionName = textEdit14.Text;
            function.Formname = boxMaDV.SelectedValue.ToString();


            db.SaveChanges();
        }
        private void DanhSachfunctions()
        {
            List<functionview> list = new List<functionview>();
            list.Add(new functionview() { ID = "DoAnCoSo.Modules.mdlDanhmuc", FormName = "Danh Mục" });
            list.Add(new functionview() { ID = "DoAnCoSo.Modules.mdlLop", FormName = "Lớp" });
            list.Add(new functionview() { ID = "DoAnCoSo.Modules.mdlUser", FormName = "User" });
            list.Add(new functionview() { ID = "DoAnCoSo.Controls.mdlRole", FormName = "Role" });
            list.Add(new functionview() { ID = "DoAnCoSo.Students", FormName = "Học Vụ" });
            list.Add(new functionview() { ID = "DoAnCoSo.Modules.mdlDonvi", FormName = "Đơn Vị" });
            list.Add(new functionview() { ID = "DoAnCoSo.Controls.mdlFunctionRole", FormName = "Chức năng của vai trò" });
            list.Add(new functionview() { ID = "DoAnCoSo.Controls.mdlFunction", FormName = "Chức năng" });
            list.Add(new functionview() { ID = "DoAnCoSo.frmSS", FormName = "Chỉnh sửa học vụ" });
            
            boxMaDV.DataSource = list;
            boxMaDV.DisplayMember = "FormName";
            boxMaDV.ValueMember = "ID";
        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            DanhSachfunctions();
            
            laydata();
        }
    }
}