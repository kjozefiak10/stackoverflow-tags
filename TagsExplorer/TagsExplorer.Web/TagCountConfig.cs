namespace TagsExplorer.Web
{
    /// <summary>
    /// Temporary place to configure tag count on site.
    /// </summary>
    public class TagCountConfig
    {
        public TagCountConfig(int tagCount)
        {
            TagCount = tagCount;
        }

        public int TagCount { get; }
    }
}
