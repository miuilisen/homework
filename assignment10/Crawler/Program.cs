using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrawlerForm
{
    class Crawler
    {
        public event Action<Crawler> CrawlerStopped;
        public event Action<Crawler, string, string> PageDownloaded;
        public static readonly string UrlDetectRegex = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";
        public static readonly string urlParseRegex = @"^(?<site>(?<protocal>https?)://(?<host>[\w\d.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";
        //主机过滤规则
        // 待下载URL队列
        private ConcurrentQueue<string> pending = new ConcurrentQueue<string>();
        // 已下载URL
        private ConcurrentDictionary<string, bool> downloaded = new ConcurrentDictionary<string, bool>();
        private int downloadedCount = 0;

        public string HostFilter { get; set; }
        public string FileFilter { get; set; }
        public int MaxPage { get; set; }
        public string StartURL { get; set; }
        public Encoding HtmlEncoding { get; set; }

        public Crawler()
        {
            MaxPage = 100;
            HtmlEncoding = Encoding.UTF8;
        }

        public void Start()
        {
            downloaded.Clear();
            downloadedCount = 0;
            pending.Enqueue(StartURL);

            List<Task> tasks = new List<Task>();

            while (downloadedCount < MaxPage && pending.Count > 0)
            {
                while (tasks.Count < Environment.ProcessorCount * 2 && pending.TryDequeue(out string url))
                {
                    tasks.Add(Task.Run(() => ProcessPage(url)));
                }

                Task.WhenAny(tasks).Wait();

                tasks.RemoveAll(t => t.IsCompleted);
            }

            CrawlerStopped?.Invoke(this);
        }

        private void ProcessPage(string url)
        {
            try
            {
                string html = Download(url);
                downloaded[url] = true;
                PageDownloaded?.Invoke(this, url, "成功");

                if (downloadedCount >= MaxPage)
                    return;

                Parse(html, url);
            }
            catch (Exception ex)
            {
                downloaded[url] = false;
                PageDownloaded?.Invoke(this, url, "错误：" + ex.Message);
            }
        }

        private string Download(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = HtmlEncoding;
                string html = webClient.DownloadString(url);
                string fileName = downloadedCount.ToString();
                File.WriteAllText(fileName, html, HtmlEncoding);
                downloadedCount++;
                return html;
            }
        }

        private void Parse(string html, string pageUrl)
        {
            var matches = new Regex(UrlDetectRegex).Matches(html);
            foreach (Match match in matches)
            {
                string linkUrl = match.Groups["url"].Value;
                if (string.IsNullOrEmpty(linkUrl) || linkUrl.StartsWith("javascript:"))
                    continue;

                linkUrl = FixUrl(linkUrl, pageUrl);

                Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
                string host = linkUrlMatch.Groups["host"].Value;
                string file = linkUrlMatch.Groups["file"].Value;

                if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(file, FileFilter)
                    && !downloaded.ContainsKey(linkUrl) && !pending.Contains(linkUrl))
                {
                    pending.Enqueue(linkUrl);
                }
            }
        }

        //将非完整路径转为完整路径
        static private string FixUrl(string url, string pageUrl)
        {
            if (url.Contains("://"))
            { //完整路径
                return url;
            }
            if (url.StartsWith("//"))
            {
                Match urlMatch = Regex.Match(pageUrl, urlParseRegex);
                string protocal = urlMatch.Groups["protocal"].Value;
                return protocal + ":" + url;
            }
            if (url.StartsWith("/"))
            {
                Match urlMatch = Regex.Match(pageUrl, urlParseRegex);
                String site = urlMatch.Groups["site"].Value;
                return site.EndsWith("/") ? site + url.Substring(1) : site + url;
            }

            if (url.StartsWith("../"))
            {
                url = url.Substring(3);
                int idx = pageUrl.LastIndexOf('/');
                return FixUrl(url, pageUrl.Substring(0, idx));
            }

            if (url.StartsWith("./"))
            {
                return FixUrl(url.Substring(2), pageUrl);
            }
            //非上述开头的相对路径
            int end = pageUrl.LastIndexOf("/");
            return pageUrl.Substring(0, end) + "/" + url;
        }


    }
}
namespace CrawlerForm
{
    class Program
    {
        static void Crawler_CrawlerStopped(Crawler crawler)
        { }
        static void Crawler_PageDownloaded(Crawler crawler, string url, string status)
        {
            // 页面下载完成事件处理程序
            Console.WriteLine("下载完成：" + url + " 状态：" + status);
        }
        static void Main(string[] args)
        {
            // 创建一个爬虫实例
            Crawler crawler = new Crawler();

            // 设置爬虫属性
            crawler.StartURL = "https://www.apple.com"; // 设置起始URL
            crawler.HostFilter = "apple.com"; // 设置主机过滤规则
            crawler.FileFilter = ".htm"; // 设置文件过滤规则
            crawler.MaxPage = 100; // 设置最大下载数量


            // 订阅事件
            crawler.CrawlerStopped += Crawler_CrawlerStopped;
            crawler.PageDownloaded += Crawler_PageDownloaded;
            // 启动爬虫
            crawler.Start();

            Console.WriteLine("爬取完成。按任意键退出。");
            Console.ReadKey();
        }


    }
}

