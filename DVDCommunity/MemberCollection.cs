public class MemberCollection
{
    private Member[] members;
    private int memberCount;

    public MemberCollection(int size)
    {
        members = new Member[size];
        memberCount = 0;
    }

    public Member GetMember(int index)
    {
        if (index < 0 || index >= memberCount)
        {
            throw new IndexOutOfRangeException("Index out of range.");
        }
        return members[index];
    }
    public int MemberCount
    {
        get { return memberCount; }
    }

    public bool AddMember(Member member)
    {
        if (memberCount >= members.Length)
        {
            return false;
        }
        else
        {
            members[memberCount] = member;
            memberCount++;
            return true;
        }
    }

    public bool RemoveMember(Member member)
    {
        for (int i = 0; i < memberCount; i++)
        {
            if (members[i] == member)
            {
                for (int j = i; j < memberCount - 1; j++)
                {
                    members[j] = members[j + 1];
                }
                members[memberCount - 1] = null;
                memberCount--;
                return true;
            }
        }
        return false;
    }

    public Member FindMember(string firstName, string lastName)
    {
        for (int i = 0; i < memberCount; i++)
        {
            if (members[i].FirstName == firstName && members[i].LastName == lastName)
            {
                return members[i];
            }
        }
        return null;
    }

    public List<Member> FindMembersWithMovie(string movieTitle)
    {
        List<Member> membersWithMovie = new List<Member>();

        foreach (Member member in members)
        {
            if (member != null && member.BorrowedMovies != null && member.BorrowedMovies.Contains(movieTitle))
            {
                membersWithMovie.Add(member);
            }
        }

        return membersWithMovie;
    }


}
