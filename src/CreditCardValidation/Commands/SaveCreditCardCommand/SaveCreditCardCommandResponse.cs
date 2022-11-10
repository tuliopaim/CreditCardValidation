namespace CreditCardValidation.Commands.SaveCreditCardCommand;

public class SaveCreditCardCommandResponse
{ 
    public DateTime RegistrationDate { get; set; }
    public long Token { get; set; }
    public int CardId { get; set; }
}
