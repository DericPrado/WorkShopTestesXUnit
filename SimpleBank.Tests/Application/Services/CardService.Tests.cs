using AutoBogus;
using FluentAssertions;
using Moq;
using SimpleBank.Application.Services;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;
using SimpleBank.Core.Repositories;
using System.Security.Principal;

namespace SimpleBank.Tests.Application.Services;
public class CardServiceTests
{
    private readonly CardService _service;
    private readonly Mock<ICardRepository> _repositoryCardMock = new();
    private readonly Mock<IAccountRepository> _repositoryAccountMock = new();

    public CardServiceTests()
    {
        _service = new CardService(_repositoryCardMock.Object, _repositoryAccountMock.Object);
    }

    [Fact]
    [Trait("CardServices", "GetCardByNumberAsync")]
    public async void GetCardByNumberAsync_Should_ReturnCard()
    {
        //Arrange
        var card = AutoFaker.Generate<Card>();

        _repositoryCardMock.Setup(x => x.GetCardByNumberAsync(card.CardNumber)).ReturnsAsync(card);

        //Act
        var result = _service.GetCardByNumberAsync(card.CardNumber);

        //Assert
        result.Should().NotBeNull();
        _repositoryCardMock.Verify(x => x.GetCardByNumberAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    [Trait("CardServices", "GetCardsByAccountAsync")]
    public async void GetCardsByAccountNumberAsync_Should_ReturnCardList()
    {
        //Arrange
        var account = AutoFaker.Generate<Account>();
        var cards = AutoFaker.Generate<Card>(5);

        _repositoryCardMock.Setup(x => x.GetCardsByAccountNumberAsync(account.AccountNumber)).ReturnsAsync(cards);

        //Act
        var result = _service.GetCardsByAccountAsync(account.AccountNumber);

        //Assert
        result.Result.Should().NotBeNull();
        _repositoryCardMock.Verify(x => x.GetCardsByAccountNumberAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    [Trait("CardServices", "CreateCardAsync")]
    public async void CreateCardAsync_Should_ReturnCard()
    {
        //Arrange
        var account = AutoFaker.Generate<Account>();
        var card = AutoFaker.Generate<Card>();
        var createCard = AutoFaker.Generate<CreateCard>();

        _repositoryAccountMock.Setup(x => x.GetAccountByNumberAsync(It.IsAny<int>())).ReturnsAsync(account);

        //Act
        var result = _service.CreateCardAsync(account.AccountNumber, createCard);

        //Assert
        result.Should().NotBeNull();
        result.IsCompletedSuccessfully.Should().BeTrue();
        _repositoryCardMock.Verify(x => x.GetNextCardNumberAsync(), Times.Once);
    }

    [Fact]
    [Trait("CardServices", "UpdateCardAsync")]
    public async void UpdateCardAsync_Should_ReturnCard()
    {
        //Arrange
        var account = AutoFaker.Generate<Account>();
        long cardNumber = 56454543463;
        var updateCard = AutoFaker.Generate<UpdateCard>();

        _repositoryAccountMock.Setup(x => x.GetAccountByNumberAsync(It.IsAny<int>())).ReturnsAsync(account);

        //Act
        var result = _service.UpdateCardAsync(account.AccountNumber, cardNumber, updateCard);

        //Assert
        result.Should().NotBeNull();
        result.IsCompletedSuccessfully.Should().BeTrue();
        _repositoryCardMock.Verify(x => x.GetCardByNumberAsync(It.IsAny<long>()), Times.Once);
    }
}
