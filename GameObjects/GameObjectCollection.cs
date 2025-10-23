using System.Collections;

public class GameObjectCollection<T> : IEnumerable<T>
{
    private Dictionary<string, T> _objects = new();

    public void AddItem(string ItemId, T Item)
    {
        _objects.Add(ItemId, Item);
    }

    public T? GetItem(string ItemId)
    {
        return _objects[ItemId];
    }

    public Y? GetItemAs<Y>(string ItemId) where Y : T
    {
        var itemAsGameObject = GetItem(ItemId);
        if (itemAsGameObject is not Y) throw new InvalidCastException();

        return (Y)itemAsGameObject;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _objects.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}