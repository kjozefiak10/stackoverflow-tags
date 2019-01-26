using System.Collections.Generic;

namespace TagsExplorer.StackOverflow
{
    internal class TagItems
    {
        public IEnumerable<TagItem> Items { get; set; }
        public bool Has_More { get; set; }
    }
}
