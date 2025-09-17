using System;
using System.Windows.Forms;

namespace FitnessTracker
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Required for Microsoft.Data.Sqlite on .NET Framework
            SQLitePCL.Batteries_V2.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
