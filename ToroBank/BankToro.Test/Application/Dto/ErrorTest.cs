using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ToroBank.Application.Dto;

namespace BankToro.Test.Application.Dto
{
    public class ErrorTest
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Should_pass_if_description_or_code_is_null()
        {
            Error error = new Error("401",null);
            Assert.That(error.Code, Is.Not.Null);
            Assert.That(error.Description, Is.Null);
            Assert.True(error.Description is null);
        }

       

    }
}
