using CefSharp;
using CefSharp.WinForms;
using Microsoft.VisualBasic.Logging;
using System.Windows.Forms;

namespace assistantWin
{
    public partial class Form1 : Form
    {
        private readonly ChromiumWebBrowser browser;

        public Form1()
        {
            InitializeComponent();

            Text = "CefSharp";
            WindowState = FormWindowState.Maximized;

            var setting = new CefSettings();
            setting.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.102 Safari/537.36";
            CefSharp.Cef.Initialize(setting);


            browser = new ChromiumWebBrowser("www.baidu.com")
            {
                Dock = DockStyle.Fill,
            };
            toolStripContainer.ContentPanel.Controls.Add(browser);

            browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            browser.LoadingStateChanged += OnLoadingStateChanged;
            browser.ConsoleMessage += OnBrowserConsoleMessage;
            browser.StatusMessage += OnBrowserStatusMessage;
            browser.TitleChanged += OnBrowserTitleChanged;
            browser.AddressChanged += OnBrowserAddressChanged;
            browser.FrameLoadEnd += browser_FrameLoadEnd;

            this.FormClosing += Form1_FormClosing;

            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            var version = String.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}, Environment: {3}", Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion, bitness);
            DisplayOutput(version);
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            browser.Dispose();
            Cef.Shutdown();
            Environment.Exit(0);
        }

        async void browser_FrameLoadEnd(object? sender, FrameLoadEndEventArgs e)
        {
            //Log.WriteLog("browser_FrameLoadEnd:" + e.Url);

            var result = await browser.GetSourceAsync();
        }

        private void OnBrowserAddressChanged(object? sender, AddressChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }

        private void OnBrowserTitleChanged(object? sender, TitleChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
        }

        private void OnBrowserStatusMessage(object? sender, StatusMessageEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }

        private void OnBrowserConsoleMessage(object? sender, ConsoleMessageEventArgs args)
        {
            DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnLoadingStateChanged(object? sender, LoadingStateChangedEventArgs args)
        {
            SetCanGoBack(args.CanGoBack);
            SetCanGoForward(args.CanGoForward);

            this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));

            if (args.IsLoading == false)
            {
                browser.ExecuteScriptAsyncWhenPageLoaded("alert('All Resources Have Loaded');");
            }
        }

        private void OnIsBrowserInitializedChanged(object? sender, EventArgs e)
        {

            //if (e.IsBrowserInitialized)
            //{
            //    var b = ((ChromiumWebBrowser)sender);

            //    this.InvokeOnUiThreadIfRequired(() => b.Focus());
            //}
        }

        private void SetIsLoading(bool isLoading)
        {
            goButton.Text = isLoading ?
                "Stop" :
                "Go";
            goButton.Image = isLoading ?
                Properties.Resources.nav_plain_red :
                Properties.Resources.nav_plain_green;

            HandleToolStripLayout();
        }

        private void HandleToolStripLayout(object sender, LayoutEventArgs e)
        {
            HandleToolStripLayout();
        }

        private void HandleToolStripLayout()
        {
            var width = toolStrip1.Width;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item != urlTextBox)
                {
                    width -= item.Width - item.Margin.Horizontal;
                }
            }
            urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 18);
        }

        private void SetCanGoBack(bool canGoBack)
        {
            this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = canGoBack);
        }

        private void SetCanGoForward(bool canGoForward)
        {
            this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Environment.Is64BitProcess ? "x64" : "x32");
        }

        public void DisplayOutput(string output)
        {
            this.InvokeOnUiThreadIfRequired(() => outputLabel.Text = output);
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            LoadUrl(urlTextBox.Text);
        }

        private void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                browser.Load(url);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            browser.Back();
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            browser.Forward();
        }

        private void ShowDevToolsMenuItem_Click(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            browser.Dispose();
            Cef.Shutdown();
            Environment.Exit(0);
        }
    }
}