using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using ToroBank.Application.Dto.GatewayResponses.Repositories;
using ToroBank.Application.Dto.UseCaseRequests.Transfer;
using ToroBank.Application.Interfaces;
using ToroBank.Application.UseCases;
using ToroBank.Core.Entities;
using ToroBank.Core.Repositories.Interfaces;

namespace BankToro.Test.Application
{
    public class TransferTest
    {
       
        private Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private User mockUser;

        [SetUp]
        public void Setup()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockUser = new User(300124, "Marcus", "45358996060", 350);
            
            mockUserRepository.Setup(repo => repo.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(mockUser));
            mockUserRepository.Setup(repo => repo.GetByCPFAsync(It.IsAny<string>())).Returns(Task.FromResult(mockUser));

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
            var useCase = new TransferUseCase(mockUserRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<TransferResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<TransferResponse>()));

            var response = useCase.Handle(new TransferRequest()
            {
                Event = "TRANSFER",
                Amount = 1000M,
                Origin = new OriginTransferObject() { Bank = "033", Branch= "03312", CPF= "45358996060" },
                Target = new TargetTransferObject() { Bank = "352", Branch = "0001", Account = "300123" }
            }, mockOutputPort.Object) ;

            Assert.True(response.Result);
        }

        [Test]
        public void Should_return_false_if_transfer_will_be_made_in_other_cpf()
        {
            User mockUserNull = null;
            mockUserRepository.Setup(repo => repo.GetByCPFAsync(It.IsAny<string>())).Returns(Task.FromResult(mockUserNull));

            var useCase = new TransferUseCase(mockUserRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<TransferResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<TransferResponse>()));

            var response = useCase.Handle(new TransferRequest()
            {
                Event = "TRANSFER",
                Amount = 1000M,
                Origin = new OriginTransferObject() { Bank = "033", Branch = "03312", CPF = "05191872455" },
                Target = new TargetTransferObject() { Bank = "352", Branch = "0001", Account = "300123" }
            }, mockOutputPort.Object);

            Assert.IsFalse(response.Result);
        }

        [Test]
        public void Should_return_false_if_if_target_is_null()
        {
            User mockUserNull = null;
            mockUserRepository.Setup(repo => repo.GetByCPFAsync(It.IsAny<string>())).Returns(Task.FromResult(mockUserNull));

            var useCase = new TransferUseCase(mockUserRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<TransferResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<TransferResponse>()));

            var response = useCase.Handle(new TransferRequest()
            {
                Event = "TRANSFER",
                Amount = 1000M,
                Origin = new OriginTransferObject() { Bank = "033", Branch = "03312", CPF = "05191872455" },
                Target = null
            }, mockOutputPort.Object);

            Assert.IsFalse(response.Result);
        }

        [Test]
        public void Should_return_false_if_origin_is_null()
        {
            User mockUserNull = null;
            mockUserRepository.Setup(repo => repo.GetByCPFAsync(It.IsAny<string>())).Returns(Task.FromResult(mockUserNull));

            var useCase = new TransferUseCase(mockUserRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<TransferResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<TransferResponse>()));

            var response = useCase.Handle(new TransferRequest()
            {
                Event = "TRANSFER",
                Amount = 1000M,
                Origin = null,
                Target = new TargetTransferObject() { Bank = "352", Branch = "0001", Account = "300123" }
            }, mockOutputPort.Object);

            Assert.IsFalse(response.Result);
        }

    }


}
