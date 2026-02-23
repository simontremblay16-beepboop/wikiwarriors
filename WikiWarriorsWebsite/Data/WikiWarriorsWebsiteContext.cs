using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WikiWarriorsWebsite.Models;

namespace WikiWarriorsWebsite.Data
{
    public class WikiWarriorsWebsiteContext : DbContext
    {
        public WikiWarriorsWebsiteContext (DbContextOptions<WikiWarriorsWebsiteContext> options)
            : base(options)
        {
        }

        public DbSet<WikiWarriorsWebsite.Models.Fighter> Fighter { get; set; } = default!;
        public DbSet<WikiWarriorsWebsite.Models.FightHistory> FightHistory { get; set; } = default!;
    }
}
