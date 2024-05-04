using AutoBogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Infra.Models;
using SimpleBank.Infra.Repositories;

namespace SimpleBank.Tests.Infra.Repository;

public class AccountRepositoryTests
{
    private readonly AccountRepository _repository;
    private readonly SimpleBankContext _context;

    public AccountRepositoryTests ()
    {
        var options = new DbContextOptionsBuilder<SimpleBankContext>()
            .UseInMemoryDatabase(databaseName: "SimpleBankDbMock")
            .Options;

        _context = new SimpleBankContext (options);
        _repository = new AccountRepository(_context);
    }

    [Fact]
    [Trait("AccountRepository", "GetAccountByIdAsync")]
    public async Task GetAccountByIdAsync_Should_ReturnAccountWithSuccess()
    {
        //Arrange
        var id = 1;
        var account = new AutoFaker<Account>()
            .RuleFor(x => x.Id, id)
            .Generate();

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        //Act
        var result = _repository.GetAccountByIdAsync(1);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(id);
    }

    [Fact]
    [Trait("AccountRepository", "GetAccountByIdAsyncUnSuccess")]
    public async Task GetAccountByIdAsync_Should_ReturnAccountWithUnSuccess()
    {
        //Arrange
        var account = new AutoFaker<Account>()
            .Generate();

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        //Act
        var result = _repository.GetAccountByIdAsync(100);

        //Assert
        result.Result.Should().BeNull();
    }

    [Fact]
    [Trait("AccountRepository", "GetAccountByNumberAsync")]
    public async Task GetAccountByNumberAsync_Should_ReturnAccountWithSuccess()
    {
        //Arrange
        var accountNumber = 123456;
        var account = new AutoFaker<Account>()
            .RuleFor(x => x.AccountNumber, accountNumber)
            .Generate();

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        //Act
        var result = _repository.GetAccountByNumberAsync(accountNumber).Result;

        //Assert
        result.Should().NotBeNull();
        result.AccountNumber.Should().Be(accountNumber);
    }

    [Fact]
    [Trait("AccountRepository", "GetNextAccountNumberAsync")]
    public async Task GetNextAccountNumberAsync_Should_ReturnId()
    {
        //Arrange & Act
        var result = _repository.GetNextAccountNumberAsync();

        //Assert
        result.Should().NotBeNull();
    }
}
