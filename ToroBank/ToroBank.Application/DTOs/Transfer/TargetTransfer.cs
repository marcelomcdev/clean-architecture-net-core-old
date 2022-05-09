using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.DTOs.Transfer
{
    public class TargetTransfer : BaseTransfer
    {
        public string Account { get; set; }
    }
}
