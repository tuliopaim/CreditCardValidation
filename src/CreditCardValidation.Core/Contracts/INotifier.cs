namespace CreditCardValidation.Core.Contracts;

public interface INotifier
{
    IReadOnlyList<string> Notifications { get; }
    bool IsValid { get; }
    void Notify(string notification);
}
