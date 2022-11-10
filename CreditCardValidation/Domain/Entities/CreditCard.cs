namespace CreditCardValidation.Domain.Entities;

public class CreditCard
{
    protected CreditCard()
    {
    }

    public CreditCard(
        int customerId,
        long number,
        DateTime registredAt)
    {
        CustomerId = customerId;
        Number = number;
        TokenRegisteredAt = registredAt;
    }

    public int CardId { get; private set; }
    public int CustomerId { get; private set; }
    public long Number { get; private set; }
    public DateTime TokenRegisteredAt { get; private set; }

    public virtual Customer Customer { get; set; }

    public long CreateToken(int cvv)
    {
        int[] digits = LastFourDigitsOfNumber();

        var iterationTimes = cvv % 4;

        for (int i = 0; i < iterationTimes; i++)
        {
            (digits[0],
             digits[1],
             digits[2],
             digits[3]) 
             = 
             (digits[3],
             digits[0],
             digits[1],
             digits[2]);
        }

        return long.Parse(string.Concat(digits));
    }

    public int[] LastFourDigitsOfNumber()
    {
        var numberStr = Number.ToString();
        var lastFourIndex = numberStr.Length - 4;

        return numberStr[lastFourIndex..]
            .Select(c => int.Parse(c.ToString()))
            .ToArray();
    }
}
