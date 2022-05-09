using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Application.Interfaces
{
    public interface IOutputPort<in TTransferUseCaseResponse>
    {
        void Handle(TTransferUseCaseResponse response);
    }
}
