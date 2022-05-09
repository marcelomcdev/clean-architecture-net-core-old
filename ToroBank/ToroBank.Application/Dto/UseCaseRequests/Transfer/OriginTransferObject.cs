using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.Dto.UseCaseRequests.Transfer
{
    public class OriginTransferObject : BaseTransferObject
    {
        public string CPF { get; set; }
    }
}
