using SimpleBank.Core.Domains.DTOs;
using SimpleBank.Core.Domains.Enums;

namespace SimpleBank.Tests.Core.Domains.DTOs;

public class CreateAccountTets 
{
    [Fact]
    public void GivenRecord_Shoud_SetProperties()
    {
        //Arrange
        var identificationNumber = Guid.NewGuid().ToString();
        var holderName = "Deric";
        var birthDate = new DateTime(2001, 08, 03);
        var gender = Gender.Male;
        var email = "deric.prado@bit.com.br";

        //Act
        var createAccount = new CreateAccount(identificationNumber, holderName, birthDate, gender, email);

        //Assert
        //createAccount.IdentificationNumber.Should().Be(identificationNumber);
        Assert.Equal(identificationNumber, createAccount.IdentificationNumber);
        Assert.Equal(holderName, createAccount.HolderName);
        Assert.Equal(birthDate, createAccount.BirthDate);
        Assert.Equal(gender, createAccount.Gender);
        Assert.Equal(email, createAccount.Email);

    }
}
