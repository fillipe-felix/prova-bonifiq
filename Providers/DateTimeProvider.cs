using ProvaPub.Providers.Interfaces;

namespace ProvaPub.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
