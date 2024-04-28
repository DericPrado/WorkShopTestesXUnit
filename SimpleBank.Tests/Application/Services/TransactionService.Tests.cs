using AutoBogus;
using FluentAssertions;
using Moq;
using SimpleBank.Application.Services;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;
using SimpleBank.Core.Repositories;

namespace SimpleBank.Tests.Application.Services;
public class TransactionServiceTests
{
    private readonly TransactionService _service;
    private readonly Mock<ICardRepository> _repositoryCardMock = new();
    private readonly Mock<ITransactionRepository> _repositoryTransactionMock = new();

    public TransactionServiceTests()
    {
        _service = new TransactionService(_repositoryTransactionMock.Object, _repositoryCardMock.Object);
    }

    [Fact]
    [Trait("TransactionServiceTests", "GetTransactionsByCardNumberAsync")]
    public async void GetTransactionsByCardNumberAsync_Should_ReturnTransactionList()
    {
        //Arrange
        var card = AutoFaker.Generate<Card>();
        var transactions = AutoFaker.Generate<Transaction>(25);

        _repositoryTransactionMock.Setup(x => x.GetTransactionsByCardNumberAsync(card.CardNumber)).ReturnsAsync(transactions);

        //Act
        var result = _service.GetTransactionsByCardNumberAsync(card.CardNumber);

        //Assert
        result.Result.Should().NotBeNull();
        _repositoryTransactionMock.Verify(x => x.GetTransactionsByCardNumberAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    [Trait("TransactionServiceTests", "CreateTransactionAsync")]
    public async void CreateTransactionAsync_Should_ReturnTransaction()
    {
        //Arrange
        var card = AutoFaker.Generate<Card>();
        var createTransaction = AutoFaker.Generate<CreateTransaction>();

        _repositoryCardMock.Setup(x => x.GetCardByNumberAsync(It.IsAny<long>())).ReturnsAsync(card);

        //Act
        var result = _service.CreateTransactionAsync(card.CardNumber, createTransaction);

        //Assert
        result.Should().NotBeNull();
        _repositoryTransactionMock.Verify(x => x.CreateTransactionAsync(It.IsAny<Transaction>()), Times.Once);
    }
}

//Task<Transaction> CreateTransactionAsync(long cardNumber, CreateTransaction transaction);