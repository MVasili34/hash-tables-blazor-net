namespace Services;

public class ListNode<TKey, TValue> where TValue : Client
{
    private TKey _key;
    private TValue _value;
    private ListNode<TKey, TValue>? _nextNode;

    public ListNode(TKey key, TValue value)
    {
        _key = key;
        _value = value;
        _nextNode = null;
    }

    public TKey Key => _key;

    public TValue Value => _value;

    /// <summary>
    /// Method for adding a new node to a linked list
    /// </summary>
    /// <param name="node">Instance of <see href="ListNode"/> type</param>
    public void GenerateNextNode(ListNode<TKey, TValue>? node) => _nextNode = node;

    public ListNode<TKey, TValue>? NextNode => _nextNode;
}
