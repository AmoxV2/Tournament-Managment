using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;
using WWW_APP_PROJECT.ViewModels;

namespace WWW_APP_PROJECT.Controllers
{
    public class ShareController : Controller
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IShareRepository _shareRepository;
        private readonly UserManager<AppUser> _userManager;
        public ShareController(ITournamentRepository tournamentRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager
            , IShareRepository shareRepository)
        {
            _tournamentRepository = tournamentRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _shareRepository = shareRepository;

        }
        public async  Task<IActionResult> Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myShares = await _shareRepository.GetMyShares(userId);
            return View(myShares);
        }
        public async Task<IActionResult> Create(int id)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            var shareViewModel = new CreateShareViewModel
            {
                tournamentName = tournament.Name,
                tournamentId = tournament.Id
            };  
            return View(shareViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateShareViewModel shareViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(shareViewModel.ShareUser);
                if ( user == null)
                {
                    TempData["Error"] = "There is no such a user";
                    return View(shareViewModel);
                }
                var sharedTournament = new SharedTournament()
                {
                    TeamTournamentId = shareViewModel.tournamentId,
                    AppUserId = user.Id,
                    ShareTrybe = shareViewModel.ShareTrybe
                };

                _shareRepository.Add(sharedTournament);

                return RedirectToAction("Index");
            }
            TempData["Error"] = "Wrong Data";
            return View(shareViewModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var share = await _shareRepository.GetById(id);
            _shareRepository.Delete(share);
            return RedirectToAction("Index");
        }
        
    }
}
