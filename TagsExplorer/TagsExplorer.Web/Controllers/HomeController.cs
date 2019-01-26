using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TagsExplorer.Web.Models;

namespace TagsExplorer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITagsProvider<ExtendedTagModel> _tagsProvider;
        private readonly TagCountConfig _tagCount;

        public HomeController(ITagsProvider<ExtendedTagModel> tagsProvider, TagCountConfig tagCount)
        {
            _tagsProvider = tagsProvider ?? throw new ArgumentNullException(nameof(tagsProvider));
            _tagCount = tagCount ?? throw new ArgumentNullException(nameof(tagCount));
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ExtendedTagModel> tags = await _tagsProvider.GetMostUsedTags(_tagCount.TagCount);
            return View(tags.Select(t => new TagViewModel(t.Name, t.UsageCount, t.UsagePercentage)));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
