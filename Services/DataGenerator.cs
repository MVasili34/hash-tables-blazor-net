using Bogus;

namespace Services;

public class DataGenerator
{
    private static List<string> _series = new() { "AB", "PP", "TT", "OL", "PM" };

    /// <summary>
    /// Method for generating certain <see langword="amount"/> clients
    /// </summary>
    /// <param name="amount">Amount of clients</param>
    /// <returns><see cref="IEnumerable{Client}"/> collection of <see langword="amount"/> clients</returns>
    public static IEnumerable<Record> GenerateClients(int amount) => Enumerable.Range(0, amount)
        .Select(client => GenereteClient());

    /// <summary>
    /// Method for generating random client
    /// </summary>
    /// <returns>Random instance of <see cref="Record"/></returns>
    public static Record GenereteClient() => new Faker<Record>("en")
        .RuleFor(b => b.FullName, t => t.Name.FullName());

    /// <summary>
    /// Method for generating random Passports ID's
    /// </summary>
    /// <returns>Random Passport ID</returns>
    private static string GenerateRandomSeries() => _series[Random.Shared.Next(0, _series.Count())];
}
