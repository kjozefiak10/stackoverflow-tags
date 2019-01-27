namespace TagsExplorer.Web.Models
{
    public class TagViewModel
    {
        public TagViewModel(string name, int usageCount, double usagePercentage)
        {
            Name = name;
            UsageCount = usageCount;
            UsagePercentage = usagePercentage;
        }

        public string Name { get; }
        public int UsageCount { get; }
        public double UsagePercentage { get; }
    }
}
