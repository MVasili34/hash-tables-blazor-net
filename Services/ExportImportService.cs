using CsvHelper;
using System.Globalization;

namespace Services;

public class ExportImportService<T> where T: Record
{
    public string PathToFile { get; init; } = null!;
    public string FileName { get; init; } = null!;

    public ExportImportService(string pathToFile, string fileName)
    {
        PathToFile = pathToFile;
        FileName = fileName;
    }

    /// <summary>
    /// Export to CSV file method
    /// </summary>
    /// <param name="clients"><see cref="IEnumerable{T}"/> collrction</param>
    public void ExportPersons(IEnumerable<T> clients)
    {
        DirectoryInfo directory = new(PathToFile);
        if (!directory.Exists)
        {
            directory.Create();
        }

        using FileStream fileStream = new(Path.Combine(PathToFile, FileName), FileMode.Create);
        using StreamWriter streamWriter = new(fileStream);
        using CsvWriter writer = new(streamWriter, CultureInfo.InvariantCulture);

        writer.WriteHeader<T>();
        writer.NextRecord();
        writer.WriteRecords(clients);
        writer.NextRecord();
        writer.Flush();
    }

    /// <summary>
    /// Import from CSV file method
    /// </summary>
    /// <returns><see cref="IEnumerable{T}"/> collection from CSV file</returns>
    public IEnumerable<T> Import()
    {
        using FileStream fileStream = new(Path.Combine(PathToFile, FileName), FileMode.Open);
        using StreamReader streamReader = new(fileStream);
        using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        return reader.GetRecords<T>().ToList();
    }
}