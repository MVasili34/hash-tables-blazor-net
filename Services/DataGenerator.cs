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
    public static IEnumerable<Client> GenerateClients(int amount) => Enumerable.Range(0, amount)
        .Select(client => GenereteClient());

    /// <summary>
    /// Method for generating random client
    /// </summary>
    /// <returns>Random instance of <see cref="Client"/></returns>
    public static Client GenereteClient() => new Faker<Client>("en")
        .RuleFor(b => b.FullName, t => t.Name.FullName())
        .RuleFor(b => b.DateOfBirth, t => t.Date.BetweenDateOnly(
        DateOnly.FromDateTime(DateTime.Now.AddYears(-80).Date),
        DateOnly.FromDateTime(DateTime.Now.AddYears(-18).Date)))
        .RuleFor(b => b.Phone, t => t.Phone.PhoneNumberFormat())
        .RuleFor(b => b.Passport, GenerateRandomSeries() +  Random.Shared.Next(1_000_000, 9_999_999).ToString())
        .RuleFor(b => b.Address, t => t.Address.City())
        .RuleFor(b => b.BillNumber, Random.Shared.Next(100_000, 999_999))
        .RuleFor(b => b.Amount, Random.Shared.Next(100, 10_000))
        .Generate();

    /// <summary>
    /// Method for generating random Passports ID's
    /// </summary>
    /// <returns>Random Passport ID</returns>
    private static string GenerateRandomSeries() => _series[Random.Shared.Next(0, _series.Count())];
}
