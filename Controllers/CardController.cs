using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FowDecks.Models;
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
        public IActionResult Index(int id)
        {
            Card card = _db.Cards.Where(c => c.Id == id)
                .FirstOrDefault();

            return View(card);
        }
    }
}
