using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FowDecks.Models;
using FowDecks.ViewModels;
using FowDecks.Data;

namespace FowDecks.Controllers
{
    public class CardController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CardController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index() 
        {
            Card card = _db.Cards.Find(TempData["cardId"]);
            return View(card);
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            //Check to see if parameter entered is null?
            TempData["cardId"] = id;
            return RedirectToAction("Index");
        }
    }
}
