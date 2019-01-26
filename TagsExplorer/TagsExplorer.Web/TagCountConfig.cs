using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagsExplorer.Web
{
    public class TagCountConfig
    {
        public TagCountConfig(int tagCount)
        {
            TagCount = tagCount;
        }

        public int TagCount { get; }
    }
}
