using CefSharp;
using CefSharp.WinForms;
using HtmlAgilityPack;
using Microsoft.VisualBasic.Logging;
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace assistantWin
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 日志保存地址
        /// </summary>
        string rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "htmlLogs");
        /// <summary>
        /// url已抓取过的保存地址
        /// </summary>
        string usedRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "urlUsedLogs");

        /// <summary>
        /// 下载队列
        /// </summary>
        ConcurrentQueue<QueueItem> downQueue = new ConcurrentQueue<QueueItem>();

        /// <summary>
        /// 提取关键字
        /// </summary>
        List<string> keyWords = new List<string> {
            "版权",
            "版权侵权",
            "版权保护",
            "著作权",
            "知识产权",
            "自媒体侵权",
            "数字版权",
            "数据共享",
            "数据确权",
            "数据治理",
            "数据版权"
        };
        Random random = new Random();

        List<QueueItem> queueItems = new List<QueueItem>();

        private readonly ChromiumWebBrowser browser;

        public Form1()
        {
            InitializeComponent();

            if (!Directory.Exists(usedRootPath))
            {
                Directory.CreateDirectory(usedRootPath);
            }

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

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
                var result = browser.GetSourceAsync().Result;
                var ddd = 111;
                //browser.ExecuteScriptAsyncWhenPageLoaded("alert('All Resources Have Loaded');");
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


        private async Task requestEvent(string requestUrl)
        {
            //Config.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

            //await Http.Get(requestUrl)
            //   //.data(new { wd = "GitHub - Haku-Men HttpLib" })
            //   .redirect(true)
            //   .requestProgres(prog =>
            //   {
            //       Console.Write("{0}% 上传", prog);
            //   })
            //   .responseProgres((bytesSent, totalBytes) =>
            //   {
            //       if (totalBytes.HasValue)
            //       {
            //           double prog = (bytesSent * 1.0) / (totalBytes.Value * 1.0);
            //           Console.Write("{0}% 下载", Math.Round(prog * 100.0, 1).ToString("N1"));
            //       }
            //   })
            //   .request().ContinueWith((data) =>
            //   {
            //       var htmlData = data.Result.Data;
            //       Console.WriteLine(htmlData);
            //       try
            //       {

            //           if (!string.IsNullOrEmpty(htmlData))
            //           {
            //               HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //               doc.LoadHtml(htmlData);

            //               HtmlNodeCollection hrefList = doc.DocumentNode.SelectNodes(".//a[@href]");

            //               var htmlText = doc.DocumentNode.InnerText;

            //               if (keyWords.Any(c => htmlText.Contains(c)))
            //               {
            //                   // 满足关键字的条件，就保存该文档数据
            //                   writeDocData(rootPath, doc.DocumentNode.Name, doc.DocumentNode.SelectSingleNode("//body").InnerText);
            //               }

            //               if (hrefList != null)
            //               {
            //                   foreach (HtmlNode href in hrefList)
            //                   {

            //                       doSomething(rootPath, href, requestUrl);
            //                   }

            //               }
            //               #region 暂时无用

            //               //Regex pattern = new Regex("<a.*?href=[\"']?((https?://)?/?[^\"']+)[\"']?.*?>(.+)</a>");
            //               //var regexMatchCollection = pattern.Matches(htmlData);



            //               //string p = @"/<a.*href/s*=/s*(?:""(?<url>[^""]*)""|'(?<url>[^']*)'|(?<url>[^/>^/s]+)).*/>(?<title>[^/<^/>]*)/<[^/</a/>]*/a/>";
            //               //Regex reg = new Regex(p, RegexOptions.IgnoreCase);
            //               //MatchCollection ms = reg.Matches(htmlData);
            //               //Console.WriteLine("总共抓取了：{0}条链接。", ms.Count);
            //               //foreach (Match m in ms)
            //               //{
            //               //    Console.WriteLine("{0}/n{1}/n", m.Groups["title"].Value, m.Groups["url"].Value);
            //               //}

            //               //string mystr = @"<A HREF=""aaaahttp://www.csdn.net"">aaaa</A>";
            //               //string p = @"/<a.*href/s*=/s*(?:""(?<url>[^""]*)""|'(?<url>[^']*)'|(?<url>[^/>^/s]+)).*/>(?<title>[^/<^/>]*)/<[^/</a/>]*/a/>";
            //               //Regex reg = new Regex(p, RegexOptions.IgnoreCase);
            //               //MatchCollection ms = reg.Matches(mystr);
            //               //foreach (Match m in ms)
            //               //{
            //               //    MessageBox.Show(m.Groups["title"].Value);
            //               //    MessageBox.Show(m.Groups["url"].Value);
            //               //}

            //               //if (matchCollection.Any())
            //               //{
            //               //    foreach (var item in matchCollection)
            //               //    {
            //               //        String link = matcher.group(1).trim();
            //               //        String title = matcher.group(3).trim();
            //               //        if (!link.startsWith("http"))
            //               //        {
            //               //            if (link.startsWith("/"))
            //               //                link = "http://www.zifangsky.cn" + link;
            //               //            else
            //               //                link = "http://www.zifangsky.cn" + link;
            //               //        }
            //               //        Console.WriteLine("link: " + link);
            //               //        Console.WriteLine("title: " + title);
            //               //    }

            //               //} 
            //               #endregion

            //               this.Invoke(() =>
            //               {
            //                   this.label1.Text = $"{DateTime.Now} 下载完成";
            //               });
            //               Console.WriteLine("下载完成");

            //           }

            //       }
            //       catch (Exception ex)
            //       {
            //           Console.WriteLine(ex);
            //       }

            //   });
            //// 开始抓取内容
            //await startQueue();
        }

        private void doSomething(string rootPath, HtmlNode htmlNode, string baseUrl)
        {
            HtmlAttribute att = htmlNode.Attributes["href"];
            if (att != null && (att.Value != "/" && !string.IsNullOrEmpty(att.Value)))
            {
                if (!att.Value.ToLower().Contains("javascript"))
                {
                    var outerHtml = htmlNode.OuterHtml;
                    if (!outerHtml.ToLower().StartsWith("http"))
                    {
                        outerHtml = baseUrl + att.Value;
                    }
                    var title = htmlNode.InnerText;

                    // 根据标题或地址，检查是否已经被抓取过
                    var checkState = queueItems.FirstOrDefault(c => c.Title.Contains(title) || c.Url.Contains(outerHtml));
                    if (checkState != null)
                    {
                        Logs(usedRootPath, $"{title}|{outerHtml}");
                    }
                    else
                    {
                        var item = new QueueItem { Title = title, Url = outerHtml };
                        // 加入未采集过的URL到队列
                        downQueue.Enqueue(item);
                        Logs(rootPath, $"{title}|{outerHtml}");
                        queueItems.Add(item);
                    }

                }

            }

        }

        private static void Logs(string rootPath, string content)
        {
            File.WriteAllText(Path.Combine(rootPath, $"{DateTime.Now.Ticks}.txt"), content);
        }
        private static void writeDocData(string rootPath, string title, string content)
        {
            File.WriteAllText(Path.Combine(rootPath, $"{title}_{DateTime.Now.Ticks}.txt"), content);
        }
        /// <summary>
        /// 启动队列
        /// </summary>
        /// <returns></returns>
        async Task startQueue()
        {
            await Task.Run(async () =>
            {
                //如果参数队列里面还有数据则继续执行
                while (downQueue.Count > 0)
                {
                    this.Invoke(() =>
                    {
                        this.label1.Text = $"{DateTime.Now} 当前队列还有{downQueue.Count}个数据";
                    });

                    try
                    {
                        //从参数队列里面取出参数，同时将参数从队列中移除
                        downQueue.TryDequeue(out QueueItem a);
                        if (a != null)
                        {
                            await requestEvent(a.Url);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(() =>
                        {
                            this.label2.Text = $"{DateTime.Now} 出现异常，原因 {ex.Message}";
                        });

                    }
                    Thread.Sleep(random.Next(1000, 3000));
                }
            });
        }
    }
}