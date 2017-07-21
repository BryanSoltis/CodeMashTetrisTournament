using CodeMashTetrisTournament.Models;
using KenticoCloud.Deliver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace CodeMashTetrisTournament.Controllers
{
    public class HomeController : Controller
    {

        private static readonly IList<HighScore> _highscores;
        private static DeliverClient client = new DeliverClient(ConfigurationManager.AppSettings["KenticoDeliverProjectID"]);

        static HomeController()
        {

            _highscores = new List<HighScore>
            {

            };
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public async Task<ActionResult> HighScores()
        {
            // CLear out the list of high scores
            _highscores.Clear();

            var filters = new List<IFilter> {
                new EqualsFilter("system.type", "high_score"), // Fetch the just high scores
                new Order("elements.score", OrderDirection.Descending) // We want to order them by their score
            };

            var response = await client.GetItemsAsync(filters);

            int i = 1;

            foreach (var item in response.Items)
            {
                try
                {
                    HighScore highscore = new HighScore();
                    highscore.Name = item.System.Name;
                    highscore.Score = item.GetNumber("score");
                    _highscores.Add(highscore);

                    i += 1;
                }
                catch(Exception ex)
                { }
            }

            return Json(_highscores, JsonRequestBehavior.AllowGet);
        }

    }
}