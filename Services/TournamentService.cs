using System.Runtime.CompilerServices;
using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Data.Enum;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Services
{
    public class TournamentService
    {
        public static List<TeamMatch> GenerateMatches(TeamTournament tournament, List<Team> teams)
        {
            switch (tournament.TournamentType)
            {
                case TournamentType.League:
                    return GenerateLeagueMatches(tournament, teams);
                case TournamentType.Knockout:
                    return GenerateKnockoutMatches(tournament, teams,1);
                default:
                    return null;
            }
        }
        public static List<TeamMatch> GenerateKnockoutMatches(TeamTournament tournament, List<Team> teams,int stage)
        {
            
            List<TeamMatch> matches = new List<TeamMatch>();

            for (int i = 0; i < teams.Count; i += 2)
            {
                

                matches.Add(new TeamMatch
                {
                    HostTeamId = teams[i].Id,
                    GuestTeamId = teams[i+1].Id,
                    Date = DateTime.Now,
                    Stage = stage,
                    TeamTournamentId = tournament.Id,
                    MatchResult = MatchResult.UnPlayed
                });
            }
            return matches;
        }

        private static List<TeamMatch> GenerateLeagueMatches(TeamTournament tournament, List<Team> teams)
        {
            Random rand = new Random();
            List<TeamMatch> matches = new List<TeamMatch>();

            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    bool isHost = rand.Next(2) == 0;

                    matches.Add(new TeamMatch
                    {
                        HostTeamId = isHost ? teams[i].Id : teams[j].Id,
                        GuestTeamId = isHost ? teams[j].Id : teams[i].Id,
                        Date = DateTime.Now,
                        Stage = 1,
                        TeamTournamentId = tournament.Id,
                        MatchResult = MatchResult.UnPlayed
                    });
                }
            }
            return matches;
        }
        public static void CalcTeamsScores(Dictionary<int, TeamScore> teamScores,List<TeamMatch> matches )
        {
            foreach (var match in matches)
            {
                if (match.MatchResult == MatchResult.HostWin)
                {
                    teamScores[match.HostTeamId].Score += 3;
                    teamScores[match.HostTeamId].Wins += 1;
                    teamScores[match.GuestTeamId].Loses += 1;
                }
                else if (match.MatchResult == MatchResult.GuestWin)
                {
                    teamScores[match.GuestTeamId].Score += 3;
                    teamScores[match.GuestTeamId].Wins += 1;
                    teamScores[match.HostTeamId].Loses += 1;
                }
                else if (match.MatchResult == MatchResult.Draw)
                {
                    teamScores[match.HostTeamId].Score += 1;
                    teamScores[match.GuestTeamId].Score += 1;
                    teamScores[match.HostTeamId].Draws += 1;
                    teamScores[match.GuestTeamId].Draws += 1;
                }

            }
        }
        //true jeżeli runda skończona, false jeżeli nie
        //int - numer aktualnie trwającej rundy
        public static Tuple<bool,int> GetRound(List<List<TeamMatch>> rounds)
        {
            var roundsNumber = rounds.Count;
            for(int i = roundsNumber -1; i >= 0; i--)
            {
                if (rounds[i].Count>0)
                {
                    bool isRoundFinished = true;
                    foreach (var match in rounds[i])
                    {
                        if (match.MatchResult == MatchResult.UnPlayed)
                        {
                            isRoundFinished = false;
                            break;
                        }
                    }
                    if (isRoundFinished)
                    {
                        return new Tuple<bool, int>(true, i+1);
                    }
                    else
                    {
                        return new Tuple<bool, int>(false, i+1);
                    }
                }
            }
            //jeżeli nie ma żadnej rundy
            return new Tuple<bool, int>(false, 0);
            

        }
        public static List<Team> GetTeamsForNextRound(List<TeamMatch> matches)
        {
            List<Team> teams = new List<Team>();
            foreach (var match in matches)
            {
                if (match.MatchResult == MatchResult.HostWin)
                {
                    teams.Add(match.HostTeam);
                }
                else
                {
                    teams.Add(match.GuestTeam);
                }
            }
            return teams;
        }


    }
}
