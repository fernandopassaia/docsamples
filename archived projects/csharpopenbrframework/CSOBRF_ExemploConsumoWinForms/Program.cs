using System;
using System.Windows.Forms;

namespace CSOBRF_ExemploConsumoWinForms
{
    static class Program
    {        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmConsumoWinForms());
        }
    }
}
