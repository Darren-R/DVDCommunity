using System;

public class Program
{
    private static MemberCollection memberCollection = new MemberCollection(10);

    public static void Main(string[] args)
    {
        Movie movie1 = new Movie("Ghost in the Shell", Genre.SciFi, Classification.Mature, 82);
        Movie movie2 = new Movie("Ong-Bak: Muay Thai Warrior", Genre.Action, Classification.General, 108);
        Movie movie3 = new Movie("The Departed", Genre.Action, Classification.Restricted, 151);

        MovieCollection movieCollection = new MovieCollection();

        movieCollection.AddMovie(movie1);
        movieCollection.AddMovie(movie2);
        movieCollection.AddMovie(movie3);

        Member darren = new Member("Darren", "R", "2345");
        Member anna = new Member("Anna", "J", "56798");

        memberCollection.AddMember(darren);
        memberCollection.AddMember(anna);

        bool run = true;
        while (run)
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Register a new member");
            Console.WriteLine("2. Print existing members");
            Console.WriteLine("3. Add a new movie");
            Console.WriteLine("4. Borrow a movie");
            Console.WriteLine("5. Return a movie");
            Console.WriteLine("6. Exit");
            int option = int.Parse(Console.ReadLine());
            Console.WriteLine("");

            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter member's first name:");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Enter member's last name:");
                    string lastName = Console.ReadLine();
                    Console.WriteLine("Enter member's phone number:");
                    string phoneNumber = Console.ReadLine();

                    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phoneNumber))
                    {
                        Console.WriteLine("First name, last name, and phone number cannot be null.\n");
                    }
                    else if (!int.TryParse(phoneNumber, out _))
                    {
                        Console.WriteLine("Phone number must be a valid number.\n");
                    }
                    else
                    {
                        Member newMember = new Member(firstName, lastName, phoneNumber);
                        memberCollection.AddMember(newMember);
                        Console.WriteLine($"Member {firstName} {lastName} added successfully.");
                    }
                    break;

                case 2:
                    Console.Write("Current members: ");
                    if (memberCollection.MemberCount == 0)
                    {
                        Console.WriteLine("There are no current members");
                    }
                    else
                    {
                        for (int i = 0; i < memberCollection.MemberCount; i++)
                        {
                            Member currentMember = memberCollection.GetMember(i);
                            Console.WriteLine($"First Name: {currentMember.FirstName}, Last Name: {currentMember.LastName}, Phone Number: {currentMember.PhoneNumber}");
                        }
                    }
                    break;

                case 3:
                    Console.WriteLine("Enter movie title:");
                    string title = Console.ReadLine();

                    Genre genre = Genre.Other;
                    while (true)
                    {
                        Console.WriteLine("Enter movie genre:");
                        string genreInput = Console.ReadLine();
                        if (Enum.TryParse(genreInput, true, out Genre parsedGenre))
                        {
                            genre = parsedGenre;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid genre. Please enter a valid genre.");
                        }
                    }

                    Classification classification = Classification.General;
                    while (true)
                    {
                        Console.WriteLine("Enter movie classification:");
                        string classificationInput = Console.ReadLine();
                        if (Enum.TryParse(classificationInput, true, out Classification parsedClassification))
                        {
                            classification = parsedClassification;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid classification. Please enter a valid classification.");
                        }
                    }

                    Console.WriteLine("Enter movie duration in minutes:");
                    int duration = int.Parse(Console.ReadLine());

                    Movie newMovie = new Movie(title, genre, classification, duration);
                    movieCollection.AddMovie(newMovie);
                    if (movieCollection.AddMovie(newMovie))
                    {
                        Console.WriteLine($"Movie {title} added successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Movie {title} not added.");
                    }
                    break;

                case 4:
                    Console.WriteLine("Enter member's first name:");
                    firstName = Console.ReadLine();

                    Console.WriteLine("Enter member's last name:");
                    lastName = Console.ReadLine();

                    Member borrwingMember = memberCollection.FindMember(firstName, lastName);
                    if (borrwingMember == null)
                    {
                        Console.WriteLine("Member not found.\n");
                        break;
                    }

                    Console.WriteLine("Enter title of movie to borrow:");
                    string movieTitle = Console.ReadLine();

                    Movie movie = movieCollection.FindMovie(movieTitle);
                    if (movie == null)
                    {
                        Console.WriteLine("Movie not found.\n");
                        break;
                    }
                    if (borrwingMember.BorrowMovie(movieTitle))
                    {
                        Console.WriteLine($"{borrwingMember.FirstName} {borrwingMember.LastName} has borrowed {movieTitle}.\n");
                    }
                    else
                    {
                        Console.WriteLine($"{borrwingMember.FirstName} did not borrow {movieTitle}\n");
                    }
                    break;

                case 5:
                    Console.WriteLine("Enter member's first name:");
                    string returnFirstName = Console.ReadLine();

                    Console.WriteLine("Enter member's last name:");
                    string returnLastName = Console.ReadLine();

                    Member returningMember = memberCollection.FindMember(returnFirstName, returnLastName);
                    if (returningMember == null)
                    {
                        Console.WriteLine("Member not found.\n");
                        break;
                    }

                    Console.WriteLine("Enter title of movie to return:");
                    string returnMovieTitle = Console.ReadLine();

                    if (returningMember.ReturnMovie(returnMovieTitle))
                    {
                        Console.WriteLine($"{returningMember.FirstName} {returningMember.LastName} has returned {returnMovieTitle}.\n");
                    }
                    break;

                case 6:
                    run = false;
                    break;
            }
        }
    }
}