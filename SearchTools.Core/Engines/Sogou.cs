using SearchTools.Core.AbsClass;
using SearchTools.Core.Entities;
using SearchTools.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SearchTools.Core.Engines
{
    class Sogou : AbsSearch
    {
        private List<TitleInfo> list = new List<TitleInfo>();
        public Sogou(Action<string> opFoo) : base(opFoo) { }
        protected override void DoSearch(string keyWord)
        {
            var url = $@"https://www.sogou.com/web?query={keyWord}";
            var html = this.DownloadString(url);

            var titleInfoRegex = new Regex(@"<h3[\s\S]*?</h3>");
            var titleRegex = new Regex(@">[^\s<a<s][\s\S]*?</a>");
            var hrefRegex = new Regex(@"href\s*=\s*[\s\S]+?""");
            Console.WriteLine(titleRegex.ToString());
            html = html.RemoveNote().RemoveSpan();
            var matches = titleInfoRegex.Matches(html);
            foreach (var match in matches)
            {
                var text = match.ToString().Replace("<h3>", "");
                if (string.IsNullOrWhiteSpace(text))
                {
                    continue;
                }
                var title = titleRegex.Match(text).ToString();
                var href = hrefRegex.Match(text).ToString();
                if (string.IsNullOrWhiteSpace(title))
                {
                    continue;
                }
                list.Add(new TitleInfo()
                {
                    Title = title
                    .ToString()
                    .Replace("<em>", "")
                    .Replace("</em>", "")
                    .Replace("</a>", "")
                    .Substring(1).Trim(),
                    Url = $@"www.sogou.com{href.Replace("\"", "").Replace("href=", "")}"
                });
            }

        }

        protected override void ShowResult()
        {
            foreach (var item in list)
            {
                this.outputAction(item.ToString());
            }
        }
    }
}
