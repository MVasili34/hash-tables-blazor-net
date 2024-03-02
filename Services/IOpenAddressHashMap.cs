namespace Services;

public interface IOpenAddressHashMap<TKey, TValue> where TValue : Record
{
    HashRow<TKey, TValue>?[] HashRows { get; }

    /// <summary>
    /// Collision counter
    /// </summary>
    int CollisionCount { get; }

    /// <summary>
    /// Last search comparisons counter
    /// </summary>
    int LastSearchComparions { get; }

    /// <summary>
    /// Method for adding values to OA Hash Table of type <see langword="TKey - TValue"/>
    /// </summary>
    /// <param name="key">Key of <see langword="TKey"/> type</param>
    /// <param name="item">Value of <see langword="TValue"/> type</param>
    /// <returns><see langword="true"/>, if successfully added, else <see langword="false"/></returns>
    bool Add(TKey key, TValue item);

    /// <summary>
    /// Method for getting value by key from an OA Hash Table
    /// </summary>
    /// <param name="key">Key of <see langword="TKey"/> type</param>
    /// <param name="value">Value of <see langword="TValue"/>, if found,
    /// else <see langword="default"/></param>
    /// <returns><see langword="true"/>, if successfully found, else <see langword="false"/></returns>
    bool TryGetValue(TKey key, out TValue? value);

    /// <summary>
    /// Method for removing a value by key from an OA Hash Table
    /// </summary>
    /// <param name="key">Кey of <see langword="TKey"/> type</param>
    /// <returns><see langword="true"/>, if successfully removed, else <see langword="false"/></returns>
    bool Remove(TKey key);
}
