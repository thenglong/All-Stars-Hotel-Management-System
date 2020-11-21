using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace All_Stars_Hotel.FORM
{
    static class Program
    {
        // Mutex can be made static so that GC doesn't recycle
        // Same effect with GC.KeepAlive(mutex)
        // "All Stars Hotel" is a unique value
        static Mutex mutex = new Mutex(false, "All Stars Hotel");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Wait a few seconds in case that the instance is just 
            // shutting down
            if (!mutex.WaitOne(TimeSpan.FromMilliseconds(300), false))
            {
                MessageBox.Show("All Stars Hotel already started!", "All Stars Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormLogin());
            }
            finally { mutex.ReleaseMutex(); }
        }
    }
}
