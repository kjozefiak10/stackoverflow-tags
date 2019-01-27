namespace TagsExplorer
{
    public class TagModel
    {
        public TagModel(string name, int usageCount)
        {
            Name = name;
            UsageCount = usageCount;
        }

        public string Name { get; }
        public int UsageCount { get; }
    }
}
