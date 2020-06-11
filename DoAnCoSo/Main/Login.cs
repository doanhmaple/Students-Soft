using DevExpress.XtraEditors;
using DoAnCoSo.Main;
using DoAnCoSo.Table;
using DoAnCoSo.ViewTable;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;

namespace DoAnCoSo
{
    public partial class Login : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        database db = new database();
        public Login()
        {
            InitializeComponent();
        }
        public User UserInfo { get; set; }
        private static Login _instance;
        public static Login Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Login();
                return _instance;
            }
        }

        public string searchString { get; private set; }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                XtraMessageBox.Show("Xin hãy nhập Email.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return;
                
            }
            if (checkBox1.Checked == true)
            {
                Properties.Settings.Default.un = txtUserName.Text;
                Properties.Settings.Default.pass = txtpassword.Text;
                Properties.Settings.Default.Save();

                mail();
                
                
            }

            else if (checkBox1.Checked == false)
            {
                mail();
            }

            else if (txtUserName.Text == Properties.Settings.Default.un && txtpassword.Text == Properties.Settings.Default.pass)
            {
                try
                {
                    var query = (from o in db.Users
                                 join a in db.Roles on o.RolesID equals a.RolesID
                                 where o.Email == txtUserName.Text 
                                 select o).SingleOrDefault();

                    if (query != null)
                    {
                        UserInfo = query;
                        MessageBox.Show("Bạn đã đăng nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Main1 ad = new Main1();
                        ad.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Bạn nhập sai tài khoản hoặc mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
        
        private void materialSingleLineTextField1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)//Enter key
                txtpassword.Focus();
        }

        private void txtpassword_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                btnLogin.PerformClick();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            _instance = this;
        }
        void mail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("1714234@dlu.edu.vn");
                mail.To.Add("1714234@dlu.edu.vn");
                mail.Subject = "Verification";
                mail.Body = $"Đăng nhập thành công {DateTime.Now}";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(txtUserName.Text, txtpassword.Text);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                var query = (from o in db.Users
                             join a in db.Roles on o.RolesID equals a.RolesID
                             where o.Email == txtUserName.Text
                             select o).SingleOrDefault();

                if (query != null)
                {
                    UserInfo = query;
                    MessageBox.Show("Bạn đã đăng nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Main1 ad = new Main1();
                    ad.Show();
                    this.Close();
                }
                

            }

            catch (Exception ex)
            {
                MessageBox.Show("Bạn nhập sai tài khoản hoặc mật khẩu");
            }
        }
    }
}
