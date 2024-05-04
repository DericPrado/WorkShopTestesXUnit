using AutoBogus;
using FluentAssertions;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;
using CardType = SimpleBank.Core.Domains.Enums.CardType;

namespace SimpleBank.Tests.Core.Domain;

public class CardTests
{
    public CardTests() 
    { 
    }

    [Fact]
    [Trait("CardTests", "FromCreateCard")]
    public async void FromCreateCard_Should_ReturnCard()
    {
        //Arrange
        var createCard = AutoFaker.Generate<CreateCard>();
        int accountId = 123;
        long cardNumber = 534758348545;

        //Act
        var result = new Card().FromCreateCard(createCard, accountId, cardNumber);

        //Assert
        result.Should().NotBeNull();
        result.AccountId.Should().Be(accountId);
        result.CardNumber.Should().Be(cardNumber);
    }

    [Fact]
    [Trait("CardTests", "FromUpdateCard")]
    public async void FromUpdateCard_Should_ReturnCard()
    {
        //Arrange
        var updateCard = AutoFaker.Generate<UpdateCard>();

        //Act
        var result = new Card().FromUpdateCard(updateCard);

        //Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(updateCard.Status);
    }

    [Theory]
    [InlineData(CardType.Simple, 18)]
    [InlineData(CardType.Silver, 24)]
    [InlineData(CardType.Gold, 36)]
    [InlineData(CardType.Platinum, 48)]
    [Trait("CardTests", "GenerateExpiration")]
    public async void GenerateExpiration_Should_ReturnExpiration(CardType cardType, int months)
    {
        //Arrange & Act
        var result = new Card().GenerateExpiration(cardType);

        //Assert
        result.Should().NotBeNull();
        result.Equals(DateTime.UtcNow.AddMonths(months).ToString("MM/yyyy"));
    }
}
