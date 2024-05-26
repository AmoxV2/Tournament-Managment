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
                string imageUrl;
                if(TeamPlayerVM.Image != null)
                {
                    var result = await _photoService.AddPhotoAsync(TeamPlayerVM.Image);
                    imageUrl = result.Url.ToString();
                }
                else
                {
                    imageUrl = "https://www.pngitem.com/pimgs/m/146-1468479_my-profile-icon-blank-profile-picture-circle-hd.png";
                }
              //  var result = await _photoService.AddPhotoAsync(TeamPlayerVM.Image);

                var teamPlayer = new TeamPlayer
                {
                    FirstName = TeamPlayerVM.FirstName,
                    LastName = TeamPlayerVM.LastName,
                    Age = TeamPlayerVM.Age,
                    TeamId = TeamPlayerVM.TeamId,
                    ImageUrl = imageUrl,


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
        public async Task<IActionResult> Detail(int id)
        {
            var teamPlayer = await _teamPlayerRepository.GetByIdAsync(id);
            return View(teamPlayer);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var teamPlayer = await _teamPlayerRepository.GetByIdAsync(id);
            _teamPlayerRepository.Delete(teamPlayer);
            return RedirectToAction("Detail", "Team", new { id = teamPlayer.TeamId });
        }
       
        public async Task<IActionResult> Edit(int Id)
        {
            var player = await _teamPlayerRepository.GetByIdAsync(Id);
            var playerVM = new EditPlayerViewModel()
            {
                Id= player.Id,
                Age = player.Age,
                FirstName = player.FirstName,
                LastName = player.LastName,
                TeamId = player.TeamId,

            };
            return View(playerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditPlayerViewModel playerVM)
        {
            if (ModelState.IsValid)
            {
                var player = await _teamPlayerRepository.GetByIdAsync(playerVM.Id);
                
                if (playerVM.Image != null)
                {
                    var result = await _photoService.AddPhotoAsync(playerVM.Image);
                    player.ImageUrl = result.Url.ToString();
                }
               
                player.FirstName = playerVM.FirstName;
                player.LastName = playerVM.LastName;
                player.Age = playerVM.Age;

               
                _teamPlayerRepository.Update(player);
                return RedirectToAction("Detail", "TeamPlayer", new { id = player.Id });

            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(playerVM);
        }
    }
}
