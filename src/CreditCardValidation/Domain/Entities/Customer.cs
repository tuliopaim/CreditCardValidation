namespace CreditCardValidation.Domain.Entities;
public class Customer
{
    protected Customer()
    {
    }

    public Customer(int customerId, string name)
    {
        CustomerId = customerId;
        Name = name;
    }

    public int CustomerId { get; private set; }
    public string Name { get; private set; } = "";

    public List<CreditCard> CreditCards { get; private set; } = new();
}
