using SearchTools.Core.AbsClass;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchTools.Core.Utils
{
    static class ExtenssionClass
    {
        private const string userAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36";
        public static string DownloadString(this AbsSearch search, string url)
        {

            var webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.UserAgent, userAgent);
            return webClient.DownloadString(url);
        }

        public static async Task<string> DownloadStringAsync(this AbsSearch search, string url)
        {

            var webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.UserAgent, userAgent);
            return await webClient.DownloadStringTaskAsync(url);
        }

        public static string RemoveNote(this string html)
        {
            var noteRegex = new Regex(@"<!\-\-[\s\S]*?\-\->");
            var matches = noteRegex.Matches(html);
            foreach (var match in matches)
            {
                html = html.Replace(match.ToString(), "");
            }
            return html;
        }

        public static string RemoveSpan(this string html)
        {
            var spanRegex = new Regex(@"<span[\s\S]*?</span>");
            var matches = spanRegex.Matches(html);
            foreach (var match in matches)
            {
                html = html.Replace(match.ToString(), "");
            }
            return html;
        }
    }
}
