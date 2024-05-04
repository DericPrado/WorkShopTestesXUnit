using AutoBogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleBank.Application.Services;
using SimpleBank.Application.Services.Interfaces;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;
using SimpleBank.WebAPI.Controllers;
using System.Net;

namespace SimpleBank.Tests.WebAPI.Controllers;

public class AccountControllerTest
{
    private readonly AccountController _controller;
    private readonly Mock<IAccountService> _serviceMock = new();

    public AccountControllerTest()
    {
        _controller = new AccountController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetAccountById_should_ReturnOkResult()
    {
        //Arrange
        var id = 1;
        var account = new AutoFaker<Account>()
            .RuleFor(x => x.Id, id)
            .Generate();

        _serviceMock.Setup(x => x.GetAccountByIdAsync(id)).ReturnsAsync(account);

        //Act
        var result = await _controller.GetAccountById(id) as ObjectResult;

        //Assert
        result.Should().NotBeNull();
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task GetAccountById_should_ReturnNotFoundResult()
    {
        //Arrange
        var id = 1;

        //Act
        var result = await _controller.GetAccountById(id) as ObjectResult;

        //Assert
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task PostAccount_Should_ReturnAcceptedResult()
    {
        //Arrange
        var createAccount = new AutoFaker<CreateAccount>().Generate();

        _serviceMock.Setup(x => x.CreateAccountAsync(createAccount)).ReturnsAsync(new Account().FromCreateAccount(createAccount));

        //Act
        var result = await _controller.Post(createAccount) as ObjectResult;

        //Assert
        Assert.Equal((int)HttpStatusCode.Accepted, result.StatusCode);
    }

    [Fact(Skip = "Precisa de ajustes")]
    public async Task GetAll_Should_ReturnAccountsOkResult()
    {
        //Arrange 
        var accounts = new AutoFaker<Account>().Generate(10);

        _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(accounts);
        
        //Act
        var result = await _controller.GetAll() as ObjectResult;

        //Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }
}
