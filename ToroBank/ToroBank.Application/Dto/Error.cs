using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.Dto
{
    public sealed class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public Error(string code, string description)
        {
            this.Code = code;
            this.Description = description;
        }
    }
}
