namespace CreditCardValidation.Commands;

public class ErrorCommandResponse
{
    public bool IsValid  => !Errors.Any();
    public required List<string> Errors { get; set; } = new();
}
