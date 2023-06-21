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

    public bool BorrowMovie(string dvdTitle)
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
            BorrowedMovies.Add(dvdTitle);
            return true;
            //For testing Console.WriteLine("You have succesfully borrowed " + dvdTitle);
        }
    }

    public bool ReturnMovie(string dvdTitle)
    {
        if (BorrowedMovies.Contains(dvdTitle))
        {
            BorrowedMovies.Remove(dvdTitle);
            return true;
        }
        else
        {
            Console.WriteLine("You are not currently renting that Dvd");
            return false;
        }
    }
}