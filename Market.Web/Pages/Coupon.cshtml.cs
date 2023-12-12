using Htmx;
using Market.Web.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Market.Web.Pages
{
    public class Coupon : PageModel
    {
        private static List<Game> Games = new()
        {
            new Game(1993, "Super Mario Bros. 3", "Nintendo", "NES"),
            new Game(1992, "The Legend of Zelda: A Link To The Past", "Nintendo", "SNES"),
            new Game(1992, "Street Fighter II Turbo", "Capcom", "Arcade"),
            new Game(1992, "Sonic The Hedgehog 2", "Sega", "Mega Drive"),
            new Game(1986, "Outrun", "Sega", "Arcade"),
            new Game(1978, "Space Invaders", "Taito", "Arcade"),
            new Game(1992, "Streets Of Rage 2", "Sega", "Mega Drive"),
            new Game(1994, "Super Metroid", "Nintendo", "SNES"),
            new Game(1972, "Pong", "Atari", "Atari"),
            new Game(1996, "Resident Evil", "Capcom", "Playstation"),
            new Game(1995, "Chrono Trigger", "Squaresoft", "SNES")
        };

        [BindProperty(SupportsGet = true)]
        public string? Query { get; set; }

        public List<Game> Results { get; private set; }
            = Games;

        public IActionResult OnGet()
        {
            Results = string.IsNullOrEmpty(Query)
                ? Games
                : Games.Where(g => g.ToString().Contains(Query, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                // we want to push the current url 
                // into the history
                h.Push(Request.GetEncodedUrl());
            });

            return Partial("_Results", this);
        }
    }
}
