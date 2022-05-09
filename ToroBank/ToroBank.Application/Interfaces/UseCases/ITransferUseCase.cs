using System.Threading.Tasks;
using ToroBank.Application.Dto.UseCaseRequests.Transfer;

namespace ToroBank.Application.Interfaces.UseCases
{
    public interface ITransferUseCase
    {
        Task<bool> Handle(TransferRequest message, IOutputPort<TransferResponse> outputPort);
    }
}
