namespace ProvaPub.Models;

public class CustomerList : PagedList<Customer>
{
    public List<Customer> Customers
    {
        get => Items;
        set => Items = value;
    }
}
