using Microsoft.AspNetCore.Mvc;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;
using WWW_APP_PROJECT.ViewModels;

namespace WWW_APP_PROJECT.Controllers
{
    public class TeamPlayerController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamPlayerRepository _teamPlayerRepository;
        private readonly IPhotoService _photoService;
       
        public TeamPlayerController(ITeamPlayerRepository teamPlayerRepository, IPhotoService photoService,
            ITeamRepository teamRepository)
        {
            _teamPlayerRepository = teamPlayerRepository;
            _photoService = photoService;
            _teamRepository = teamRepository;
        }
        public async Task<IActionResult> Create(int teamId)
        {
            var teamName = (await _teamRepository.GetByIdAsync(teamId)).Name;
            var CreateTeamPlayerVM = new CreateTeamPlayerViewModel
            {
                TeamId = teamId,
                
                TeamName = teamName
            };
         
            return View(CreateTeamPlayerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamPlayerViewModel TeamPlayerVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(TeamPlayerVM.Image);

                var teamPlayer = new TeamPlayer
                {
                    FirstName = TeamPlayerVM.FirstName,
                    LastName = TeamPlayerVM.LastName,
                    Age = TeamPlayerVM.Age,
                    TeamId = TeamPlayerVM.TeamId,
                    ImageUrl = result.Url.ToString(),


                };
                _teamPlayerRepository.Add(teamPlayer);
                return RedirectToAction("Detail", "Team", new { id = TeamPlayerVM.TeamId });
            
            }   
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(TeamPlayerVM);
        }
    }
}
