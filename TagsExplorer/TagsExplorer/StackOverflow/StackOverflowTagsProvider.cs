using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TagsExplorer.StackOverflow
{
    public class StackOverflowTagsProvider : ITagsProvider<TagModel>
    {
        private readonly IRestClient _restClient;

        /// <summary>
        /// This value is defined by StackOverflow Api Docs.
        /// See more: https://api.stackexchange.com/docs/paging.
        /// </summary>
        public static readonly byte PageSize = 100;

        public StackOverflowTagsProvider(string apiAddress)
            : this(new RestClient(apiAddress))
        {
            if (string.IsNullOrEmpty(apiAddress))
                throw new ArgumentException("Parameter cannot be null or empty.", nameof(apiAddress));
        }

        public StackOverflowTagsProvider(IRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        /// <summary>
        /// TODO:
        ///     - using paraller requests, but it can be impossible because RestClient using underhood lock statement...,
        ///     - handle exceptions on above layer
        ///     - optimize tags count condition.
        /// </summary>
        /// <exception cref="Exception" />
        public async Task<IEnumerable<TagModel>> GetMostUsedTags(int count)
        {
            var (pageCount, lastPageSize) = CalculatePageParams(count);
            IRestRequest request = CreateBaseRequest();

            List<TagModel> tags = new List<TagModel>();
 
            int currentPage = 1;
            do
            {
                int currentPageSize = currentPage == pageCount ? lastPageSize : PageSize;
                request.AddOrUpdateParameter("pageSize", currentPageSize);
                request.AddOrUpdateParameter("page", currentPage);

                var response = await _restClient.ExecuteGetTaskAsync<TagItems>(request);

                if (response.Data == null || response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Bad response from StackOverflow api. Response code: {response.StatusCode}.");

                IEnumerable<TagModel> pageTags = response.Data.Items.Select(i => new TagModel(i.Name, i.Count));
                tags.AddRange(pageTags);

                if (response.Data.Has_More == false)
                    break;

            } while (++currentPage <= pageCount);


            return tags.Count > count ? tags.Take(count) : tags;
        }

        /// <summary>
        /// TODO: 
        ///     - add filter param to choose only needed fields,
        ///     - move explicitly strings to dedicated class like StackOverflowParamsConstant.
        /// </summary>
        /// <returns></returns>
        private IRestRequest CreateBaseRequest()
        {
            return new RestRequest("tags")
                .AddQueryParameter("order", "desc")
                .AddQueryParameter("sort", "popular")
                .AddQueryParameter("site", "stackoverflow");
        }

        private (int pageCount, int lastPageSize) CalculatePageParams(int count)
        {
            int pageCount = 1, lastPageSize = count;

            if (PageSize < count)
            {
                pageCount = count / PageSize;
                lastPageSize = count & PageSize;
            }

            return (pageCount, lastPageSize);
        }
    }
}
