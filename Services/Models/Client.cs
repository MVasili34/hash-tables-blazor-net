namespace Services;

public class Client
{
    public string FullName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Phone { get; set; } = null!;

    public string Passport { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int BillNumber { get; set; }

    public int Amount { get; set; }

    public override int GetHashCode()
    {
        int hash = 0;
        for (int i = 0; i < Passport.Length; i++)
            hash += Passport[i];
        double a = (Math.Sqrt(5) - 1) / 2;
        double result = (hash * a) % 1;
        result *= 60;
        hash = (int)Math.Floor(result);

        return hash;
    }

    public override string ToString()
    {
        return $"ФИО: {FullName}; ДР: {DateOfBirth}; Пасспорт: {Passport}; Номер счёта: {BillNumber};";
    }
}
