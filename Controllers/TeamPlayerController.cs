using Microsoft.AspNetCore.Mvc;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;
using WWW_APP_PROJECT.ViewModels;

namespace WWW_APP_PROJECT.Controllers
{
    public class TeamPlayerController : Controller
    {
        private readonly ITeamPlayerRepository _teamPlayerRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TeamPlayerController(ITeamPlayerRepository teamPlayerRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _teamPlayerRepository = teamPlayerRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Create(int teamId)
        {

         
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(CreateTeamViewModel TeamVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _photoService.AddPhotoAsync(TeamVM.Image);

        //        var team = new Team
        //        {
        //            Name = TeamVM.Name,
        //            TeamSportDiscipline = TeamVM.TeamSportDiscipline,
        //            AppUserId = TeamVM.AppUserId,
        //            ImageUrl = result.Url.ToString(),


        //        };
        //        _teamRepository.Add(team);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Photo upload failed");
        //    }
        //    return View(TeamVM);
        //}
    }
}
