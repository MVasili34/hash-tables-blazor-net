namespace Services;

public interface IChainHashMap<TKey, TValue> where TValue : Client 
{
    /// <summary>
    /// Collision counter
    /// </summary>
    int CollisionCount { get; }

    /// <summary>
    /// Method for adding values to Chained Hash Table of type <see langword="TKey - TValue"/>
    /// </summary>
    /// <param name="key">Key of <see langword="TKey"/> type</param>
    /// <param name="item">Value of <see langword="TValue"/> type</param>
    /// <returns><see langword="true"/>, if successfully added, else <see langword="false"/></returns>
    bool Add(TKey key, TValue item);

    /// <summary>
    /// Method for getting value by key from a Chained Hash Table
    /// </summary>
    /// <param name="key">Key of <see langword="TKey"/> type</param>
    /// <param name="value">Value of <see langword="TValue"/>, if found,
    /// else <see langword="default"/></param>
    /// <returns><see langword="true"/>, if successfully found, else <see langword="false"/></returns>
    bool TryGetValue(TKey key, out TValue? value);

    /// <summary>
    /// Method for removing a value by key from a Chained Hash Table
    /// </summary>
    /// <param name="key">Кey of <see langword="TKey"/> type</param>
    /// <returns><see langword="true"/>, if successfully removed, else <see langword="false"/></returns>
    bool Remove(TKey key);

    /// <summary>
    /// Method of retrieving all pairs <see langword="TKey - TValue"/> of current Chained Hash Table
    /// </summary>
    /// <returns>Dictionary with all pairs</returns>
    Dictionary<int, IEnumerable<TValue>> OutputValues();
}
