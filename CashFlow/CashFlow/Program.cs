using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CashFlow
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
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Type t = Type.GetType(System.Configuration.ConfigurationManager.AppSettings["Repository"]);
            Assembly a = Assembly.GetAssembly(t);
            CashFlow.DataAccess.IRepository repository = (CashFlow.DataAccess.IRepository)a.CreateInstance(t.FullName);
            Form1.Repository = repository;
            Application.Run(new Form1());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message + " " + e.Exception.StackTrace);
        }
    }
}
