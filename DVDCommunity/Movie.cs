public enum Genre
{
    Family,
    Action,
    SciFi,
    Comedy,
    Animated,
    Thriller,
    Drama,
    Other
}

public enum Classification
{
    General,
    ParentalGuidance,
    Mature,
    Restricted
}

public class Movie
{
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public Classification Classification { get; set; }
    public int Duration { get; set; }
    public int Copies { get; set; }

    public Movie(string title, Genre genre, Classification classification, int duration, int copies = 1)
    {
        Title = title;
        Genre = genre;
        Classification = classification;
        Duration = duration;
        Copies = copies;
    }

    public void AddCopies(int copies)
    {
        Copies += copies;
    }
}
