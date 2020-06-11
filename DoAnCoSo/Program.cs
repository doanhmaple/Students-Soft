using System;
using System.Windows.Forms;

namespace DoAnCoSo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login login = new Login();
            login.Show();
            Application.Run();

            //Application.Run(new Admin());
        }
    }
}
