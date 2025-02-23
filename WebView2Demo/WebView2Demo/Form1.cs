using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace WebView2Demo
{
    public partial class Form1 : Form
    {
        private string initialUrl;

        public Form1(string url)
        {
            InitializeComponent();
            initialUrl = url;
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.Source = new Uri(initialUrl);
        }
    }
}
