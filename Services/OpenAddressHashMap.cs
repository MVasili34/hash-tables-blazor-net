﻿using Services.Models;

namespace Services;

public class OpenAddressHashMap<TKey, TValue> : IOpenAddressHashMap<TKey, TValue> where TValue : Record
{
    public HashRow<TKey, TValue>?[] HashRows { get; }
    private const int _size = 60;
    private readonly double _constValue = (Math.Sqrt(5) - 1) / 2;
    private int _currentCount = 0;

    public OpenAddressHashMap()
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
        if (_currentCount >= _size)
        {
            return false;
        }
        int hash = HashFunction(key);
        if (HashRows[hash]?.Status == RowStatus.Occupied)
            CollisionCount++;
        while (HashRows[hash]?.Status == RowStatus.Occupied)
        {
            hash = (hash + 1) % _size;
        }

        HashRows[hash] = new() { 
            Key = key, 
            Value = item, 
            Status = RowStatus.Occupied
        };
        _currentCount++;
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
            hash = (hash + 1) % _size;
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
                _currentCount--;
                return true;
            }
            hash = (hash + 1) % _size;
        } while (hash != started);

        return false;
    }

    /// <summary>
    /// Multiplication Hash function
    /// </summary>
    /// <param name="value">Value</param>
    /// <returns>Hash result</returns>
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
}
