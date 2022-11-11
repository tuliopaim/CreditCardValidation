using CreditCardValidation.Tests.UnitTests.Fixtures;
using Moq;

namespace CreditCardValidation.Tests.UnitTests;

[Collection(nameof(ValidateTokenFixtures))]
public class ValidateTokenCommandHandlerTests
{
    private readonly ValidateTokenFixtures _fixtures;

    public ValidateTokenCommandHandlerTests(ValidateTokenFixtures fixtures)
	{
        _fixtures = fixtures;
    }

    [Fact]
    public async Task ShouldBe_Invalid_When_CreateDate_MoreThan_30Min()
    {
        //arrange
        var currentTime = DateTime.UtcNow;
        var previousTime = currentTime.AddHours(-1);
        var cvv = 999;
        var input = _fixtures.GetValidateTokenCommandInput(cvv);

        var creditCard = _fixtures.GetCreditCard(previousTime, cvv);

        _fixtures.CreditCardRepositoryMock
            .Setup(x => x.GetToValidateToken(It.IsAny<int>()))
            .ReturnsAsync(creditCard);

        _fixtures.DateTimeProviderMock
            .Setup(x => x.UtcNow)
            .Returns(currentTime);

        var handler = _fixtures.GetCommandHandler();

        //act

        var result = await handler.Handle(input, default);

        //assert
        Assert.False(result.Validated);
    }

    [Fact]
    public async Task ShouldBe_Invalid_When_Customer_IsNot_TheOwner()
    {
        //arrange
        var currentTime = DateTime.UtcNow;
        var previousTime = currentTime.AddMinutes(10);
        var dbCustomerId = 1;
        var inputCustomerId = 5;
        var cvv = 999;
        var input = _fixtures.GetValidateTokenCommandInput(cvv, customerId: inputCustomerId);

        var creditCard = _fixtures.GetCreditCard(previousTime, cvv, dbCustomerId);

        _fixtures.CreditCardRepositoryMock
            .Setup(x => x.GetToValidateToken(It.IsAny<int>()))
            .ReturnsAsync(creditCard);

        _fixtures.DateTimeProviderMock
            .Setup(x => x.UtcNow)
            .Returns(currentTime);

        var handler = _fixtures.GetCommandHandler();

        //act

        var result = await handler.Handle(input, default);

        //assert
        Assert.False(result.Validated);
    }

    [Fact]
    public async Task ShouldBe_Invalid_When_Invalid_Token()
    {
        //arrange
        var currentTime = DateTime.UtcNow;
        var previousTime = currentTime.AddMinutes(10);
        var customerId = 1;
        var cvv = 005;
        var token = 40;
        var input = _fixtures.GetValidateTokenCommandInput(cvv, token: token, customerId: customerId);

        var creditCard = _fixtures.GetCreditCard(previousTime, cvv, customerId);

        _fixtures.CreditCardRepositoryMock
            .Setup(x => x.GetToValidateToken(It.IsAny<int>()))
            .ReturnsAsync(creditCard);

        _fixtures.DateTimeProviderMock
            .Setup(x => x.UtcNow)
            .Returns(currentTime);

        var handler = _fixtures.GetCommandHandler();

        //act

        var result = await handler.Handle(input, default);

        //assert
        Assert.False(result.Validated);
    }

    [Fact]
    public async Task ShouldBe_Valid()
    {
        //arrange
        var currentTime = DateTime.UtcNow;
        var previousTime = currentTime.AddMinutes(10);
        var customerId = 1;
        var cvv = 005;
        var token = 14;
        var input = _fixtures.GetValidateTokenCommandInput(cvv, token: token, customerId: customerId);

        var creditCard = _fixtures.GetCreditCard(previousTime, cvv, customerId);

        _fixtures.CreditCardRepositoryMock
            .Setup(x => x.GetToValidateToken(It.IsAny<int>()))
            .ReturnsAsync(creditCard);

        _fixtures.DateTimeProviderMock
            .Setup(x => x.UtcNow)
            .Returns(currentTime);

        var handler = _fixtures.GetCommandHandler();

        //act

        var result = await handler.Handle(input, default);

        //assert
        Assert.True(result.Validated);
    }
}
