using CreditCardValidation.Tests.IntegrationTests.Fixtures;
using System.Net;

namespace CreditCardValidation.Tests.IntegrationTests;

[Collection(nameof(CreditCardFixtures))]
public class CreateCreditCardIntegrationTests
{
    private readonly CreditCardFixtures _fixtures;

    public CreateCreditCardIntegrationTests(CreditCardFixtures fixtures)
	{
        _fixtures = fixtures;
    }

    [Fact]
    public async Task Must_Return_BadRequest_When_InvalidInput()
    {
        //arrange
        var client = _fixtures.GetSampleApplication().CreateClient();
        var invalidCommand = _fixtures.InvalidCreateCreditCardCommandInput();
        //act
        var httpResult = await _fixtures.PostCreateCreditCardEndpoint(client, invalidCommand);
        //assert
        Assert.Equal(HttpStatusCode.BadRequest, httpResult.StatusCode);
        Assert.Null(httpResult.SuccessResponse);
        Assert.NotNull(httpResult.ErrorResponse);
        Assert.Equal(3, httpResult.ErrorResponse.Errors.Count);
    }

    [Fact]
    public async Task Must_Return_BadRequest_When_CustomerNotFound()
    {
        //arrange
        var client = _fixtures.GetSampleApplication().CreateClient();
        var invalidCommand = _fixtures.CreateCreditCardCommandInputWithNonExistentCustomer();

        //act
        var httpResult = await _fixtures.PostCreateCreditCardEndpoint(client, invalidCommand);

        //assert
        Assert.Equal(HttpStatusCode.BadRequest, httpResult.StatusCode);
        Assert.Null(httpResult.SuccessResponse);
        Assert.NotNull(httpResult.ErrorResponse);
        Assert.Single(httpResult.ErrorResponse.Errors);
    }

    [Fact]
    public async Task MustReturn_OK_WithToken()
    {
        //arrange
        var client = _fixtures.GetSampleApplication().CreateClient();
        var invalidCommand = _fixtures.CreateValidCreditCardCommandInput();

        //act
        var httpResult = await _fixtures.PostCreateCreditCardEndpoint(client, invalidCommand);

        //assert
        Assert.Equal(HttpStatusCode.OK, httpResult.StatusCode);
        Assert.Null(httpResult.ErrorResponse);
        Assert.NotNull(httpResult.SuccessResponse);
        Assert.Equal(4001, httpResult.SuccessResponse.Result.Token);
    }
}
