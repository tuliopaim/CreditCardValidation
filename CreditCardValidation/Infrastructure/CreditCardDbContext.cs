using CreditCardValidation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditCardValidation.Infrastructure;

public class CreditCardDbContext : DbContext
{
    public CreditCardDbContext(DbContextOptions<CreditCardDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers {  get; set; }
    public DbSet<CreditCard> CreditCards {  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CreditCard>(config =>
        {
            config.HasKey(x => x.CardId);

            config.Property(x => x.CardId).ValueGeneratedOnAdd();

            config
                .HasOne(cc => cc.Customer)
                .WithMany(ct => ct.CreditCards)
                .HasForeignKey(cc => cc.CustomerId);
        });

        modelBuilder.Entity<Customer>(config =>
        {
            config.HasKey(x => x.CustomerId);

            config.Property(x => x.CustomerId).ValueGeneratedOnAdd();

            config.HasData(
                new Customer(1, "Customer X"),
                new Customer(5, "Customer Y"));
        });
    }
}
