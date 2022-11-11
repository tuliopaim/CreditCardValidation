using CreditCardValidation.Commands.ValidateTokenCommand;
using CreditCardValidation.Core;
using CreditCardValidation.Core.Contracts;
using CreditCardValidation.Domain.Contracts;
using CreditCardValidation.Domain.Entities;
using CreditCardValidation.Tests.IntegrationTests.Base;
using Moq;

namespace CreditCardValidation.Tests.UnitTests.Fixtures;

public class ValidateTokenFixtures : IntegrationTestBase
{
    public Mock<IDateTimeProvider> DateTimeProviderMock { get; set; } = new();
    public Mock<ICreditCardRepository> CreditCardRepositoryMock { get; set; } = new();

    public ValidateTokenCommandHandler GetCommandHandler()
    {
        return new ValidateTokenCommandHandler(
            CreditCardRepositoryMock.Object,
            DateTimeProviderMock.Object,
            new Notifier());
    }

    public CreditCard GetCreditCard(
        DateTime date = default,
        int cvv = default,
        int customerId = default)
    {
        var creditCard = new CreditCard(customerId, 4607_3808_1998_0140);

        _ = creditCard.CreateToken(cvv, date);

        return creditCard;
    }

    public ValidateTokenCommandInput GetValidateTokenCommandInput(
        int cvv = default,
        long token = default,
        int customerId = default)
    {
        return new ValidateTokenCommandInput 
        {
            CardId = 1,
            CustomerId = customerId,
            CVV = cvv, 
            Token = token,
        };
    }

}

[CollectionDefinition(nameof(ValidateTokenFixtures))]
public class SaveCreditCardFixturesCollection : ICollectionFixture<ValidateTokenFixtures>
{ }
