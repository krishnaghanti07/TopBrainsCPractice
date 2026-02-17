// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.Linq;

public class Team : IComparable<Team>
{
    public string Name { get; set; }
    public int Points { get; set; }
    
    public int CompareTo(Team other)
    {
        // Compare by points descending, then by name
        int pointCompare = other.Points.CompareTo(this.Points);
        if (pointCompare == 0)
            return this.Name.CompareTo(other.Name);

        return pointCompare;
    }
}

public class Match
{
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }

    public Match(Team t1, Team t2)
    {
        Team1 = t1;
        Team2 = t2;
    }

    public Match Clone()
    {
        return new Match(Team1, Team2)
        {
            Team1Score = this.Team1Score,
            Team2Score = this.Team2Score
        };
    }
}

public class Tournament
{
    private SortedList<int, Team> _rankings = new SortedList<int, Team>();
    private LinkedList<Match> _schedule = new LinkedList<Match>();
    private Stack<Match> _undoStack = new Stack<Match>();
    private List<Team> _teams = new List<Team>();

    // Add match to schedule
    public void ScheduleMatch(Match match)
    {
        // Add to linked list
        _schedule.AddLast(match);

        if (!_teams.Contains(match.Team1))
            _teams.Add(match.Team1);

        if (!_teams.Contains(match.Team2))
            _teams.Add(match.Team2);

        RefreshRankings();
    }

    // Record match result and update rankings
    public void RecordMatchResult(Match match, int team1Score, int team2Score)
    {
        _undoStack.Push(match.Clone());

        match.Team1Score = team1Score;
        match.Team2Score = team2Score;

        // Simple scoring: Win = 3, Draw = 1, Loss = 0
        if (team1Score > team2Score)
            match.Team1.Points += 3;
        else if (team1Score < team2Score)
            match.Team2.Points += 3;
        else
        {
            match.Team1.Points += 1;
            match.Team2.Points += 1;
        }

        RefreshRankings();
    }

    // Undo last match
    public void UndoLastMatch()
    {
        if (_undoStack.Count == 0)
            return;

        Match last = _undoStack.Pop();

        // Revert points
        if (last.Team1Score > last.Team2Score)
            last.Team1.Points -= 3;
        else if (last.Team1Score < last.Team2Score)
            last.Team2.Points -= 3;
        else
        {
            last.Team1.Points -= 1;
            last.Team2.Points -= 1;
        }

        RefreshRankings();
    }

    // Get ranking position using binary search
    public int GetTeamRanking(Team team)
    {
        var sortedTeams = _teams.OrderBy(t => t).ToList();
        return sortedTeams.BinarySearch(team);
    }

    private void RefreshRankings()
    {
        _rankings.Clear();

        var sorted = _teams.OrderBy(t => t).ToList();
        int index = 0;

        foreach (var team in sorted)
        {
            _rankings.Add(index++, team);
        }
    }

    public List<Team> GetRankings()
    {
        return _rankings.Values.ToList();
    }
}

class Program
{
    static void Main()
    {
        Tournament tournament = new Tournament();

        Team teamA = new Team { Name = "Team Alpha", Points = 0 };
        Team teamB = new Team { Name = "Team Beta", Points = 0 };

        Match match = new Match(teamA, teamB);

        tournament.ScheduleMatch(match);
        tournament.RecordMatchResult(match, 3, 1); // Team A wins

        var rankings = tournament.GetRankings();
        Console.WriteLine(rankings[0].Name); // Should output: Team Alpha

        tournament.UndoLastMatch();
        Console.WriteLine(teamA.Points); // Should output: 0 (back to original)
    }
}
