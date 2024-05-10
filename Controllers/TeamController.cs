using Microsoft.AspNetCore.Mvc;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;
using WWW_APP_PROJECT.ViewModels;

namespace WWW_APP_PROJECT.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TeamController(ITeamRepository teamRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _teamRepository = teamRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        

        }
        public async Task<IActionResult> Index()
        {
            List<Team> teams = (List<Team>)await _teamRepository.GetUserTeams();
            return View(teams);
        }
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createTeamVM = new CreateTeamViewModel
            {
                AppUserId = curUserId
            };
            return View(createTeamVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamViewModel TeamVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(TeamVM.Image);

                var team = new Team
                {
                   Name = TeamVM.Name,
                   TeamSportDiscipline = TeamVM.TeamSportDiscipline,
                   AppUserId = TeamVM.AppUserId,
                   ImageUrl = result.Url.ToString(),


                };
                _teamRepository.Add(team);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(TeamVM);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            
            return View(team);
        }
        
    }
}
