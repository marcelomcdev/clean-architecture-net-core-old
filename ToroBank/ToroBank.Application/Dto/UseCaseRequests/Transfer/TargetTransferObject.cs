using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.Dto.UseCaseRequests.Transfer
{
    public class TargetTransferObject : BaseTransferObject
    {
        public string Account { get; set; }
    }
}
