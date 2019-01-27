using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagsExplorer.Web.Controllers;
using TagsExplorer.Web.Models;

namespace TagsExplorer.Web.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public async Task Index_EmptySource_ReturnEmptyViewModel()
        {
            var tagsProvider = Substitute.For<ITagsProvider<ExtendedTagModel>>();
            tagsProvider.GetMostUsedTags(Arg.Any<int>()).Returns(Task.FromResult(new List<ExtendedTagModel>().AsEnumerable()));
            var homeController = new HomeController(tagsProvider, new TagCountConfig(20));


            var result =  await homeController.Index();


            result.GetType().Should().Be(typeof(ViewResult));
            (((ViewResult)result).ViewData.Model as IEnumerable<TagViewModel>).Should().BeEmpty();
        }

        [TestMethod]
        public async Task Index_OneItemSource_ReturnOneItemViewModel()
        {
            var tagsProvider = Substitute.For<ITagsProvider<ExtendedTagModel>>();
            tagsProvider.GetMostUsedTags(Arg.Any<int>()).Returns(Task.FromResult(new List<ExtendedTagModel>{ new ExtendedTagModel("tag1", 10, 100) }.AsEnumerable()));
            var homeController = new HomeController(tagsProvider, new TagCountConfig(20));


            var result = await homeController.Index();


            result.GetType().Should().Be(typeof(ViewResult));
            (((ViewResult)result).ViewData.Model as IEnumerable<TagViewModel>).Should().BeEquivalentTo(
                new List<TagViewModel>
                {
                    new TagViewModel("tag1", 10, 100)
                });
        }
    }
}
