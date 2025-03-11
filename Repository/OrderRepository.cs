using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly TestDbContext _context;

    public OrderRepository(TestDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
}
