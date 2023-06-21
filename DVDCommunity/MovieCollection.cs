public class MovieCollection
{
    private const int MAX_MOVIES = 1000;
    private readonly int _size;
    private readonly LinkedList<KeyValue<string, Movie>>[] _items;
    private int _count = 0;

    public MovieCollection()
    {
        _size = MAX_MOVIES;
        _items = new LinkedList<KeyValue<string, Movie>>[_size];
    }

    protected int GetArrayPosition(string key)
    {
        int position = key.GetHashCode() % _size;
        return Math.Abs(position);
    }

    public Movie FindMovie(string title)
    {
        int position = GetArrayPosition(title);
        LinkedList<KeyValue<string, Movie>> linkedList = GetLinkedList(position);
        foreach (KeyValue<string, Movie> item in linkedList)
        {
            if (item.Key.Equals(title))
            {
                return item.Value;
            }
        }

        return null;
    }

    public bool AddMovie(Movie movie)
    {
        if (FindMovie(movie.Title) != null)
        {
            return false;
        }

        if (_count >= MAX_MOVIES)
        {
            Console.WriteLine("Movie collection is full. Cannot add more movies.");
            return false;
        }

        int position = GetArrayPosition(movie.Title);
        LinkedList<KeyValue<string, Movie>> linkedList = GetLinkedList(position);
        linkedList.AddLast(new KeyValue<string, Movie>() { Key = movie.Title, Value = movie });
        _count++;

        return true;
    }

    public bool RemoveMovie(string title)
    {
        int position = GetArrayPosition(title);
        LinkedList<KeyValue<string, Movie>> linkedList = GetLinkedList(position);
        bool itemFound = false;
        KeyValue<string, Movie> foundItem = default(KeyValue<string, Movie>);
        foreach (KeyValue<string, Movie> item in linkedList)
        {
            if (item.Key.Equals(title))
            {
                itemFound = true;
                foundItem = item;
            }
        }

        if (itemFound)
        {
            linkedList.Remove(foundItem);
            _count--;
            return true;
        }

        return false;
    }

    protected LinkedList<KeyValue<string, Movie>> GetLinkedList(int position)
    {
        LinkedList<KeyValue<string, Movie>> linkedList = _items[position];
        if (linkedList == null)
        {
            linkedList = new LinkedList<KeyValue<string, Movie>>();
            _items[position] = linkedList;
        }

        return linkedList;
    }

    public int Count()
    {
        return _count;
    }
}

public struct KeyValue<K, V>
{
    public K Key { get; set; }
    public V Value { get; set; }
}
