using Microsoft.EntityFrameworkCore;

using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly TestDbContext _context;

    public CustomerRepository(TestDbContext context)
    {
        _context = context;
    }

    public IQueryable<Customer?> GetAll()
    {
        return _context.Customers;
    }

    public async Task<Customer?> GetByIdAsync(int customerId)
    {
        return await _context.Customers.FindAsync(customerId);
    }

    public async Task<bool> HasPurchasedInLastMonthAsync(int customerId, DateTime baseDate)
    {
        var ordersInThisMonth = await _context.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
        return ordersInThisMonth > 0;
    }

    public async Task<bool> HasEverPurchasedAsync(int customerId)
    {
        var hasPurchasedBefore = await _context.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
        return hasPurchasedBefore > 0;
    }
}
