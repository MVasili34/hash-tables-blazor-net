using Bogus.Bson;
using Services.Models;

namespace Services;

public class ChainHashMap<TKey, TValue> : IChainHashMap<TKey, TValue> where TValue : Client
{
    private readonly ListNode<TKey, TValue>?[] _hashTable;
    private int _currentAmount = 0;
    private const int _size = 60;
    private readonly double _constValue = (Math.Sqrt(5) - 1) / 2;
    public int CollisionCount { get; private set; }

    public ChainHashMap() => _hashTable = new ListNode<TKey, TValue>[_size];

    public bool Add(TKey key, TValue value)
    {
        int hash = HashFunction(key);
        if (_hashTable[hash] is null)
        {
            _currentAmount ++;
            _hashTable[hash] = new ListNode<TKey, TValue>(key, value);
            return true;
        }

        if (_hashTable[hash] is not null)
        {
            ListNode<TKey, TValue>? current = _hashTable[hash];
            CollisionCount++;
            while (current is not null)
            {
                if (current.Key!.Equals(key))
                    return false;
                if (current.NextNode is null)
                {
                    current.GenerateNextNode(new ListNode<TKey, TValue>(key, value));
                    break;
                }

                current = current.NextNode;
            }
        }
        return true;
    }

    public bool TryGetValue(TKey key, out TValue? value)
    {
        int hash = HashFunction(key);
        if (_hashTable[hash] is not null)
        {
            ListNode<TKey, TValue>? current = _hashTable[hash];
            while (current is not null)
            {
                if(current.Key!.Equals(key))
                {
                    value = current.Value;
                    return true;
                }
                current = current.NextNode;
            }
        }    

        value = default;
        return false;
    }

    public bool Remove(TKey key)
    {
        int hash = HashFunction(key);
        if (_hashTable[hash] is not null)
        {
            if (_hashTable[hash]!.Key!.Equals(key))
            {
                _hashTable[hash] = _hashTable[hash]!.NextNode;
                return true;
            }

            ListNode<TKey, TValue>? current = _hashTable[hash];
            while (current!.NextNode is not null)
            {
                if (current.NextNode.Key!.Equals(key))
                {
                    current.GenerateNextNode(current.NextNode.NextNode);
                    return true;
                }
                current = current.NextNode;
            }
        }

        return false;
    }

    public Dictionary<int, IEnumerable<TValue>> OutputValues()
    {
        Dictionary<int, IEnumerable<TValue>> result = new();
        for (int i = 0; i < _size; i++)
        {
            if (_hashTable[i] is not null)
            {
                List<TValue> clients = new();
                ListNode<TKey, TValue>? current = _hashTable[i];
                while (current is not null)
                {
                    clients.Add(current.Value);
                    current = current.NextNode;
                }
                result.Add(i, clients);
            }
        }
        return result;
    }

    /// <summary>
    /// Multiplication Hash function
    /// </summary>
    /// <param name="value">Value</param>
    /// <returns>Hash result</returns>
    private int HashFunction(TKey value)
    {
        int hash = 0;
        string symbols = value!.ToString()!;
        for (int i = 0; i < symbols.Length; i++)
            hash += symbols[i];

        double result = (hash * _constValue) % 1;
        result *= _size;
        hash = (int)Math.Floor(result);

        return hash;
    }

    /// <summary>
    /// Method for retrieving Diagram data 
    /// </summary>
    /// <returns><see cref="IEnumerable{DataItem}"/> collection for Quality Diagram</returns>
    public DataItem[] DiagramData()
    {
        DataItem[] result = new DataItem[_currentAmount];
        int index = 0;
        for(int i = 0; i < _hashTable.Length; i++)
        {
            if (_hashTable[i] is not null)
            {
                int helper = 0;
                var current = _hashTable[i];
                while (current is not null) 
                {
                    helper++;
                    current = current.NextNode;
                }
                result[index] = new() { HashValue = i, TotalAmounts = helper };
                index++;
            }
        }
        return result;
    }
}
