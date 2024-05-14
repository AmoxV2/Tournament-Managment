using Microsoft.AspNetCore.Mvc;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.ViewModels;

namespace WWW_APP_PROJECT.Controllers
{
    public class TournamentController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TournamentController(IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            this._photoService = photoService;
            this._httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createTournamentVM = new CreateTournamentViewModel
            {
                AppUserId = curUserId
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
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(tournamentVM);

        }
        public IActionResult SetNumberOfTeams()
        {
            return View();
        }
    }
}
