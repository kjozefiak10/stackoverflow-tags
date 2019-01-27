namespace TagsExplorer
{
    public class ExtendedTagModel : TagModel
    {
        public ExtendedTagModel(string name, int usageCount, double usagePercentage) 
            : base(name, usageCount)
        {
            UsagePercentage = usagePercentage;
        }

        public double UsagePercentage { get; }
    }
}
