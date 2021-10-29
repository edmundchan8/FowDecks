using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FowDecks.Models;
using FowDecks.ViewModels;
using Microsoft.EntityFrameworkCore;
using FowDecks.Data;
using Newtonsoft.Json;

namespace FowDecks.Controllers
{
    public class SearchResultsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SearchResultsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            CardDatabaseViewModel cardViewModel = new()
            {
                Card = null,
                Cards = null
            };

            if (TempData["cardResults"] != null) {
                //need to deserializeobject to use model
                IEnumerable<Card> cards = JsonConvert.DeserializeObject<IEnumerable<Card>>((string)TempData["cardResults"]);
                cardViewModel.Cards = cards;
                return View(cardViewModel);
            }
            else
            {
                return View(cardViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CardDatabaseViewModel cardDBVM)
        {
            var cards = from allCards in _db.Cards
                        select allCards;

            var cardName = "";
            var cardType = "";
            var cardText = "";

            if (!String.IsNullOrEmpty(cardDBVM.Card.Name))
                cardName = cardDBVM.Card.Name;

            if (!String.IsNullOrEmpty(cardDBVM.Card.Type))
                cardType = cardDBVM.Card.Type;

            if (!String.IsNullOrEmpty(cardDBVM.Card.CardText))
                cardText = cardDBVM.Card.CardText;


            cards = cards.Where(c => c.CardText.Contains(cardText)
                && c.Name.Contains(cardName)
                && c.Type.Contains(cardType));

            // TempData as you can't redirectToAction a model directly, only strings
            // Need to serialize object to access it later
            TempData["cardResults"] = JsonConvert.SerializeObject(cards);

            return RedirectToAction ("Index");
        }
    }
}
