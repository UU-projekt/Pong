using System.Collections;
using System.Collections;
using System.Collections.Generic;

public class GameObjectCollection<T> : IEnumerable<T> 
    where T : GameObject
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public void Remove(T item)
    {
        items.Remove(item);
    }

    public void Clear()
    {
        items.Clear();
    }

    public int Count => items.Count;

    public T this[int index] => items[index];

    public IEnumerator<T> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
