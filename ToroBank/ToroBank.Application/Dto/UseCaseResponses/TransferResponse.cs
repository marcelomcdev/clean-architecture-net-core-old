using System.Collections.Generic;
using ToroBank.Application.Dto.GatewayResponses;

namespace ToroBank.Application.Dto.UseCaseResponses
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
