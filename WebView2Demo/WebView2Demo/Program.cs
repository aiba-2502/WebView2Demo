namespace WebView2Demo
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string url = args.Length > 0 ? args[0] : "https://www.google.com";  // デフォルトURL
            Application.Run(new Form1(url));
        }
    }
}