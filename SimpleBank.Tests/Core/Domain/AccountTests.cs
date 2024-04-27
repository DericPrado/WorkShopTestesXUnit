using AutoBogus;
using FluentAssertions;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Tests.Core.Domain;
public class AccountTests
{
    public AccountTests() 
    { 
    }

    [Fact]
    public void FromCreateAccount_Should_SetPropieties()
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

    [Fact]
    public void FromUpdateAccount_Should_SetPropieties()
    {
        //Arrange
        var updateAcount = AutoFaker.Generate<UpdateAccount>();

        //Act
        var result = new Account().FromUpdateAccount(updateAcount);

        //Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(updateAcount.Status);
        result.Email.Should().Be(updateAcount.Email);
    }
}
