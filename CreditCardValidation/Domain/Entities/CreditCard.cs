﻿namespace CreditCardValidation.Domain.Entities;

public class CreditCard
{
    protected CreditCard()
    {
    }

    public CreditCard(
        int customerId,
        int cvv,
        long number,
        DateTime registredAt)
    {
        CustomerId = customerId;
        CVV = cvv;
        Number = number;
        RegistredAt = registredAt;
    }

    public int CardId { get; private set; }
    public int CustomerId { get; private set; }
    public long Number { get; private set; }
    public int CVV { get; private set; }
    public DateTime RegistredAt { get; private set; }

    public long CreateToken()
    {
        int[] digits = LastFourDigitsOfNumber();

        var iterationTimes = CVV % 4;

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
