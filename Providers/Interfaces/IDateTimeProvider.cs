namespace ProvaPub.Providers.Interfaces;

public interface IDateTimeProvider
{
     /// <summary>
     /// Gets the current Coordinated Universal Time (UTC).
     /// </summary>
     /// <remarks>
     /// This property provides the current date and time in UTC, which is independent
     /// of the local time zone. It is often used in scenarios where time needs to be
     /// standardized, such as in logging, scheduling, or data synchronization across
     /// different regions.
     /// </remarks>
     DateTime UtcNow { get; }
}
