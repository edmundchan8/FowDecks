using FowDecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FowDecks.ViewModels
{
    public class CardDatabaseViewModel
    {
        public Card Card{ get; set; }
        public IEnumerable<Card> Cards { get; set; }
    }
}
