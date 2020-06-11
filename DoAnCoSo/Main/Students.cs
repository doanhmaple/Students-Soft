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
    public partial class Students : DevExpress.XtraEditors.XtraForm
    {
        database db = new database();
        
        public string searchString { get; private set; }
        

        public Students()
        {
            InitializeComponent();
        }

        private void Students_Load(object sender, EventArgs e)
        {
            
            DanhSachDonVi();
            ngaytao.Enabled = false;
            lop.Enabled = false;
            tensv.Enabled = false;
            masosv.Enabled = false;
            
            tensv.Text = $"{Login.Instance.UserInfo.UserName}";
            lop.Text = $"{Login.Instance.UserInfo.Lop.TenLop}";
            masosv.Text = $"{Login.Instance.UserInfo.UserID}";
            loaddataHocVu();
        }

        private void Students_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            //Application.Exit();
        }

        private void tensv_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void DanhSachDonVi()
        {
            List<DonVi> lstdonvi = db.DonVis.ToList();
            comboBox2.DisplayMember = "TenDonVi";
            comboBox2.ValueMember = "DonViID";
            comboBox2.DataSource = lstdonvi;
        }
        void loaddataHocVu()
        {
            var model = from a in db.HocVus
                        join b in db.DonVis on a.DonViID equals b.DonViID
                        join c in db.Users on a.UserID equals c.UserID where(c.UserID == Login.Instance.UserInfo.UserID)
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
                model = model.Where(x => x.UserName.Contains(searchString) || x.TenDonVi.Contains(searchString) || x.TenDanhMuc.Contains(searchString));
            }
            gridControl1.DataSource = model.ToList();
        }
        void dangkyhocvu()
        {

            HocVu hv = new HocVu()
            {
                UserID = Convert.ToInt32(masosv.Text),
                DonViID = Convert.ToInt32(comboBox2.SelectedValue.ToString()),
                DanhMucID = Convert.ToInt32(comboBox1.SelectedValue.ToString()),
                TinhTrang = false,
                NgayTao = DateTime.Now,
                NgayHen = DateTime.Today.AddDays(7),
                NoiDung = noidung.Text

            };
            db.HocVus.Add(hv);
            db.SaveChanges();
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn muốn đăng ký không?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { 
                dangkyhocvu();
                XtraMessageBox.Show($"Đã đăng ký. Ngày nhận giấy {DateTime.Today.AddDays(7)}");
                loaddataHocVu();                
            }
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
        
    }
}