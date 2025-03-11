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
        var currentDateTime = _dateTime.UtcNow;
        if (await HasPurchasedInLastMonth(customerId, currentDateTime))
            return false;

        //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
        if (!await HasMadePreviousPurchase(customerId) && purchaseValue > 100)
            return false;

        //Business Rule: A customer can purchases only during business hours and working days
        if (IsOutsideBusinessHours(_dateTime.UtcNow) || IsWeekend(_dateTime.UtcNow))
            return false;

        return true;
    }
    
    private bool IsOutsideBusinessHours(DateTime dateTime)
    {
        return dateTime.Hour < 8 || dateTime.Hour > 18;
    }

    private bool IsWeekend(DateTime dateTime)
    {
        return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
    }
    
    private async Task<bool> HasPurchasedInLastMonth(int customerId, DateTime currentDateTime)
    {
        var oneMonthAgo = currentDateTime.AddMonths(-1);
        return await _customerRepository.HasPurchasedInLastMonthAsync(customerId, oneMonthAgo);
    }

    private async Task<bool> HasMadePreviousPurchase(int customerId)
    {
        return await _customerRepository.HasEverPurchasedAsync(customerId);
    }
}
