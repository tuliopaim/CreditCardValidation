using CreditCardValidation.Domain.Entities;

namespace CreditCardValidation.Tests;

public class CreditCardTests
{
    [Theory]
    [InlineData(001, 4607380819980140, 0014)]
    [InlineData(002, 4607380819980140, 4001)]
    [InlineData(003, 4607380819980140, 1400)]
    [InlineData(004, 4607380819980140, 0140)]
    [InlineData(005, 4607380819980140, 0014)]
    [InlineData(010, 4607380819980140, 4001)]
    public void ShouldCreateTokenCorrectly(
        int cvv,
        long creditCardNumber,
        long expectedToken)
    {
        var creditCard = new CreditCard(
            0,
            creditCardNumber,
            DateTime.Now);

        var token = creditCard.CreateToken(cvv);

        Assert.Equal(expectedToken, token);
    }
}