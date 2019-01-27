using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagsExplorer.Tests
{
    [TestClass]
    public class UsagePercentageTagsProviderTests
    {
        [TestMethod]
        public async Task GetMostUsedTags_EmptySourceColelction_ReturnEmptyCollection()
        {
            var tagsProvider = Substitute.For<ITagsProvider<TagModel>>();
            tagsProvider.GetMostUsedTags(Arg.Any<int>()).Returns(Task.FromResult(Enumerable.Empty<TagModel>()));
            var usagePercentageTagsProvider = new UsagePercentageTagsProvider(tagsProvider);


            var tags = await usagePercentageTagsProvider.GetMostUsedTags(1000);


            tags.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetMostUsedTags_TwoItemsSourceCollection_ReturnTwoItemsCollectionWithPercentageUsage()
        {
            var sourceCollection =
                new List<TagModel>
                {
                    new TagModel("tag1", 20),
                    new TagModel("tag2", 10)
                };

            var tagsProvider = Substitute.For<ITagsProvider<TagModel>>();
            tagsProvider.GetMostUsedTags(Arg.Any<int>()).Returns(Task.FromResult(sourceCollection.AsEnumerable()));
            var usagePercentageTagsProvider = new UsagePercentageTagsProvider(tagsProvider);


            var tags = await usagePercentageTagsProvider.GetMostUsedTags(1000);


            tags.Should().BeEquivalentTo(new List<ExtendedTagModel>
            {
                new ExtendedTagModel("tag1", 20, 66.67),
                new ExtendedTagModel("tag2", 10, 33.33),
            });
        }
    }
}
