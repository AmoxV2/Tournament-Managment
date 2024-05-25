using Microsoft.AspNetCore.Mvc;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.ViewModels;

namespace WWW_APP_PROJECT.Controllers
{
    public class ShareController : Controller
    {
        private readonly ITournamentRepository _tournamentRepository;
        public ShareController(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }
        public IActionResult Index()
        {
            return View();
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
            return View(shareViewModel);
        }    
    }
}
