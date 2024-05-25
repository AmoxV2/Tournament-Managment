using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
using System.Net.NetworkInformation;
using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Data.Enum;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;
using WWW_APP_PROJECT.Services;
using WWW_APP_PROJECT.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace WWW_APP_PROJECT.Controllers
{
    public class TournamentController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamToTournamentRepository _teamToTournamentRepository;
        private readonly IMatchRepository _matchRepository;
        
        public TournamentController(IPhotoService photoService, IHttpContextAccessor httpContextAccessor, ITournamentRepository tournamentRepository,
            ITeamRepository teamRepository, ITeamToTournamentRepository teamToTournamentRepository, IMatchRepository matchRepository)
        {
            this._photoService = photoService;
            this._httpContextAccessor = httpContextAccessor;
            _tournamentRepository = tournamentRepository;
            _teamRepository = teamRepository;
            _teamToTournamentRepository = teamToTournamentRepository;
            _matchRepository = matchRepository;
            
        }
        public async Task<IActionResult> Index()
        {
            List<TeamTournament> tournaments = (List<TeamTournament>)await _tournamentRepository.GetUserTournaments();
            tournaments.Reverse();
            return View(tournaments);
            
        }
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createTournamentVM = new CreateTournamentViewModel
            {
                AppUserId = curUserId,
                NumberOfTeams = 0
            };
            return View(createTournamentVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentViewModel tournamentVM)
        {
            if (ModelState.IsValid)
            { 
                return RedirectToAction("SetNumberOfTeams",tournamentVM);
            }
            
            return View(tournamentVM);

        }
        
        public IActionResult SetNumberOfTeams(CreateTournamentViewModel tournamentVM)
        {
            return View(tournamentVM);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFin(CreateTournamentViewModel tournamentVM)
        {
            string resultUrl;
            if (tournamentVM.Image == null || tournamentVM.Image.Length == 0)
            {
                resultUrl = "data:image / png; base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAbFBMVEX///8AAADb29vPz8/T09O1tbVqamq6urqDg4Po6Ojw8PBOTk6urq739/fc3Nzh4eHAwMAkJCTFxcWlpaVhYWGNjY0dHR1/f38pKSmdnZ3KyspWVlY7OzuVlZUWFhZ0dHQzMzNISEgNDQ1lZWUipwcEAAAE/UlEQVR4nO2d6XbiMAxGCTshAVqgbG2h7fu/4wwt0xZZgiy2ZM/57m+d4EsS23IcpdMBAAAAAAAAAAAAACBGyq41eWDDfWbNILBhz1owG8EQhjC0FoQhDGEIQxjCEIYwhCEMYQhDGMIQhjCsRN9asLlh2etXobAWzHbdKu3slY5h17rlnunCMHlgmD4wTB8Ypg8M0weGWXYYshxMmvubKd+wFQm7b+hGfGKfPS34huUk7L5hnz9QtBnwhITBEIaGwPACDGFoCAwvwBCGhlQ0nDkRMxLR4w8U7cybGs6diHm1A9mnkY/VGjZxImj2IWyJp3+EPnu+YQsS5j6Z6ZCINX8gejHoIySuSxLGhJCIA38geqr1cS+/T4bXUU9MyJQciT9QqWYiIbwzQ6JWTMiGxAjDxauWiQRzg3Xc/oG7yQYkZswbPmuZCDzwzXohYdxQQP+Fd/5Q5HpXZ8o364GEsXcrPRY/XtA/Sxv+0qJjBX+m6dl5ZaNGWioCS7ZVWxK1qdb2gouyntSws0nah0gTH+dobMes4iHDdaXuEMYLup0Ie1e/KWjIbLkmOY8ahHGAyYxemKixionEkWmR2/m5icWFkxPK9KePGiIiTIPcvk8YUdhYJt+0nZm64xwdKDJxOnaGjpsZ1ztbzmrccY7mFNmtU8j+H9mQBu2Ce8g4001uiiXkV1/QBOPMlgxBdL1DE3L99ehIf4brjH7gE9zVdd/EHVaH67nynD73/YJPPr4Rpp2H33N1u8v090W64K63TFxg+kHsR4bF7Ovfye3qKoy+ZlnlrBBTnNvX6Gf7NVvsn9NdwRhWRNtQqS6IdX7UBnelm4UZRBNBWEt1sc7jm1JZsNOxf+egCTemoy72hXZq8yQsFUtMmEl41HzU8ztDV4jjhl1Tukffbv5Zl1XT8liJDBunWl3MNeXauvX3eW75ul65jLvL+Whx/r7pj4XnTXr36TP/Nx+KO7lgdebFkfzG6qWv9yzxb+I7X1634H21E/ZSNKecL4rlX4pRb37purQWFi8LL+Vktl8MisGiN/F27u6h0xHttHQ4hJUErziLfbo8BRc82Qoq7D5Ru+UkQm8hqpkxhCDsus7N9WstQm5X9DFj8UC4sxjFGTwT6l4UH3Tqk4eYnm9D10Suh/+39oStg3b4frwvbTYwhHu62hxhQ4wt+Zs3v6n5REbA14PFRgtnOkx85BqHuPpQyuK9pd/r3We45rS7VE2z3aq0WILcxNrDUPJmj+de4r4BCUXdpcZTxB2oQLfOo51xNFlEPR43VZZxtutIssBmzJY3N/e/D4sIlilaIz/CSu/e43H2mX8T+psxWsAwfWCYPjBMHximDwzTB4bpA8P0gWH6wDB9YJg+MEwfGKYPDNMHhukDw/SBYfrAMH1gmD7/v6FcSiT0FzfDkPeL3Xr8G77Y0ZnVVdx4V/Sj37z3eBRtqnKsUb9Dm9zXGwnrOM9k2f70/bCJ0NF3/YXYtrx5fNfiH3G9cxGmMG3Fok8ahHovP5peNVxBokg2D4d81zmKDcRBq2MIFdh1CftFROOCCmfkxMEP9i8iBhaUyvvrEb48H19mXo/ggmJ9dSU0qoHZpslujXP/3CpeHRydbyNZZlI6Jess16vkFRif8J/j0EFFkP12kxJadaPtDLXqz9tlGMoV6QzQqottN/vO9z0N9hEuLQIAAAAAAAAAAAAAQPkDIQZtX2EDBXwAAAAASUVORK5CYII";
            }
            else
            {
                resultUrl = (await _photoService.AddPhotoAsync(tournamentVM.Image)).Url.ToString();
            }
            var tournament = new TeamTournament
            {
                Name = tournamentVM.Name,
                Description = tournamentVM.Description,
                StartDate = tournamentVM.StartDate,
                EndDate = tournamentVM.EndDate,
                TournamentType = tournamentVM.TournamentType,
                NumberOfTeams = tournamentVM.NumberOfTeams,
                TeamSportDiscipline = tournamentVM.TeamSportDiscipline,
                ImageUrl = resultUrl,
                Address = new Address
                {
                    Street = tournamentVM.Street,
                    City = tournamentVM.City,
                    State = tournamentVM.State
                },
                IsPublic = tournamentVM.IsPublic,
                AppUserId = tournamentVM.AppUserId
            };
            _tournamentRepository.Add(tournament);
            return RedirectToAction("Index");
        }
            public async Task<IActionResult> Manage(int id)
        {
            var torunamentTeams = await _tournamentRepository.GetTeams(id);
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            if (torunamentTeams.Count < tournament.NumberOfTeams)
            {
                return RedirectToAction("AddTeams", new { id = id });
            }
            else if (!_tournamentRepository.hasTorunamentStarted(id))
            {
                return RedirectToAction("Start", new { id = id }); ;
            }
            else if(tournament.WinnerTeamId!=null)
            {
                if (tournament.TournamentType == TournamentType.League)
                {
                    return RedirectToAction("LeagueResults", new { id = id });
                }
                else
                {
                    return RedirectToAction("ManageKnockout", new { id = id });
                }
            }
            else
            {
                if (tournament.TournamentType == TournamentType.League)
                {
                    return RedirectToAction("ManageLeague", new { id = id });
                }
                else
                {
                    return RedirectToAction("ManageKnockout", new { id = id });
                }
                
            }
            
        }
        public async Task<IActionResult> ManageKnockout(int id)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            var matches = await _matchRepository.GetMatchesByTournament(id);
            var tournamentTeams = await _tournamentRepository.GetTeams(id);
            var roundsCount = (int)Math.Log2(tournamentTeams.Count);
            List<List<TeamMatch>> rounds = new List<List<TeamMatch>>();
            for (int i =0; i<roundsCount; i++)
            {
                rounds.Add(new List<TeamMatch>());
            }
            foreach (var match in matches)
            {
                rounds[match.Stage - 1].Add(match);
            }
            Tuple<bool,int> currentRound = TournamentService.GetRound(rounds);
            if (currentRound.Item1==true && currentRound.Item2<roundsCount)
            {
                List<Team> teamsForNextRound = TournamentService.GetTeamsForNextRound(rounds[currentRound.Item2-1]);
                List<TeamMatch> nextRound = TournamentService.GenerateKnockoutMatches(tournament, teamsForNextRound, currentRound.Item2 + 1);

                foreach (var match in nextRound)
                {
                    _matchRepository.Add(match);
                    rounds[currentRound.Item2].Add(match);
                }
            }
            else if(currentRound.Item1 == true && currentRound.Item2==roundsCount) 
            {
                if(tournament.WinnerTeamId==null)
                {
                    if (rounds[roundsCount - 1][0].MatchResult == MatchResult.HostWin)
                    {
                        tournament.WinnerTeamId = rounds[roundsCount - 1][0].HostTeamId;
                    }
                    else
                    {
                        tournament.WinnerTeamId = rounds[roundsCount - 1][0].GuestTeamId;
                    }
                    _tournamentRepository.Update(tournament);
                }
            }
            var knockoutVM = new ManageKnockoutViewModel
            {
                Rounds = rounds,
                Tournament = tournament
            };

            return View(knockoutVM);
        }
        public async Task<IActionResult> ManageLeague(int id)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            var matches = await _matchRepository.GetMatchesByTournament(id);
            var tournamentTeams = await _tournamentRepository.GetTeams(id);
            bool isFinished = true;

            //chceck if all matches are played
            foreach(var match in matches)
            {
                if (match.MatchResult == MatchResult.UnPlayed)
                {
                    isFinished = false;
                    break;
                }
            }
            if(isFinished)
            {
                return RedirectToAction("LeagueResults", new { id = id });
            }
            Dictionary<int, TeamScore> teamScores = new Dictionary<int, TeamScore>();
            foreach (var team in tournamentTeams)
            {
                teamScores.Add(team.Id, new TeamScore());
            }
            TournamentService.CalcTeamsScores(teamScores, matches);
            List<TeamScoreViewModel> results = new List<TeamScoreViewModel>();
            foreach (var team in tournamentTeams)
            {
                results.Add(new TeamScoreViewModel
                {
                    team = team,
                    teamScore = teamScores[team.Id]
                });
            }
            results.Sort((x, y) => y.teamScore.Score.CompareTo(x.teamScore.Score));

            var manageLeagueVM = new ManageLeagueViewModel
            {
                Tournament = tournament,
                Matches = matches,
                TeamScores = results
            };
            return View(manageLeagueVM);
        }
        public async Task<IActionResult> LeagueResults(int id)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            var matches = await _matchRepository.GetMatchesByTournament(id);
            var tournamentTeams = await _tournamentRepository.GetTeams(id);

            Dictionary<int, TeamScore> teamScores = new Dictionary<int, TeamScore>();
            foreach (var team in tournamentTeams)
            {
                teamScores.Add(team.Id, new TeamScore());
            }
            TournamentService.CalcTeamsScores(teamScores, matches);
            if (tournament.WinnerTeamId == null)
            {
                int bestscore = -1;
                foreach(Team team in tournamentTeams)
                {
                    if (teamScores[team.Id].Score > bestscore)
                    {
                        bestscore = teamScores[team.Id].Score;
                        tournament.WinnerTeamId = team.Id;
                    }
                }
                _tournamentRepository.Update(tournament);
            }
            List<TeamScoreViewModel> results = new List<TeamScoreViewModel>();
            foreach (var team in tournamentTeams)
            {
                results.Add(new TeamScoreViewModel
                {
                    team = team,
                    teamScore = teamScores[team.Id]
                });
            }
            results.Sort((x, y) => y.teamScore.Score.CompareTo(x.teamScore.Score));
            
            
            return View(results);
        }
        public async Task<IActionResult> Start(int id)
        {
            var tournamentTeams = await _tournamentRepository.GetTeams(id);
            ViewData["TournamentId"] = id;
            return View(tournamentTeams);
        }
        public async Task<IActionResult> AddTeams(int id)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            List<Team> torunamentTeams = await _tournamentRepository.GetTeams(id);
            List<Team> availableTeams = await _teamRepository.GetTeamsBySport(tournament.TeamSportDiscipline);
            availableTeams.RemoveAll(team => torunamentTeams.Any(t => t.Id == team.Id));

            var addTeamsTournamentVM = new AddTeamsTournamentViewModel
            {
                TournamentTeams = torunamentTeams,
                AvailableTeams = availableTeams,
                Tournament = tournament,
                
            };

            return View(addTeamsTournamentVM);
        }
        public IActionResult AddTeam(int tournamentId, int teamId)
        {
            var teamtoTournament = new TeamToTournament
            {
                TeamId = teamId,
                TeamTournamentId = tournamentId
            };
            _teamToTournamentRepository.Add(teamtoTournament);
            return RedirectToAction("Manage", new { id = tournamentId });
        }
        public IActionResult RemoveTeam(int tournamentId, int teamId)
        {
            var teamToTournament = _teamToTournamentRepository.GetById(teamId, tournamentId);
            _teamToTournamentRepository.Delete(teamToTournament);
            return RedirectToAction("Manage", new { id = tournamentId });
        }
        public async Task<IActionResult> GenerateMatches(int id)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            var teams = await _tournamentRepository.GetTeams(id);
            var matches = TournamentService.GenerateMatches(tournament,teams);
            foreach (var match in matches)
            {
                _matchRepository.Add(match);
            }
            return RedirectToAction("Manage", new { id = id });
        }
       
    }
}
