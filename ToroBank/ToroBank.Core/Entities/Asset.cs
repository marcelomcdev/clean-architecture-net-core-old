namespace ToroBank.Core.Entities
{
    public class Asset : BaseEntity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
