using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PowerPlanChanger
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
            Application.Run(new LogoForm(1000, 1500));
            Application.Run(new APP());
        }
    }
}
