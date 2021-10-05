using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FowDecks.Models;
using FowDecks.ViewModels;
using FowDecks.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace FowDecks.Controllers
{
    public class CardDatabaseController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CardDatabaseController(ApplicationDbContext db, IWebHostEnvironment HostEnv)
        {
            _db = db;
            webHostEnvironment = HostEnv;
        }
        public int MyProperty { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            CardDatabaseViewModel cardDbViewModel = new()
            {
                Card = null,
                Cards = null
            };

            return View(cardDbViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CardDatabaseViewModel cardDBVM)
        {
            if (ModelState.IsValid)
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

                if(!String.IsNullOrEmpty(cardDBVM.Card.CardText))
                    cardText = cardDBVM.Card.CardText;


                cardDBVM.Cards = cards.Where(c => c.CardText.Contains(cardText)
                    && c.Name.Contains(cardName)
                    && c.Type.Contains(cardType));
                
                return View(cardDBVM);
            }
            else 
            {
                cardDBVM.Card = null;
                cardDBVM.Cards = null;
            }
                return View(cardDBVM);
        }

        [HttpPost]
        public IActionResult GetCardName([FromBody]Card card)
        {
            return Json(card.Name);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCardViewModel createCardVM)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;
                    if (createCardVM.Image != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + createCardVM.Image.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        createCardVM.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    }

                    int AtkValue = 0;

                    Card card = new() 
                    {
                        Name = createCardVM.Name,
                        Image = uniqueFileName,
                        Type = createCardVM.Type,
                        Cost = createCardVM.Cost,
                        TotalCost = createCardVM.TotalCost,
                        Atk = AtkValue,
                        Def = createCardVM.Def,
                        Divinity = createCardVM.Divinity,
                        Race = createCardVM.Race,
                        Attribute = createCardVM.Attribute,
                        CardText = createCardVM.CardText,
                        FlavorText = createCardVM.FlavorText,
                        CardNumber = createCardVM.CardNumber,
                        Rarity = createCardVM.Rarity,
                        Artist = createCardVM.Artist,
                        Set = createCardVM.Set,
                        Format = createCardVM.Format
                    };
                    _db.Add(card);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(createCardVM);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");
        }
    }
}
