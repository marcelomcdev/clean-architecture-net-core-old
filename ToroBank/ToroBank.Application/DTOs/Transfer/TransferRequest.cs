using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.DTOs.Transfer
{
    public class TransferRequest
    {
        public string Event { get; set; }
        public TargetTransferObject Target { get; set; }
        public OriginTransferObject Origin { get; set; }
        public decimal Amount { get; set; }
    }
}
