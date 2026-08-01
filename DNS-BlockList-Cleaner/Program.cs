namespace DNS_BlockList_Cleaner
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // DPI-aware rendering op alle schermresoluties
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}