namespace ProvaPub.Services.Interfaces;

public interface IRandomService
{
    /// <summary>
    /// Generates a random integer, stores it in the database, and returns the generated value.
    /// </summary>
    /// <returns>
    /// A randomly generated integer value.
    /// </returns>
    Task<int> GetRandom();
}
