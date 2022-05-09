using ToroBank.Core.Common;

namespace ToroBank.Core.Entities
{
    public class UserAsset : BaseEntity
    {
        protected UserAsset() { }
        public UserAsset(int assetId, int userId)
        {
            AssetId = assetId;
            UserId = userId;
        }
        public int AssetId { get; set; }
        public int UserId { get; set; }
    }
}
