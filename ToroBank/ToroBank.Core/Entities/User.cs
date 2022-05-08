namespace ToroBank.Core.Entities
{
    public class User : BaseEntity
    {
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public decimal Balance { get; set; }
    }
}
