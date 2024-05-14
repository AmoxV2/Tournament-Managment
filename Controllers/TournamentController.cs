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
        public TournamentController(IPhotoService photoService, IHttpContextAccessor httpContextAccessor, ITournamentRepository tournamentRepository)
        {
            this._photoService = photoService;
            this._httpContextAccessor = httpContextAccessor;
            _tournamentRepository = tournamentRepository;
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
    }
}
