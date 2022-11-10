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

    public int CustomerId { get; set; }
    public string Name { get; set; } = "";
}
