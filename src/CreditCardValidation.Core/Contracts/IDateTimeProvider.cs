namespace CreditCardValidation.Core.Contracts;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
