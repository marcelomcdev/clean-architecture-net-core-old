using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToroBank.Application.DTOs.Transfer;
using ToroBank.Core.Entities;
using ToroBank.Core.Repositories.Interfaces;
using ToroBank.Core.Repositories.Interfaces.Base;

namespace BankToro.Test.Application
{
    public class TransferTest
    {
        TransferRequest mockRequest;
        List<User> mockListUser;


        [SetUp]
        public void Setup()
        {

            mockRequest = new TransferRequest()
            {
                Event = "TRANSFER",
                Target = new TargetTransferObject()
                {
                    Bank = "352",
                    Branch = "0001",
                    Account = "300123"
                },
                Origin = new OriginTransferObject()
                {
                    Bank = "033",
                    Branch = "03312",
                    CPF = "45358996060"
                },
                Amount = 1000M
            };

            mockListUser = new List<User>() { 
                new User(300123, "Jonh", "45358996060", 0), 
                new User(300124, "Marcus", "45358996060",350),
                new User(300125, "Andrew", "12544566895",420),
            };
        }


        /// <summary>
        /// Eu, como investidor, gostaria de poder depositar um valor na minha conta Toro, através de PiX ou TED bancária, para que eu possa realizar investimentos.
        /// A Toro já participa do Sistema Brasileiro de Pagamentos (SPB) do Banco Central, e está integrado a ele. Isto significa que a Toro tem um número de banco (352), cada cliente tem um número único de conta na Toro, 
        /// e que toda transferência entre bancos passa pelo SBP do Banco Central, e quando a transferência é identificada como tendo o destino o banco Toro (352), uma requisição HTTP é enviada pelo Banco Central notificando tal evento. 
        /// O formato desta notificação segue o padrão REST + JSON a seguir (hipotético para efeito de simplificação do desafio):
        /// 
        /// Outra restrição é que a origem da transferência deve sempre ser do mesmo CPF do usuário na Toro.
        /// </summary>
        [Test]
        public void Can_Notify_Transfer_To_An_User()
        {
            var mockListUser = new List<User>() {
                new User(300123, "Jonh", "45358996060", 0),
                new User(300124, "Marcus", "45358996060",350),
                new User(300125, "Andrew", "12544566895",420),
            };

            var mockUser = new User(300124, "Marcus", "45358996060", 350);
            var mockUserExpected = new User(300124, "Marcus", "45358996060", 1350);

            var mockUserRepository = new Mock<IUserRepository>();
            var o = mockUserRepository.Setup(repo => repo.UpdateAsync(mockUser)).Returns(Task.FromResult(mockUserExpected));

            var useCase = new TransferUseCase(mockUserRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<TransferResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<TransferResponse>()));

            var response =  useCase.Handle(new TransferRequest()
            {
                Event = "TRANSFER",
                Amount = 1000M,
                Origin = new OriginTransferObject() { },
                Target = new TargetTransferObject() { }
            }, mockOutputPort.Object).Result;

            Assert.True(response);
        }
    }

    //Domain
    //public interface IUserRepository : IRepository<User>
    //{
    //    Task<User> GetByCPFAsync(string cpf);
    //}

    public interface ITransferRepository
    {
        Task<TransferResponse> Transfer(TransferRequest request, User user);
    }

    //Infrastructure
    public class UserRepository : IUserRepository
    {
        public Task<User> AddAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> UpdateAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByCPFAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByCPFAsync(string cpf)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface ITransferUseCase
    {
        Task<bool> Handle(TransferRequest message, IOutputPort<TransferResponse> outputPort);
    }


    public interface IOutputPort<in TTransferUseCaseResponse>
    {
        void Handle(TTransferUseCaseResponse response);
    }

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

    public class TransferResponse : BaseGatewayResponse
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
