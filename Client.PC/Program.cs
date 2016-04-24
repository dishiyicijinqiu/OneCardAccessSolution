using FengSharp.OneCardAccess.Common;
using System;

namespace FengSharp.OneCardAccess.Client.PC
{
    class Program
    {
        static System.Threading.Mutex RunMutex;
        [STAThread]
        public static void Main(string[] args)
        {
            bool isNotRun = false;
            RunMutex = new System.Threading.Mutex(true, "OneCardAccess.Client.PC", out isNotRun);
            if (!isNotRun) return;
            App app = new App();
            app.Run();
        }
    }
}
