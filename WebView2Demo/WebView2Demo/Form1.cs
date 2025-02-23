using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace WebView2Demo
{
    public partial class Form1 : Form
    {
        private string initialUrl;
        private TextBox urlTextBox;
        private Button goButton;
        private Button backButton;
        private Button forwardButton;
        private Button devToolsButton;

        public Form1(string url)
        {
            InitializeComponent();
            initialUrl = url;
            InitializeControls();
            InitializeWebView();
        }

        private void InitializeControls()
        {
            // URLテキストボックスの設定
            urlTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(400, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // 移動ボタンの設定
            goButton = new Button
            {
                Text = "Go",
                Location = new System.Drawing.Point(420, 10),
                Size = new System.Drawing.Size(50, 23),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // 戻るボタンの設定
            backButton = new Button
            {
                Text = "←",
                Location = new System.Drawing.Point(480, 10),
                Size = new System.Drawing.Size(30, 23),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // 進むボタンの設定
            forwardButton = new Button
            {
                Text = "→",
                Location = new System.Drawing.Point(520, 10),
                Size = new System.Drawing.Size(30, 23),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // 開発者ツールボタンの設定
            devToolsButton = new Button
            {
                Text = "Dev Tools",
                Location = new System.Drawing.Point(560, 10),
                Size = new System.Drawing.Size(70, 23),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // WebViewの位置とサイズを調整
            webView.Location = new System.Drawing.Point(10, 40);
            webView.Size = new System.Drawing.Size(this.ClientSize.Width - 20, this.ClientSize.Height - 50);
            webView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // イベントハンドラの設定
            goButton.Click += GoButton_Click;
            backButton.Click += BackButton_Click;
            forwardButton.Click += ForwardButton_Click;
            devToolsButton.Click += DevToolsButton_Click;
            urlTextBox.KeyPress += UrlTextBox_KeyPress;

            // コントロールの追加
            this.Controls.AddRange(new Control[] { 
                urlTextBox, 
                goButton, 
                backButton, 
                forwardButton, 
                devToolsButton 
            });
        }

        private async void InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.Source = new Uri(initialUrl);
            urlTextBox.Text = initialUrl;

            // WebViewのナビゲーションイベントを設定
            webView.NavigationCompleted += WebView_NavigationCompleted;
        }

        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            urlTextBox.Text = webView.Source.ToString();
            backButton.Enabled = webView.CanGoBack;
            forwardButton.Enabled = webView.CanGoForward;
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            NavigateToUrl();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
                webView.GoBack();
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            if (webView.CanGoForward)
                webView.GoForward();
        }

        private void DevToolsButton_Click(object sender, EventArgs e)
        {
            webView.CoreWebView2.OpenDevToolsWindow();
        }

        private void UrlTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                NavigateToUrl();
            }
        }

        private void NavigateToUrl()
        {
            try
            {
                var url = urlTextBox.Text;
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                {
                    url = "https://" + url;
                }
                webView.Source = new Uri(url);
            }
            catch (UriFormatException)
            {
                MessageBox.Show("無効なURLです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
