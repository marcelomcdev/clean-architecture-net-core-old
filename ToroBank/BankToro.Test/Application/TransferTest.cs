using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ToroBank.Core.Entities;

namespace BankToro.Test.Application
{
    public class TransferTest
    {
        TransferRequest mockRequest;
        List<User> mockListUser;
        Transfer transfer;

        [SetUp]
        public void Setup()
        {
            transfer = new Transfer();
            mockRequest = new TransferRequest()
            {
                Event = "TRANSFER",
                Target = new TargetTransfer()
                {
                    Bank = "352",
                    Branch = "0001",
                    Account = "300123"
                },
                Origin = new OriginTransfer()
                {
                    Bank = "033",
                    Branch = "03312",
                    CPF = "45358996060"
                },
                Amount = 1000M
            };

            mockListUser = new List<User>() { 
                new User(300123, "Jonh", "45358996060", 0), 
                new User(300124, "Marcus", "45358996060"),
                new User(300125, "Andrew", "12544566895"),
            };
        }

        [Test]
        public void Should_have_error_if_transfer_is_null()
        {
            mockRequest = null;
            Assert.Throws<System.NullReferenceException>(() => transfer.ValidateTransfer(mockRequest, mockListUser));
        }

        [Test]
        public void Should_have_error_if_value_eq_0()
        {
            mockRequest.Amount = 0;
            Assert.Throws<System.ArgumentException>(() => transfer.ValidateTransfer(mockRequest, mockListUser));
        }

        [Test]
        public void Should_have_error_if_origin_is_null()
        {
            mockRequest.Origin = null;
            Assert.Throws<System.NullReferenceException>(() => transfer.ValidateTransfer(mockRequest, mockListUser));
        }

        [Test]
        public void Should_have_error_if_target_is_null()
        {
            mockRequest.Target = null;
            Assert.Throws<System.NullReferenceException>(() => transfer.ValidateTransfer(mockRequest, mockListUser));
        }

        [Test]
        public void Should_have_error_if_origin_cpf_is_not_eq_target_cpf()
        {
            mockRequest.Origin.CPF = "05191596524";
            Assert.Throws<System.NullReferenceException>(() => transfer.ValidateTransfer(mockRequest, mockListUser));
        }        

        [Test]
        public void Should_pass_if_origin_cpf_is_eq_target_cpf()
        {
            Assert.DoesNotThrow(() => transfer.ValidateTransfer(mockRequest, mockListUser));
            Assert.IsTrue(transfer.ValidateTransfer(mockRequest, mockListUser));
        }

    }

    public class Transfer
    {
        public bool ValidateTransfer(TransferRequest transfer, List<User> users)
        {           

            if (transfer == null || transfer?.Target == null || transfer?.Origin == null)
                throw new System.NullReferenceException("A requisição de transferência é inválida");
            
            if (transfer.Amount == 0)
                throw new System.ArgumentException("O valor transferido não é válido");

            User user = users.FirstOrDefault(f => f.CPF.Equals(transfer.Origin.CPF));
            if (user == null)
                throw new System.NullReferenceException("CPF não encontrado");

            return true;
        }
    }

    public class ValidationException : System.Exception
    {
        public List<string> Errors { get; }
        public ValidationException() : base("Um ou mais erros de validação ocorreram.")
        {
            Errors = new List<string>();
        }
    }

    public class TransferRequest
    {
        public string Event { get; set; }
        public TargetTransfer Target { get; set; }
        public OriginTransfer Origin { get; set; }
        public decimal Amount { get; set; }
    }

    public abstract class BaseTransfer
    {
        public string Bank { get; set; }
        public string Branch { get; set; }
    }
    public class TargetTransfer : BaseTransfer
    {
        public string Account { get; set; }
    }

    public class OriginTransfer : BaseTransfer
    {
        public string CPF { get; set; }
    }

}
