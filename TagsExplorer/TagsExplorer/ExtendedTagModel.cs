namespace TagsExplorer
{
    public class ExtendedTagModel : TagModel
    {
        public ExtendedTagModel(string name, int usageCount, int usagePercentage) 
            : base(name, usageCount)
        {
            UsagePercentage = usagePercentage;
        }

        public int UsagePercentage { get; }
    }
}
