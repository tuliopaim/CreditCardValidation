using CreditCardValidation.Domain.Entities;

namespace CreditCardValidation.Tests.UnitTests;

public class CreditCardEntityTests
{
    [Theory]
    [InlineData(001, 4607_3808_1998_0140, 0014)]
    [InlineData(002, 4607_3808_1998_0140, 4001)]
    [InlineData(003, 4607_3808_1998_0140, 1400)]
    [InlineData(004, 4607_3808_1998_0140, 0140)]
    [InlineData(005, 4607_3808_1998_0140, 0014)]
    [InlineData(010, 4607_3808_1998_0140, 4001)]
    public void ShouldCreateTokenCorrectly(
        int cvv,
        long creditCardNumber,
        long expectedToken)
    {
        var creditCard = new CreditCard(45, creditCardNumber);

        var token = creditCard.CreateToken(cvv, DateTime.UtcNow);

        Assert.Equal(expectedToken, token);
    }
}