using Microsoft.AspNetCore.Mvc;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;
using WWW_APP_PROJECT.ViewModels;

namespace WWW_APP_PROJECT.Controllers
{
    public class TournamentController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamToTournamentRepository _teamToTournamentRepository;
        public TournamentController(IPhotoService photoService, IHttpContextAccessor httpContextAccessor, ITournamentRepository tournamentRepository,
            ITeamRepository teamRepository, ITeamToTournamentRepository teamToTournamentRepository)
        {
            this._photoService = photoService;
            this._httpContextAccessor = httpContextAccessor;
            _tournamentRepository = tournamentRepository;
            _teamRepository = teamRepository;
            _teamToTournamentRepository = teamToTournamentRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<TeamTournament> tournaments = (List<TeamTournament>)await _tournamentRepository.GetUserTournaments();
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
            if (tournamentVM.Image == null || tournamentVM.Image.Length == 0)
            {
                ModelState.AddModelError("file", "Please upload an image.");
                return RedirectToAction("SetNumberOfTeams", tournamentVM);
            }
            else
            {
                var result = await _photoService.AddPhotoAsync(tournamentVM.Image);
                var tournament = new TeamTournament
                {
                    Name = tournamentVM.Name,
                    Description = tournamentVM.Description,
                    StartDate = tournamentVM.StartDate,
                    EndDate = tournamentVM.EndDate,
                    TournamentType = tournamentVM.TournamentType,
                    NumberOfTeams = tournamentVM.NumberOfTeams,
                    TeamSportDiscipline = tournamentVM.TeamSportDiscipline,
                    ImageUrl = result.Url.ToString(),
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
            }


            return View(tournamentVM);

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
            return View();
        }
        public async Task<IActionResult> Start(int id)
        {
            var tournamentTeams = await _tournamentRepository.GetTeams(id);
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
    }
}
