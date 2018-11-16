using SearchTools.Core.AbsClass;
using SearchTools.Core.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchTools.Core
{
    public class SearchEngine
    {
        private Action<string> outputAction = str => Console.WriteLine(str);
        public SearchEngine SetOutputAction(Action<string> action)
        {
            this.outputAction = action;
            return this;
        }
        public SearchEngine Search(string keyWord)
        {
            AbsSearch[] engines = new AbsSearch[] { new Zhihu(this.outputAction) };
            //{
            //    new Baidu(this.outputAction),
            //    new Bing(this.outputAction),
            //    new Sogou(this.outputAction),
            //    new Zhihu(this.outputAction)
            //};
            foreach (var engine in engines)
            {
                engine.Search(keyWord);
            }
            return this;
        }
    }
}
