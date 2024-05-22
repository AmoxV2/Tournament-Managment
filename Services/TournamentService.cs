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
                    return null;
                default:
                    return null;
            }
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


    }
}
