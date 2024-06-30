namespace Focus.Domain.Entities
{
    public class AccountTemplate: BaseEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string JsonTemplate { get; set; }
    }
}
