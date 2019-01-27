using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TagsExplorer.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace TagsExplorer.StackOverflow
{
    internal class TagItems
    {
        public IEnumerable<TagItem> Items { get; set; }
        public bool Has_More { get; set; }
    }
}
