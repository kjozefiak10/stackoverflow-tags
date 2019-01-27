using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagsExplorer
{
    public class UsagePercentageTagsProvider : ITagsProvider<ExtendedTagModel>
    {
        private readonly ITagsProvider<TagModel> _tagsProvider;

        public UsagePercentageTagsProvider(ITagsProvider<TagModel> tagsProvider)
        {
            _tagsProvider = tagsProvider ?? throw new ArgumentNullException(nameof(tagsProvider));
        }

        public async Task<IEnumerable<ExtendedTagModel>> GetMostUsedTags(int count)
        {
            IEnumerable<TagModel> tags = await _tagsProvider.GetMostUsedTags(count);
            int usageCountSum = tags.Sum(t => t.UsageCount);
            return tags.Select(t => new ExtendedTagModel(t.Name, t.UsageCount, Math.Round(t.UsageCount / (double)usageCountSum * 100, 2)));
        }
    }
}
