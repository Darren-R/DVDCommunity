public class Member
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public List<string> BorrowedMovies { get; set; }

    public Member(string firstName, string lastName, string phoneNumber, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Password = password;
        BorrowedMovies = new List<string>();
    }

    public bool BorrowMovie(string dvdTitle, MovieCollection movieCollection)
    {
        if (BorrowedMovies.Count >= 5)
        {
            Console.WriteLine("You are only allowed to borrow 5 Dvd's and you currently have " + BorrowedMovies.Count.ToString());
            return false;
        }
        else if (BorrowedMovies.Contains(dvdTitle))
        {
            Console.WriteLine("Already borrowing this Dvd");
            return false;
        }
        else
        {
            Movie? movie = movieCollection.FindMovie(dvdTitle);
            if (movie != null && movie.Copies > 0)
            {
                BorrowedMovies.Add(dvdTitle);
                movie.Copies--;
                return true;
            }
            else
            {
                Console.WriteLine("The movie is currently unavailable.");
                return false;
            }
        }
    }

    public bool ReturnMovie(string dvdTitle, MovieCollection movieCollection)
    {
        if (BorrowedMovies.Contains(dvdTitle))
        {
            BorrowedMovies.Remove(dvdTitle);
            Movie? movie = movieCollection.FindMovie(dvdTitle);
            if (movie != null)
            {
                movie.Copies++;
                return true;
            }
            else
            {
                Console.WriteLine("This movie doesn't exist in the collection. Please check the movie title.");
                return false;
            }
        }
        else
        {
            Console.WriteLine("You are not currently renting that movie");
            return false;
        }
    }
}