using AutoBogus;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Tests.Core.Domain
{
    internal class AccountTests
    {
        public AccountTests() 
        { 
        }

        [Fact]
        public void FromCreateAccount_Should_SetPropieties ()
        {
            //Arrange

            var createAccount = AutoFaker.Generate<CreateAccount>();

            //Act
            var result = new Account().FromCreateAccount(createAccount);

            //Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(Status.Active);
            result.Balance.Should().Be(0.00M);
        }
    }
}
