using Services.Models;

namespace Services;

public class HashRow<TKey, TValue> where TValue : Record
{
    public TKey? Key { get; set; }
    public TValue? Value { get; set; }
    public RowStatus Status { get; set; }
    public HashRow() => Status = RowStatus.Free;
}
