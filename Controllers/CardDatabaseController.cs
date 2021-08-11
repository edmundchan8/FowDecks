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
    public class CardDatabaseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CardDatabaseController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            CardDatabaseViewModel cardDbViewModel = new()
            {
                Card = null,
                Cards = _db.Cards

        };
            return View(cardDbViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Card card)
        {
            if (ModelState.IsValid)
            {
                _db.Add(card);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else 
            {
                return View(card);
            }
        }
    }
}
