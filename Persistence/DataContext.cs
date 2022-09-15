using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Models;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<MVPPrediction> Predictions {get; set; }
        public DbSet<CurrentPlayer> CurrentPlayers {get; set;}
        public DbSet<PastPlayer> PastPlayers {get; set;}
        public DbSet<PastMVP> PastMVPs {get; set;}
    }
}