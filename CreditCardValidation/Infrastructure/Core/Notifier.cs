using CreditCardValidation.Domain.Contracts;

namespace CreditCardValidation.Infrastructure.Core;

public class Notifier : INotifier
{
    private readonly List<string> _notifications = new();
    public IReadOnlyList<string> Notifications => _notifications;

    public bool IsValid => Notifications.Count == 0;

    public void Notify(string notification)
    {
        _notifications.Add(notification);
    }
}
