using System.Threading.Tasks;
using ToroBank.Application.Dto;
using ToroBank.Application.Dto.GatewayResponses.Repositories;
using ToroBank.Application.Dto.UseCaseRequests.Transfer;
using ToroBank.Application.Interfaces;
using ToroBank.Application.Interfaces.UseCases;
using ToroBank.Core.Repositories.Interfaces;

namespace ToroBank.Application.UseCases
{
    public class TransferUseCase : ITransferUseCase
    {
        private readonly IUserRepository _userRepository;
        public TransferUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(TransferRequest message, IOutputPort<TransferResponse> outputPort)
        {
            try
            {
                var user = await _userRepository.GetByCPFAsync(message.Origin.CPF);

                if (message == null || message?.Target == null || message?.Origin == null)
                    throw new System.NullReferenceException("Transação inválida");

                if (!message.Event.ToUpper().Equals("TRANSFER"))
                    throw new System.ArgumentException("Operação inválida");

                if (message.Amount == 0)
                    throw new System.ArgumentException("O valor transferido não é válido");

                if (user == null)
                    throw new System.NullReferenceException("CPF não encontrado");

                user.Balance += message.Amount;


                var obj = await _userRepository.UpdateAsync(user);

                outputPort.Handle(new TransferResponse($"{obj.Id}", true));
                return true;
            }
            catch (System.Exception ex)
            {
                outputPort.Handle(new TransferResponse(new[] { new Error("Falha na transferência", ex.Message) }));
                return false;
            }

        }
    }
}
