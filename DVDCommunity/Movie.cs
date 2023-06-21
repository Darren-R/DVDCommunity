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

    public Movie(string title, Genre genre, Classification classification, int duration)
    {
        Title = title;
        Genre = genre;
        Classification = classification;
        Duration = duration;
    }
}
