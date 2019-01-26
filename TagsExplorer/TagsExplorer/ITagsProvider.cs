using System.Collections.Generic;
using System.Threading.Tasks;

namespace TagsExplorer
{
    public interface ITagsProvider<T> where T : TagModel
    {
        Task<IEnumerable<T>> GetMostUsedTags(int count);
    }
}
