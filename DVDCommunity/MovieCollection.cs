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

    public bool AddCopiesToExistingMovie(string title, int copies)
    {
        Movie movie = FindMovie(title);
        if (movie == null)
        {
            Console.WriteLine("Movie not found in the collection.");
            return false;
        }

        if (_count + copies > MAX_MOVIES)
        {
            Console.WriteLine("Not enough space in the collection for the additional copies.");
            return false;
        }

        movie.AddCopies(copies);
        _count += copies;

        return true;
    }


    public void PrintMovies()
    {
        foreach (LinkedList<KeyValue<string, Movie>> linkedList in _items)
        {
            if (linkedList != null)
            {
                foreach (KeyValue<string, Movie> item in linkedList)
                {
                    Console.WriteLine($"Title: {item.Key}, Copies: {item.Value.Copies}");
                }
            }
        }
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


    public bool Remove(string title, int copiesToRemove = 1)
    {
        int position = GetArrayPosition(title);
        LinkedList<KeyValue<string, Movie>> linkedList = GetLinkedList(position);
        bool itemFound = false;
        int removedCopies = 0;
        LinkedListNode<KeyValue<string, Movie>> currentNode = linkedList.First;

        while (currentNode != null && removedCopies < copiesToRemove)
        {
            if (currentNode.Value.Key.Equals(title))
            {
                itemFound = true;
                LinkedListNode<KeyValue<string, Movie>> nextNode = currentNode.Next;
                linkedList.Remove(currentNode);
                currentNode = nextNode;
                removedCopies++;
                _count--;
            }
            else
            {
                currentNode = currentNode.Next;
            }
        }

        if (itemFound)
        {
            if (removedCopies > 1)
            {
                Console.WriteLine($"{removedCopies} copies of {title} have been removed.");
            }
            else
            {
                Console.WriteLine($"{removedCopies} copy of {title} has been removed.");
            }
            return true;
        }
        else
        {
            Console.WriteLine($"The movie {title} was not found in the collection.");
            return false;
        }
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