using System;
using System.Collections.Generic;
using System.Text;

namespace SearchTools.Core.Entities
{
    class TitleInfo
    {
        public string Title { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return $@"Title={this.Title} Url={this.Url}";
        }
    }
}
