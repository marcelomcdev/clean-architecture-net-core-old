using ToroBank.Core.Common;

namespace ToroBank.Core.Entities
{
    public class Asset : BaseEntity
    {
        protected Asset() { }
        public Asset(string name, decimal value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
