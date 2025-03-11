using ProvaPub.Models;
using ProvaPub.Providers.Interfaces;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services;

public class CustomerService : PagedListService, ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDateTimeProvider _dateTime;

    public CustomerService(ICustomerRepository customerRepository, IDateTimeProvider dateTime)
    {
        _customerRepository = customerRepository;
        _dateTime = dateTime;
    }

    public CustomerList ListCustomers(int page)
    {
        var query = _customerRepository.GetAll();
        var customers = GetPagedData(query, page, out int totalCount, out bool hasNext);

        return new CustomerList
        {
            Customers = customers,
            TotalCount = totalCount,
            HasNext = hasNext
        };
    }

    public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
    {
        if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

        if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

        //Business Rule: Non registered Customers cannot purchase
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

        //Business Rule: A customer can purchase only a single time per month
        var baseDate = _dateTime.UtcNow.AddMonths(-1);
        var ordersInThisMonth = await _customerRepository.HasPurchasedInLastMonthAsync(customerId, baseDate);
        if (ordersInThisMonth)
            return false;

        //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
        var haveBoughtBefore = await _customerRepository.HasEverPurchasedAsync(customerId);
        if (haveBoughtBefore && purchaseValue > 100)
            return false;

        //Business Rule: A customer can purchases only during business hours and working days
        if (_dateTime.UtcNow.Hour < 8 || _dateTime.UtcNow.Hour > 18 || _dateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday || _dateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday)
            return false;

        return true;
    }
}
