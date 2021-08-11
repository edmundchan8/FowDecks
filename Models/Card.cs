﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FowDecks.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        public string Type{ get; set; }
        [Required]
        public string Cost { get; set; }
        [DisplayName("Total Cost")]
        public string TotalCost{ get; set; }
        [DisplayName("Attack")]
        public int? Atk{ get; set; }
        [DisplayName("Defence")]
        public int? Def{ get; set; }
        public int? Divinity{ get; set; }
        public string Race { get; set; }
        public string Attribute { get; set; }
        [DisplayName("Card Text")]
        public string CardText{ get; set; }
        [DisplayName("Flavor Text")]
        public string FlavorText{ get; set; }
        [DisplayName("Card Number")]
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string Rarity{ get; set; }
        public string Artist{ get; set; }
        [Required]
        public string Set{ get; set; }
        public string Format { get; set; }

    }
}
