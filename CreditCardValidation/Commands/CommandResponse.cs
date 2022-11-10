namespace CreditCardValidation.Commands;

public class CommandResponse<T>
{
    public bool IsValid { get; } = true;
    public required T Result { get; set; }
}
