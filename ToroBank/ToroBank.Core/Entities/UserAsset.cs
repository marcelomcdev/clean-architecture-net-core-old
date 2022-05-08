using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Core.Entities
{
    public class UserAsset
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
    }
}
