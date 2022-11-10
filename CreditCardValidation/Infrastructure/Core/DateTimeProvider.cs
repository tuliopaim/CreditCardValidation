using CreditCardValidation.Domain.Contracts;

namespace CreditCardValidation.Infrastructure.Core;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
