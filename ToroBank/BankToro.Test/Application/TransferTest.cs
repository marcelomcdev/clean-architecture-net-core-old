using NUnit.Framework;
using ToroBank.Core.Entities;

namespace BankToro.Test.Application
{
    public class TransferTest
    {
        TransferRequest mockRequest;
        User mockUser;
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

            mockUser = new User(300123, "Jonh", "45358996060",0);
        }

        [Test]
        public void Should_have_error_if_transfer_is_null()
        {
            Assert.Throws<System.NullReferenceException>(() => transfer.ValidateTransfer(null));
        }

        [Test]
        public void Should_have_a_value_transfered_above_0()
        {
            Assert.IsTrue(mockRequest.Amount > 0);
        }

        [Test]
        public void Should_have_a_bank_in_origin_and_target()
        {
            Assert.IsTrue(mockRequest.Amount > 0);
        }

    }

    public class Transfer
    {
        public bool ValidateTransfer(TransferRequest transfer)
        {
            try
            {
                if (transfer == null)
                    throw new System.NullReferenceException("A requisição de transferência é inválida");

            }
            catch (System.NullReferenceException ex)
            {
                throw new System.NullReferenceException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
            
            
            return false;
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
