public class MovieCollection
{
    private const int MAX_MOVIES = 1000;
    private HashTable<string, Movie> movieTable;

    public MovieCollection()
    {
        movieTable = new HashTable<string, Movie>(MAX_MOVIES);
    }

    public Movie? FindMovie(string title)
    {
        return movieTable.Search(title);
    }

    public bool AddCopiesToExistingMovie(string title, int copies)
    {
        Movie? movie = FindMovie(title);
        if (movie == null)
        {
            Console.WriteLine("Movie not found in the collection.");
            return false;
        }

        if (movieTable.Count + copies > MAX_MOVIES)
        {
            Console.WriteLine("Not enough space in the collection for the additional copies.");
            return false;
        }

        movie.AddCopies(copies);
        movieTable.Insert(title, movie);

        return true;
    }

    public void PrintMovies()
    {
        movieTable.Print();
    }

    public bool AddMovie(Movie movie)
    {
        if (FindMovie(movie.Title) != null)
        {
            return false;
        }

        if (movieTable.Count >= MAX_MOVIES)
        {
            Console.WriteLine("Movie collection is full. Cannot add more movies.");
            return false;
        }

        movieTable.Insert(movie.Title, movie);

        return true;
    }

    public bool RemoveMovie(string title)
    {
        Movie? movie = FindMovie(title);
        if (movie != null)
        {
            movieTable.Delete(title);
            Console.WriteLine($"The movie {title} has been removed from the collection.");
            return true;
        }
        else
        {
            Console.WriteLine($"The movie {title} was not found in the collection.");
            return false;
        }
    }

    public int Count()
    {
        return movieTable.Count;
    }
}