using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Core.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
