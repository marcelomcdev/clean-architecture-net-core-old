using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.DTOs
{
    public class TransferRequest
    {
        public string Event { get; set; }
        public TargetTransfer Target { get; set; }
        public OriginTransfer Origin { get; set; }
        public decimal Amount { get; set; }
    }

    public abstract class BaseTransfer
    {
        public string Bank { get; set; }
        public string Branch { get; set; }
    }
    public class TargetTransfer : BaseTransfer
    {
        public string Account { get; set; }
    }

    public class OriginTransfer : BaseTransfer
    {
        public string CPF { get; set; }
    }
}
