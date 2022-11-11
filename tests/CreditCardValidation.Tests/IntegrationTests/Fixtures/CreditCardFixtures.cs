using CreditCardValidation.Commands.SaveCreditCardCommand;
using CreditCardValidation.Commands.ValidateTokenCommand;
using CreditCardValidation.Tests.IntegrationTests.Base;

namespace CreditCardValidation.Tests.IntegrationTests.Fixtures;

public class CreditCardFixtures : IntegrationTestBase
{
    public SaveCreditCardCommandInput InvalidCreateCreditCardCommandInput()
    {
        return new()
        {
            CardNumber = 0,
            CustomerId = 0,
            CVV = 0
        };
    }

    public SaveCreditCardCommandInput CreateCreditCardCommandInputWithNonExistentCustomer()
    {
        return new()
        {
            CardNumber = 4607_3808_1998_0140,
            CustomerId = 100,
            CVV = 999
        };
    }

    public SaveCreditCardCommandInput CreateValidCreditCardCommandInput()
    {
        return new()
        {
            CardNumber = 4607_3808_1998_0140,
            CustomerId = 1,
            CVV = 10
        };
    }

    public ValidateTokenCommandInput CreateInvalidValidateTokenCommandInput()
    {
        return new()
        {
            CardId = 0,
            CustomerId = 0,
            CVV = 0,
            Token = 0
        };
    }

    public ValidateTokenCommandInput CreateValidateTokenWithNonExistingCardCommandInput()
    {
        return new()
        {
            CardId = 50,
            CustomerId = 1,
            CVV = 99,
            Token = 1
        };
    }

    public ValidateTokenCommandInput CreateValidateTokendCommandInput()
    {
        return new()
        {
            CardId = 1,
            CustomerId = 1,
            CVV = 99,
            Token = 1
        };
    }

    public Task<HttpResult<SaveCreditCardCommandResponse>> PostCreateCreditCardEndpoint(
        HttpClient client,
        SaveCreditCardCommandInput input)
    {
        return base.Post<SaveCreditCardCommandInput, SaveCreditCardCommandResponse>(
            client, "CreditCard", input);
    }

    public Task<HttpResult<ValidateTokenCommandResponse>> GetCreateCreditCardEndpoint(
        HttpClient client,
        Dictionary<string, string?> queryParams)
    {
        return base.Get<ValidateTokenCommandResponse>(client,  "CreditCard/validate-token", queryParams);
    }
}

[CollectionDefinition(nameof(CreditCardFixtures))]
public class SaveCreditCardFixturesCollection : ICollectionFixture<CreditCardFixtures>
{ }
