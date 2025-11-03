using System.Collections;

// KRAV #5:
// 1: Enumerable & Enumerator
// 2: klassen implementerar IEnumerable<T> vilket gör den itererabar. Genom att anropa GetEnumerator() på den underliggande
//    listan kan alla GameObjects i samlingen användas direkt i looper uten att skapa egen enumerator.
// 3: Vi använder IEnumerable<T> för att kunna skapa en egen samling av GameObjects vilket ger oss ett säkert sätt
//    att hantera flera GameObjects med möjlighet att avgränsa mer tydligt genom Generics. (tex en Collection med bara paddles osv)
//    samtidigt som vi kan expandera klassen med logik som är specifik för GameObjects utan att ändra hur den används
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
