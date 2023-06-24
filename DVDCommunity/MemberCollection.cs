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

    public Dictionary<string, int> GetTopMovies(int topCount)
    {
        Dictionary<string, int> movieCount = new Dictionary<string, int>();

        foreach (Member member in members)
        {
            if (member != null && member.BorrowedMovies != null)
            {
                foreach (string movie in member.BorrowedMovies)
                {
                    if (movieCount.ContainsKey(movie))
                    {
                        movieCount[movie]++;
                    }
                    else if (movieCount.Count < topCount)
                    {
                        movieCount.Add(movie, 1);
                    }
                    else
                    {
                        string minMovie = null;
                        int minCount = int.MaxValue;

                        foreach (var pair in movieCount)
                        {
                            if (pair.Value < minCount)
                            {
                                minMovie = pair.Key;
                                minCount = pair.Value;
                            }
                        }

                        movieCount[minMovie]--;
                        if (movieCount[minMovie] == 0)
                        {
                            movieCount.Remove(minMovie);
                        }
                    }
                }
            }
        }

        var sortedMovieCount = new Dictionary<string, int>();
        foreach (var pair in movieCount.OrderByDescending(m => m.Value))
        {
            sortedMovieCount.Add(pair.Key, pair.Value);
        }
        return sortedMovieCount;
    }
}