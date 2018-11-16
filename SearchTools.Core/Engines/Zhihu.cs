using SearchTools.Core.AbsClass;
using SearchTools.Core.Entities;
using SearchTools.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SearchTools.Core.Engines
{
    class Zhihu : AbsSearch
    {
        public Zhihu(Action<string> opFoo) : base(opFoo) { }
        private List<TitleInfo> list = new List<TitleInfo>();
        protected override void DoSearch(string keyWord)
        {
            var url = $@"https://www.zhihu.com/search?type=content&q={keyWord}";
            var html = this.DownloadString(url);
            var titleGroupRegex = new Regex(@"<div itemProp=[\s\S]*?</div>");
            var titleRegex = new Regex(@"<span class=""Highlight"">[\s\S]*</span>");
            var hrefRegex = new Regex(@"href=[\s\S]+?/an");

            var matches = titleGroupRegex.Matches(html);
            foreach (var match in matches)
            {
                var text = match.ToString();
                var title = titleRegex.Match(text)
                    .ToString()
                    .Replace("<span class=\"Highlight\">", "")
                    .Replace("<em>", "")
                    .Replace("</em>", "")
                    .Replace("</span>", "");
                var href = hrefRegex.Match(text).ToString()
                    .Replace("\"", "")
                    .Replace("href=", "");
                href = href.Substring(0, href.Length - 2);
                list.Add(new TitleInfo()
                {
                    Title = title,
                    Url = $"www.zhihu.com{href}"
                });
            }
        }

        private void PageAnalyaze()
        {
            var ansRegex = "";
            foreach (var item in list)
            {
                var html = this.DownloadString(item.Url);

            }
        }

        protected override void ShowResult()
        {
            Console.WriteLine("Zhihu");
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
