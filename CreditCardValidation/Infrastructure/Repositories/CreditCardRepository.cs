using CreditCardValidation.Domain.Contracts;
using CreditCardValidation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditCardValidation.Infrastructure.Repositories;

public class CreditCardRepository : ICreditCardRepository
{
    private readonly CreditCardDbContext _creditCardDbContext;

    public CreditCardRepository(CreditCardDbContext creditCardDbContext)
    {
        _creditCardDbContext = creditCardDbContext;
    }

    public void Add(CreditCard creditCard)
    {
        _creditCardDbContext.CreditCards.Add(creditCard);
    }

    public Task<CreditCard?> Get(int id)
    {
        return _creditCardDbContext.CreditCards.FirstOrDefaultAsync(x => x.CardId == id);
    }

    public Task SaveChanges(CancellationToken cancellationToken = default)
    {
        return _creditCardDbContext.SaveChangesAsync(cancellationToken);
    }
}
