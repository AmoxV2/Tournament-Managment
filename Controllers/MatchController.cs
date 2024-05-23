using Microsoft.AspNetCore.Mvc;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchRepository _matchRepository;
        public MatchController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }
        public async Task<IActionResult> Edit(int id)
        {
            var match = await _matchRepository.GetMatchById(id);
            return View(match);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TeamMatch match)
        {

            var curMatch = await _matchRepository.GetMatchById(match.Id);
            curMatch.HostScore = match.HostScore;
            curMatch.GuestScore = match.GuestScore;
            curMatch.Date = match.Date;
            if (match.HostScore >= 0 && match.GuestScore >= 0)
            {
                if (curMatch.HostScore > curMatch.GuestScore)
                {
                    curMatch.MatchResult = Data.Enum.MatchResult.HostWin;
                }
                else if (curMatch.HostScore < curMatch.GuestScore)
                {
                    curMatch.MatchResult = Data.Enum.MatchResult.GuestWin;
                }
                else
                {
                    curMatch.MatchResult = Data.Enum.MatchResult.Draw;
                }


                if (_matchRepository.Update(curMatch))
                    return RedirectToAction("Manage", "Tournament", new { id = curMatch.TeamTournamentId });
            }

           

            return View(curMatch);
        }
    }
}
