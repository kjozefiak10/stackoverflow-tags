using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagsExplorer.StackOverflow;

namespace TagsExplorer.Tests
{
    [TestClass]
    public class StackOverflowTagsProviderTests
    {
        [TestMethod]
        public async Task GetMostUsedTags_EmptySourceColelction_ReturnEmptyCollection()
        {
            var restClient = Substitute.For<IRestClient>();
            var restResponse =  new RestResponse<TagItems> { StatusCode = System.Net.HttpStatusCode.OK, Data = new TagItems() { Has_More = false, Items = Enumerable.Empty<TagItem>() }};
            restClient.ExecuteGetTaskAsync<TagItems>(Arg.Any<RestRequest>()).Returns(Task.FromResult<IRestResponse<TagItems>>(restResponse));
            var usagePercentageTagsProvider = new StackOverflowTagsProvider(restClient);


            var tags = await usagePercentageTagsProvider.GetMostUsedTags(1000);


            tags.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetMostUsedTags_TwoItemsSourceCollection_ReturnTwoItemsCollection()
        {
            var sourceCollection =
                new List<TagItem>
                {
                    new TagItem { Name = "tag1", Count = 20 },
                    new TagItem { Name = "tag2", Count = 10 }
                };

            var restClient = Substitute.For<IRestClient>();
            var restResponse = new RestResponse<TagItems> { StatusCode = System.Net.HttpStatusCode.OK, Data = new TagItems() { Has_More = false, Items = sourceCollection } };
            restClient.ExecuteGetTaskAsync<TagItems>(Arg.Any<RestRequest>()).Returns(Task.FromResult<IRestResponse<TagItems>>(restResponse));
            var usagePercentageTagsProvider = new StackOverflowTagsProvider(restClient);


            var tags = await usagePercentageTagsProvider.GetMostUsedTags(1000);


            tags.Should().BeEquivalentTo(new List<TagModel>
            {
                new TagModel("tag1", 20),
                new TagModel("tag2", 10)
            });
        }

        [TestMethod]
        public async Task GetMostUsedTags_TwoItemsSourceCollectionSetCountLimitToOne_ReturnOneItemCollection()
        {
            var sourceCollection =
                new List<TagItem>
                {
                    new TagItem { Name = "tag1", Count = 20 },
                    new TagItem { Name = "tag2", Count = 10 }
                };

            var restClient = Substitute.For<IRestClient>();
            var restResponse = new RestResponse<TagItems> { StatusCode = System.Net.HttpStatusCode.OK, Data = new TagItems() { Has_More = false, Items = sourceCollection } };
            restClient.ExecuteGetTaskAsync<TagItems>(Arg.Any<RestRequest>()).Returns(Task.FromResult<IRestResponse<TagItems>>(restResponse));
            var usagePercentageTagsProvider = new StackOverflowTagsProvider(restClient);


            var tags = await usagePercentageTagsProvider.GetMostUsedTags(1);


            tags.Should().BeEquivalentTo(new List<TagModel>
            {
                new TagModel("tag1", 20)
            });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetMostUsedTags_ResponseDataIsNull_ThrowException()
        {
            var restClient = Substitute.For<IRestClient>();
            var restResponse = new RestResponse<TagItems> { StatusCode = System.Net.HttpStatusCode.OK, Data = null };
            restClient.ExecuteGetTaskAsync<TagItems>(Arg.Any<RestRequest>()).Returns(Task.FromResult<IRestResponse<TagItems>>(restResponse));
            var usagePercentageTagsProvider = new StackOverflowTagsProvider(restClient);


            await usagePercentageTagsProvider.GetMostUsedTags(1000);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetMostUsedTags_ResponseIsNotOkStatus_ThrowException()
        {
            var restClient = Substitute.For<IRestClient>();
            var restResponse = new RestResponse<TagItems> { StatusCode = System.Net.HttpStatusCode.BadRequest, Data = new TagItems() { Has_More = false, Items = Enumerable.Empty<TagItem>() } };
            restClient.ExecuteGetTaskAsync<TagItems>(Arg.Any<RestRequest>()).Returns(Task.FromResult<IRestResponse<TagItems>>(restResponse));
            var usagePercentageTagsProvider = new StackOverflowTagsProvider(restClient);


            await usagePercentageTagsProvider.GetMostUsedTags(1000);
        }
    }
}
