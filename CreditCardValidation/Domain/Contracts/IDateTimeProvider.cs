namespace CreditCardValidation.Domain.Contracts;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
