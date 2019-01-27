using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TagsExplorer.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace TagsExplorer.StackOverflow
{
    internal class TagItem
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
