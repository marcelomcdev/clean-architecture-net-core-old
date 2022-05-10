using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.Dto.GatewayResponses.Repositories
{
    public sealed class TransferResponse : BaseGatewayResponse
    {
        public string Id { get; set; }
        public TransferResponse(string id, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Id = id;
        }

        public TransferResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }
    }
}
