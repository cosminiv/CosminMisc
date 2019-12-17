using System;
using System.Windows.Forms;
using CosminIv.Games.UI.WinForms.Pool;

namespace WindowsFormsApp1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PoolMainForm());
        }
    }
}
