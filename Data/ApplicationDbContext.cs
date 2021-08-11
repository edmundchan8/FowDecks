using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FowDecks.Models;
using Microsoft.EntityFrameworkCore;


namespace FowDecks.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Card> Cards{ get; set; }
    }
}
