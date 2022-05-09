using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.DTOs.Transfer
{
    public abstract class BaseTransferObject
    {
        public string Bank { get; set; }
        public string Branch { get; set; }
    }
}
