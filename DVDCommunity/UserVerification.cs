public class UserVerification
{
    public static bool VerifyIdentity(string userType)
    {
        Console.WriteLine($"Please enter your {userType} username:");
        string username = Console.ReadLine();

        Console.WriteLine($"Please enter your {userType} password:");
        string password = Console.ReadLine();

        if (userType == "Staff" && username == "staff" && password == "today123")
        {
            return true;
        }
        return false;
    }
}
