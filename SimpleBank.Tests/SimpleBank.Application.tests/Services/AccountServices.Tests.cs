using AutoBogus;
using Moq;
using SimpleBank.Application.Services;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Repositories;

namespace SimpleBank.Tests.SimpleBankApplication.tests.Services;
public class AccountServicesTests
{
    private readonly AccountService _service;
    private readonly Mock<IAccountRepository> _repositoryMock = new();

    public AccountServicesTests()
    {
        _service = new AccountService(_repositoryMock.Object);
    }

    [Fact]
    public async void GetAccountById_Should_ReturnAccount()
    {
        //Arrange
        var account = AutoFaker.Generate<Account>();

        _repositoryMock.Setup(x => x.GetAccountByIdAsync(account.Id)).ReturnsAsync(account);

        //Act
        var result = _service.GetAccountByIdAsync(account.Id).Result;

        //Assert
        Assert.NotNull(result);
    }
}

//Task<Account> GetAccountByIdAsync(long id);
//Task<Account> GetAccountByNumberAsync(int accountNumber);
//Task<IEnumerable<Account?>> GetAllAsync();
//Task<Account> CreateAccountAsync(CreateAccount createAccount);
//Task<Account> UpdateAccountAsync(int accountNumber, UpdateAccount updateAccount);
//Task<bool> DeleteAccountAsync(int accountNumber);
