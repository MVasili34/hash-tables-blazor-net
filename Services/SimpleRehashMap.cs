using Services.Models;

namespace Services;

public class SimpleRehashMap<TKey, TValue> : IOpenAddressHashMap<TKey, TValue> where TValue : Record
{
    private const int _size = 60;
    private readonly double _constValue = (Math.Sqrt(5) - 1) / 2;
    public HashRow<TKey, TValue>?[] HashRows { get; }

    public SimpleRehashMap()
    {
        HashRows = new HashRow<TKey, TValue>[_size];
        for (int i = 0; i < HashRows.Length; i++)
        {
            HashRows[i] = new();
        }
    }

    public int CollisionCount { get; private set; }

    public int LastSearchComparions { get; private set; }

    public bool Add(TKey key, TValue item)
    {
        int hash = HashFunction(key);

        while (HashRows[hash]?.Status == RowStatus.Occupied)
        {
            CollisionCount++;
            hash = Rehash(hash);
        }

        HashRows[hash] = new()
        {
            Key = key,
            Value = item,
            Status = RowStatus.Occupied
        };
        return true;
    }

    public bool TryGetValue(TKey key, out TValue? value)
    {
        int hash = HashFunction(key);
        LastSearchComparions = 0;
        if (hash == -1)
        {
            value = default;
            return false;
        }
        int started = hash;

        do
        {
            LastSearchComparions++;
            if (HashRows[hash]!.Status == RowStatus.Occupied &&
                HashRows[hash]!.Key!.Equals(key))
            {
                value = HashRows[hash]!.Value;
                return true;
            }
            hash = SimpleRehashMap<TKey, TValue>.Rehash(hash);
        } while (hash != started);

        value = default;
        return false;
    }

    public bool Remove(TKey key)
    {
        int hash = HashFunction(key);
        int started = hash;
        do
        {
            if (HashRows[hash]!.Status == RowStatus.Occupied &&
                HashRows[hash]!.Key!.Equals(key))
            {
                HashRows[hash]!.Status = RowStatus.Deleted;
                return true;
            }
            hash = SimpleRehashMap<TKey, TValue>.Rehash(hash);
        } while (hash != started);

        return false;
    }

    private int HashFunction(TKey key)
    {
        string symbols = key!.ToString()!;

        if (symbols.Length < 2)
        {
            return -1;
        }

        int hash = symbols[0] + symbols[1];

        double result = (hash * _constValue) % 1;
        result *= _size;
        hash = (int)Math.Floor(result);

        return hash;
    }

    private static int Rehash(int oldHash) => (oldHash + 1) % _size;
}
