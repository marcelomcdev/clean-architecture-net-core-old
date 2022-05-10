using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.Dto.GatewayResponses
{
    public abstract class BaseGatewayResponse
    {
        protected BaseGatewayResponse(bool success = false, IEnumerable<Error> errors = null)
        {
            Success = success;
            Errors = errors;
        }

        protected BaseGatewayResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }
        public IEnumerable<Error> Errors { get; set; }
        public string Message { get; }
    }
}
