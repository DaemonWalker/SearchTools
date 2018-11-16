using SearchTools.Core.AbsClass;
using SearchTools.Core.Entities;
using SearchTools.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SearchTools.Core.Engines
{
    class Bing : AbsSearch
    {
        private List<TitleInfo> titles = new List<TitleInfo>();
        public Bing(Action<string> opFoo) : base(opFoo) { }
        protected override void DoSearch(string keyWord)
        {
            var url = $@"https://cn.bing.com/search?q={keyWord}";
            var html = this.DownloadString(url);
            var titleInfoRegex = new Regex(@"<h2>[\s\S]*?</h2>");
            var titleRegex = new Regex(@""">[\s\S]*</a>");
            var hrefRegex = new Regex(@"href=""\S*""");

            var matches = titleInfoRegex.Matches(html);
            foreach (Match match in matches)
            {
                if (string.IsNullOrEmpty(match.ToString()))
                {
                    continue;
                }
                titles.Add(new TitleInfo()
                {
                    Title = titleRegex
                        .Match(match.ToString())
                        .ToString().Replace("</a>", "")
                        .Replace("<strong>", "")
                        .Replace("</strong>", "")
                        .Replace("\"", "")
                        .Replace(">", "")
                        .Trim(),
                    Url = hrefRegex.Match(match.ToString()).ToString().Replace("\"", "").Replace("href=", "").Trim()
                });
            }
        }

        protected override void ShowResult()
        {
            this.outputAction("Bing CN");
            foreach (var title in titles)
            {
                this.outputAction(title.ToString());
            }
        }
    }
}
