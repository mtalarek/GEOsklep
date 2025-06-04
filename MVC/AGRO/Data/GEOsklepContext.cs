using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GEOsklep.Models;

namespace GEOsklep.Data
{
    public class GEOsklepContext : DbContext
    {
        public GEOsklepContext (DbContextOptions<GEOsklepContext> options)
            : base(options)
        {
        }

        public DbSet<Artykul> Artykuls { get; set; }
        public DbSet<ArtykulBasket> Basket { get; set; }    //dodane
    }
}
