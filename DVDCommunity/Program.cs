public class Program
{
    private static MemberCollection memberCollection = new MemberCollection(10);

    public static void Main(string[] args)
    {
        Movie movie1 = new Movie("Ghost in the Shell", Genre.SciFi, Classification.Mature, 82, 5);
        Movie movie2 = new Movie("Ong-Bak: Muay Thai Warrior", Genre.Action, Classification.General, 108, 4);
        Movie movie3 = new Movie("The Departed", Genre.Action, Classification.Restricted, 151, 3);
        Movie movie4 = new Movie("The Matrix", Genre.Action, Classification.Mature, 136);

        MovieCollection movieCollection = new MovieCollection();

        movieCollection.AddMovie(movie1);
        movieCollection.AddMovie(movie2);
        movieCollection.AddMovie(movie3);
        movieCollection.AddMovie(movie4);

        Member darren = new Member("Darren", "R", "2345", "0000");
        Member anna = new Member("Anna", "J", "56798", "0000");

        memberCollection.AddMember(darren);
        memberCollection.AddMember(anna);

        bool run = true;
        while (run)
        {

            Console.WriteLine("===========================================");
            Console.WriteLine("COMMUNITY LIBRARY AND DVD MANAGEMENT SYSTEM");
            Console.WriteLine("===========================================\n");

            Console.WriteLine("Main Menu");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Select from the following:\n");

            Console.WriteLine("1. Staff");
            Console.WriteLine("2. Member");
            Console.WriteLine("0. Exit");

            Console.Write("Enter your choice ==> ");
            string input = Console.ReadLine();
            int option;

            if (int.TryParse(input, out option))
            {
                switch (option)
                {
                    case 1:
                        if (UserVerification.VerifyIdentity("Staff"))
                        {
                            StaffMenu(movieCollection);
                        }
                        else
                        {
                            Console.WriteLine("Invalid staff credentials.");
                        }
                        break;
                    case 2:
                        Console.WriteLine("You Selected member");
                        MemberMenu(movieCollection);
                        break;
                    case 0:
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please enter 1 for Staff, 2 for Member or 0 to Exit.");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Invalid input. {input} is not a valid integer. Please enter 1 for Staff, 2 for Member or 0 to Exit.");
            }

        }

        static void StaffMenu(MovieCollection movieCollection)
        {
            while (true)
            {
                Console.WriteLine("\nStaff Menu:");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1. Add DVD's of a new movie to the system");
                Console.WriteLine("2. Add DVD's of an existing movie to the system");
                Console.WriteLine("3. Remove a DVD from the system");
                Console.WriteLine("4. Register a new member to the system");
                Console.WriteLine("5. Remove a registered member from the system");
                Console.WriteLine("6. Find a member's contact phone number given the member's name");
                Console.WriteLine("7. Find member's who are currently renting a particular movie");
                Console.WriteLine("0. Return to Main Menu");

                Console.Write("\nEnter your choice ==> ");
                string input = Console.ReadLine();

                int option;
                if (int.TryParse(input, out option))
                {
                    switch (option)
                    {
                        case 1:
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

                            Console.WriteLine("Enter the number of DVD's to be added");
                            int copies = int.Parse(Console.ReadLine());

                            Movie newMovie = new Movie(title, genre, classification, duration, copies);
                            if (movieCollection.AddMovie(newMovie))
                            {
                                Console.WriteLine($"Movie {title} added successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"Movie {title} not added.");
                            }
                            break;

                        case 2:
                            Console.WriteLine("Existing: ");
                            movieCollection.PrintMovies();

                            Console.WriteLine("Enter movie title:");
                            string existingTitle = Console.ReadLine();

                            Console.WriteLine("Enter number of copies to add:");
                            int newCopies = int.Parse(Console.ReadLine());

                            if (movieCollection.AddCopiesToExistingMovie(existingTitle, newCopies))
                            {
                                Console.WriteLine($"Copies of movie {existingTitle} added successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"Failed to add copies of movie {existingTitle}.");
                            }
                            break;

                        case 3:
                            Console.WriteLine("Existing: ");
                            movieCollection.PrintMovies();

                            Console.WriteLine("Enter movie title: ");
                            string titleToRemove = Console.ReadLine();
                            /*
                            Console.WriteLine("Enter number of copies to remove");
                            int copiesToRemove = int.Parse(Console.ReadLine());*/
                            movieCollection.RemoveMovie(titleToRemove);

                            break;

                        case 4:
                            Console.WriteLine("Enter member's first name:");
                            string addingFirstName = Console.ReadLine();
                            Console.WriteLine("Enter member's last name:");
                            string addingLastName = Console.ReadLine();
                            Console.WriteLine("Enter member's phone number:");
                            string phoneNumber = Console.ReadLine();
                            Console.WriteLine("Enter a four-digit password for the member:");
                            string password = Console.ReadLine();

                            if (string.IsNullOrEmpty(addingFirstName) || string.IsNullOrEmpty(addingLastName) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(password))
                            {
                                Console.WriteLine("First name, last name, phone number and password cannot be null.\n");
                            }
                            else if (!int.TryParse(phoneNumber, out _) || (password.Length != 4) || !int.TryParse(password, out _))
                            {
                                Console.WriteLine("Phone number and password must be valid numbers. The password must be exactly four digits.\n");
                            }
                            else
                            {
                                Member newMember = new Member(addingFirstName, addingLastName, phoneNumber, password);
                                memberCollection.AddMember(newMember);
                                Console.WriteLine($"Member {addingFirstName} {addingLastName} added successfully.");
                            }

                            break;

                        case 5:
                            Console.WriteLine("Enter member's first name:");
                            string removingFirstName = Console.ReadLine();
                            Console.WriteLine("Enter member's last name:");
                            string removingLastName = Console.ReadLine();

                            Member memberToRemove = memberCollection.FindMember(removingFirstName, removingLastName);

                            if (memberToRemove != null)
                            {
                                if (memberCollection.RemoveMember(memberToRemove))
                                {
                                    Console.WriteLine($"Member {removingFirstName} {removingLastName} removed successfully.");
                                }
                                else
                                {
                                    Console.WriteLine($"Error removing member {removingFirstName} {removingLastName}.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Member {removingFirstName} {removingLastName} not found.");
                            }

                            break;

                        case 6:
                            Console.WriteLine("Enter member's first name:");
                            string firstName = Console.ReadLine();
                            Console.WriteLine("Enter member's last name:");
                            string lastName = Console.ReadLine();

                            Member memberToFind = memberCollection.FindMember(firstName, lastName);
                            if (memberToFind != null)
                            {
                                Console.WriteLine($"Member {firstName} {lastName}'s phone number: {memberToFind.PhoneNumber}");
                            }
                            else
                            {
                                Console.WriteLine($"Member {firstName} {lastName} not found.");
                            }

                            break;

                        case 7:
                            Console.WriteLine("Enter the title of the movie:");
                            string movieTitle = Console.ReadLine();

                            List<Member> membersWithMovie = memberCollection.FindMembersWithMovie(movieTitle);

                            if (membersWithMovie.Count > 0)
                            {
                                Console.WriteLine($"Members currently renting {movieTitle}:");
                                foreach (Member member in membersWithMovie)
                                {
                                    Console.WriteLine($"{member.FirstName} {member.LastName}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"No members are currently renting {movieTitle}.");
                            }

                            break;

                        case 0:
                            Console.WriteLine("Returning to Main Menu...");
                            return;
                        default:
                            Console.WriteLine("Invalid selection. Please input an option from task list or 0 to Return to Main Menu.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid input. {input} is not a valid. Please input an option from task list or 0 to Return to Main Menu.");
                }
            }
        }
        static void MemberMenu(MovieCollection movieCollection)
        {
            while (true)
            {
                Console.WriteLine("\nMember Menu");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1. Browse all movies");
                Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
                Console.WriteLine("3. Borrow a movie DVD");
                Console.WriteLine("4. Return a borrowed DVD");
                Console.WriteLine("5. List current borrowing movies");
                Console.WriteLine("6. Display the top 3 movies rented by the members");
                Console.WriteLine("0. Return to main menu");

                Console.Write("\nEnter your choice ==> ");
                string input = Console.ReadLine();

                int option;
                if (int.TryParse(input, out option))
                {
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("\nPlease see the existing movies and number of DVD's in stock:");
                            movieCollection.PrintMovies();
                            break;

                        case 2:
                            Console.WriteLine("Enter title of movie to view:");
                            string movieInfoTitle = Console.ReadLine();
                            Movie movieInfo = movieCollection.FindMovie(movieInfoTitle);
                            if (movieInfo == null)
                            {
                                Console.WriteLine("Movie not found.\n");
                                break;
                            }
                            else
                            {
                                Console.Write("\n");
                                Console.WriteLine($"Title: {movieInfo.Title}");
                                Console.WriteLine($"Genre: {movieInfo.Genre}");
                                Console.WriteLine($"Classification: {movieInfo.Classification}");
                                Console.WriteLine($"Running Time: {movieInfo.Duration}");
                                Console.WriteLine($"Available Copies: {movieInfo.Copies}");
                                break;
                            }

                        case 3:
                            Console.WriteLine("Enter member's first name:");
                            string firstName = Console.ReadLine();

                            Console.WriteLine("Enter member's last name:");
                            string lastName = Console.ReadLine();

                            Member borrwingMember = memberCollection.FindMember(firstName, lastName);
                            if (borrwingMember == null)
                            {
                                Console.WriteLine("Member not found.\n");
                                break;
                            }
                            else
                            {
                                Console.Write("Enter your 4 digit pincode: ");
                                string pin = Console.ReadLine();
                                if (pin == borrwingMember.Password)
                                {
                                    Console.WriteLine("Enter title of movie to borrow:");
                                    string movieBorrowTitle = Console.ReadLine();

                                    Movie movie = movieCollection.FindMovie(movieBorrowTitle);
                                    if (movie == null)
                                    {
                                        Console.WriteLine("Movie not found.\n");
                                        break;
                                    }
                                    if (borrwingMember.BorrowMovie(movieBorrowTitle, movieCollection))
                                    {
                                        Console.WriteLine($"{borrwingMember.FirstName} {borrwingMember.LastName} has borrowed {movieBorrowTitle}.\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{borrwingMember.FirstName} did not borrow {movieBorrowTitle}\n");
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect PIN");
                                    break;
                                }
                            }

                        case 4:
                            Console.WriteLine("Enter first name:");
                            string returnFirstName = Console.ReadLine();

                            Console.WriteLine("Enter last name:");
                            string returnLastName = Console.ReadLine();

                            Member returningMember = memberCollection.FindMember(returnFirstName, returnLastName);
                            if (returningMember == null)
                            {
                                Console.WriteLine("Member not found.\n");
                                break;
                            }
                            Console.WriteLine("Enter title of movie to return:");
                            string returnMovieTitle = Console.ReadLine();

                            if (returningMember.ReturnMovie(returnMovieTitle, movieCollection))
                            {
                                Console.WriteLine($"{returningMember.FirstName} {returningMember.LastName} has returned {returnMovieTitle}.\n");
                            }
                            break;

                        case 5:
                            Console.WriteLine("Enter first name:");
                            string currentBorrowFirstName = Console.ReadLine();

                            Console.WriteLine("Enter last name:");
                            string currentBorrowLastName = Console.ReadLine();

                            Member currentBorrowMember = memberCollection.FindMember(currentBorrowFirstName, currentBorrowLastName);
                            if (currentBorrowMember == null)
                            {
                                Console.WriteLine("Member not found.\n");
                                break;
                            }
                            else
                            {
                                Console.Write("Enter your 4 digit pincode: ");
                                string pin = Console.ReadLine();
                                if (pin == currentBorrowMember.Password)
                                {
                                    if (currentBorrowMember.BorrowedMovies.Count == 0)
                                    {
                                        Console.WriteLine("No borrowed movies.");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Borrowed Movies:");

                                        foreach (string movieTitle in currentBorrowMember.BorrowedMovies)
                                        {
                                            Console.WriteLine(movieTitle);
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect PIN");
                                    break;
                                }
                            }

                        case 6:
                            var topMovies = memberCollection.GetTopMovies(3);
                            if (topMovies.Count == 0)
                            {
                                Console.WriteLine("No movies are currently rented.");
                                break;
                            }
                            else
                            {
                                foreach (var movie in topMovies)
                                {
                                    Console.WriteLine($"Movie: {movie.Key}, Rented: {movie.Value} times");
                                }
                                break;
                            }

                        case 0:
                            Console.WriteLine("Returning to Main Menu...");
                            return;
                        default:
                            Console.WriteLine("Invalid selection. Please input an option from task list or 0 to Return to Main Menu.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid input. {input} is not a valid. Please input an option from task list or 0 to Return to Main Menu.");
                }
            }
        }
    }
}