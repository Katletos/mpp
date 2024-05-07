using System.Collections;

namespace tree;

public class MyList<T> : IList<T>
{
    private T[] _items;
    private int _lastIndex = 0;

    public MyList(int capacity)
    {
        _items = new T[capacity];
    }

    public void Add(T item)
    {
        if (Count < _items.Length)
        {
            _lastIndex += 1;
            _items[_lastIndex] = item;
        }
        else
        {
            Array.Resize(ref _items, _items.Length * 2);
            _lastIndex += 1;
            _items[_lastIndex] = item;
        }
    }

    public void Clear()
    {
        Array.Clear(_items);
    }

    public bool Contains(T item)
    {
        return _items.Length != 0 && IndexOf(item) >= 0;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);
        if (index < 0) return false;

        RemoveAt(index);
        return true;
    }

    public int Count => _lastIndex + 1;

    public bool IsReadOnly => false;

    public int IndexOf(T item)
    {
        return Array.IndexOf(_items, item, 0, _lastIndex);
    }

    public void Insert(int index, T item)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _lastIndex);
        Array.Copy(_items, index, _items, index + 1, _lastIndex - index);
    }

    public void RemoveAt(int index)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _lastIndex);
        _lastIndex -= 1;
        Array.Copy(_items, index + 1, _items, index, _lastIndex - index);
    }

    public T this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _lastIndex);
            return _items[index];
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _lastIndex);
            _items[index] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new MyListEnumerator<T>(_items);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class MyListEnumerator<T> : IEnumerator<T>
{
    private T[] _items;
    private int _position = -1;

    public MyListEnumerator(T[] myList)
    {
        _items = myList;
    }

    public bool MoveNext()
    {
        _position += 1;
        return _position < _items.Length;
    }

    public void Reset()
    {
        _position = -1;
    }

    public T Current
    {
        get
        {
            try
            {
                return _items[_position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}