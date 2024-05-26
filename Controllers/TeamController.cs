using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
                string imageUrl;
                if (TeamVM.Image != null)
                {
                    var result = await _photoService.AddPhotoAsync(TeamVM.Image);
                    imageUrl = result.Url.ToString();
                }
                else
                {
                    imageUrl= "https://img.redro.pl/plakaty/ikona-ludzi-ikona-grupy-ikona-zespolu-700-136664894.jpg";
                }
                var team = new Team
                {
                   Name = TeamVM.Name,
                   TeamSportDiscipline = TeamVM.TeamSportDiscipline,
                   AppUserId = TeamVM.AppUserId,
                   ImageUrl = imageUrl,


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
        public async Task<IActionResult> Edit(int Id)
        {
            var team = await _teamRepository.GetByIdAsync(Id);
            var teamVM = new EditTeamViewModel()
            {
                Id= team.Id,
                Name = team.Name,
               
            };
            return View(teamVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTeamViewModel teamVM)
        {
            if (ModelState.IsValid)
            {
                var team = await _teamRepository.GetByIdAsync(teamVM.Id);
                
                if (teamVM.Image != null)
                {
                    var result = await _photoService.AddPhotoAsync(teamVM.Image);
                    team.ImageUrl = result.Url.ToString();
                }
               
                team.Name = teamVM.Name;
                _teamRepository.Update(team);
                return RedirectToAction("Index");
            }
            return View(teamVM);
        }
        
    }
}
