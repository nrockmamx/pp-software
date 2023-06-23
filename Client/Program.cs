using static Emgu.CV.VideoCapture;

namespace Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

#if DEBUG
            MasterCache.ApiUrl = "https://dev-pp-software.newryapis-nonprod.com";
#else
            MasterCache.ApiUrl = "https://pp-software.nry-apis.com"; 
#endif

            ApplicationConfiguration.Initialize();
            Application.Run(new Login());          
        }
    }
}