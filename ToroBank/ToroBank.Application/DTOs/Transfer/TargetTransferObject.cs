using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.DTOs.Transfer
{
    public class TargetTransferObject : BaseTransferObject
    {
        public string Account { get; set; }
    }
}
