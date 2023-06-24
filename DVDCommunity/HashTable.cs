public class HashTable<TKey, TValue> where TValue : class
{
    protected struct Entry
    {
        public TKey Key;
        public TValue Value;
    }

    protected Entry?[] hashTable;

    public HashTable(int capacity)
    {
        this.Capacity = capacity;
        this.hashTable = new Entry?[capacity];
    }

    public int Count { get; protected set; }

    public int Capacity { get; protected set; }

    public TValue Search(TKey key)
    {
        int hash = Math.Abs(key.GetHashCode()) % Capacity;
        for (int i = 0; i < Capacity; i++)
        {
            int probe = (hash + i) % Capacity;
            if (hashTable[probe]?.Key.Equals(key) == true)
                return hashTable[probe]?.Value;
            else if (hashTable[probe] == null)
                return null;
        }
        return null;
    }

    public void Delete(TKey key)
    {
        int hash = Math.Abs(key.GetHashCode()) % Capacity;
        for (int i = 0; i < Capacity; i++)
        {
            int probe = (hash + i) % Capacity;
            if (hashTable[probe]?.Key.Equals(key) == true)
            {
                hashTable[probe] = null;
                Count--;
                return;
            }
        }
        throw new KeyNotFoundException($"The given key '{key}' was not present in the hash table.");
    }

    public void Clear()
    {
        this.hashTable = new Entry?[Capacity];
        this.Count = 0;
    }

    public void Insert(TKey key, TValue value)
    {
        if (this.Count >= this.Capacity)
            throw new InvalidOperationException("Hash table is full");

        int hash = Math.Abs(key.GetHashCode()) % this.Capacity;
        for (int i = 0; i < this.Capacity; i++)
        {
            int probe = (hash + i) % this.Capacity;
            if (hashTable[probe] == null)
            {
                hashTable[probe] = new Entry { Key = key, Value = value };
                this.Count++;
                return;
            }
        }
    }

    public void Print()
    {
        for (int i = 0; i < this.Capacity; i++)
        {
            if (hashTable[i] != null)
            {
                Movie movie = hashTable[i].Value.Value as Movie;
                if (movie != null)
                {
                    Console.WriteLine($"Title: {movie.Title}, Copies: {movie.Copies}");
                }
            }
        }
    }

}