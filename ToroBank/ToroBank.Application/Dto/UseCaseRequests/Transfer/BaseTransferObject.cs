using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.Dto.UseCaseRequests.Transfer
{
    public abstract class BaseTransferObject
    {
        public string Bank { get; set; }
        public string Branch { get; set; }
    }
}
