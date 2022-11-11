using CreditCardValidation.Commands.ValidateTokenCommand;
using CreditCardValidation.Tests.IntegrationTests.Fixtures;
using System.Net;

namespace CreditCardValidation.Tests.IntegrationTests;

[Collection(nameof(CreditCardFixtures))]
public class ValidateTokenIntegrationTests
{
    private readonly CreditCardFixtures _fixtures;

    public ValidateTokenIntegrationTests(CreditCardFixtures fixtures)
	{
        _fixtures = fixtures;
    }

    [Fact]
    public async Task Must_Return_BadRequest_When_InvalidInput()
    {
        //arrange
        var client = _fixtures.GetSampleApplication().CreateClient();
        var invalidCommand = _fixtures.CreateInvalidValidateTokenCommandInput();
        Dictionary<string, string> queryParams = GenerateQueryParams(invalidCommand);

        //act
        var httpResult = await _fixtures.GetCreateCreditCardEndpoint(client, queryParams);

        //assert
        Assert.Equal(HttpStatusCode.BadRequest, httpResult.StatusCode);
        Assert.Null(httpResult.SuccessResponse);
        Assert.NotNull(httpResult.ErrorResponse);
        Assert.Equal(3, httpResult.ErrorResponse.Errors.Count);
    }

    [Fact]
    public async Task Must_Return_BadRequest_When_CreditCardNotFound()
    {
        //arrange
        var client = _fixtures.GetSampleApplication().CreateClient();
        var invalidCommand = _fixtures.CreateValidateTokenWithNonExistingCardCommandInput();
        Dictionary<string, string> queryParams = GenerateQueryParams(invalidCommand);

        //act
        var httpResult = await _fixtures.GetCreateCreditCardEndpoint(client, queryParams);

        //assert
        Assert.Equal(HttpStatusCode.BadRequest, httpResult.StatusCode);
        Assert.Null(httpResult.SuccessResponse);
        Assert.NotNull(httpResult.ErrorResponse);
        Assert.Single(httpResult.ErrorResponse.Errors);
    }

    private static Dictionary<string, string> GenerateQueryParams(ValidateTokenCommandInput input)
    {
        return new Dictionary<string, string>
        {
            { "customerId", input.CustomerId.ToString() },
            { "cardId", input.CardId.ToString() },
            { "token", input.Token.ToString() },
            { "cvv", input.CVV.ToString() },
        };
    }
}
