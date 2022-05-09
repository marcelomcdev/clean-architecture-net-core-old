using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.DTOs.Transfer
{
    public class TransferRequest
    {
        public string Event { get; set; }
        public TargetTransfer Target { get; set; }
        public OriginTransfer Origin { get; set; }
        public decimal Amount { get; set; }
    }
}
