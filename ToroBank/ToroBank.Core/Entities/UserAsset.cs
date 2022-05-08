namespace ToroBank.Core.Entities
{
    public class UserAsset : BaseEntity
    {
        public int AssetId { get; set; }
        public int UserId { get; set; }
    }
}
