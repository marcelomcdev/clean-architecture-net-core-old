using NUnit.Framework;
using ToroBank.Core.Entities;

namespace BankToro.Test.Application
{
    public class TransferTest
    {
        TransferRequest mockRequest;
        User mockUser;

        [SetUp]
        public void Setup()
        {
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
        public void Should_have_a_value_transfered_above_0()
        {
            Assert.Fail();
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
