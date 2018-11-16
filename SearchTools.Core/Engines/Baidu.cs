using SearchTools.Core.AbsClass;
using SearchTools.Core.Entities;
using SearchTools.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SearchTools.Core.Engines
{
    class Baidu : AbsSearch
    {
        public Baidu(Action<string> outputAction) : base(outputAction) { }

        private List<TitleInfo> list = new List<TitleInfo>();
        protected override void DoSearch(string keyWord)
        {
            var url = $@"https://www.baidu.com/s?wd={keyWord}";
            var html = this.DownloadString(url);

            var titleGroupRegex = new Regex(@"<h3\s*class=[\S\s]{0,1000}</h3>");
            var titleRegex = new Regex(@">[^<\s][\S\s]*?</a>");
            var urlRegex = new Regex(@"href\s*=\s*""[\S]*""");
            var matches = titleGroupRegex.Matches(html);

            for (var i = 0; i < matches.Count; i++)
            {
                var text = matches[i].ToString();
                if (string.IsNullOrWhiteSpace(text))
                {
                    continue;
                }
                list.Add(new TitleInfo()
                {
                    Title = titleRegex.Match(text)
                        .ToString()
                        .Replace("</a>", "")
                        .Replace("\"", "")
                        .Replace("<em>", "")
                        .Replace("</em>", "")
                        .Replace(">", ""),
                    Url = urlRegex.Match(text)
                        .ToString()
                        .Replace(" ", "")
                        .Replace("href=\"", "")
                        .Replace("\"", "")
                        .Trim()
                });
            }
        }

        protected override void ShowResult()
        {
            Console.WriteLine("Baidu");
            list.ForEach(p => outputAction(p.ToString()));
        }
    }
}
