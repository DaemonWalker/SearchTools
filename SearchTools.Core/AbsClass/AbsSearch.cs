using System;
using System.Collections.Generic;
using System.Text;

namespace SearchTools.Core.AbsClass
{
    abstract class AbsSearch
    {
        public AbsSearch(Action<string> opAction)
        {
            this.outputAction = opAction;
        }
        protected Action<string> outputAction;
        protected abstract void DoSearch(string keyWord);
        protected abstract void ShowResult();

        public void Search(string keyWord)
        {
            this.DoSearch(keyWord);
            this.ShowResult();
        }
    }
}
