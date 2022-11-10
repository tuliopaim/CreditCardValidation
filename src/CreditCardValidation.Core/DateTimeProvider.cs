using CreditCardValidation.Core.Contracts;

namespace CreditCardValidation.Core;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
