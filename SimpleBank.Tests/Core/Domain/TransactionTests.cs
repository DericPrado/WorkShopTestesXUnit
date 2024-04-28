using AutoBogus;
using FluentAssertions;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Tests.Core.Domain;

public class TransactionTests
{
    public TransactionTests() 
    {
    }

    [Fact]
    [Trait("TransactionTests", "UnitTest")]
    public async void FromCreateTransaction_Should_ReturnTransaction()
    {
        //Arrange
        var createTransaction = AutoFaker.Generate<CreateTransaction>();
        int cardId = 5;

        //Act
        var result = new Transaction().FromCreateTransaction(cardId, createTransaction);

        //Assert
        result.Should().NotBeNull();
        result.TransactionType.Should().Be(createTransaction.TransactionType);
        result.Amount.Should().Be(createTransaction.Amount);
        result.CardId.Should().Be(cardId);
    }
}
