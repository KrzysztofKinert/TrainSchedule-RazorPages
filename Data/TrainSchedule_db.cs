using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainSchedule.Models;

namespace TrainSchedule.Data
{
    public class TrainSchedule_db : DbContext
    {
        public TrainSchedule_db (DbContextOptions<TrainSchedule_db> options)
            : base(options)
        {
        }

        public DbSet<Connection> Connections { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Station> Stations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>().ToTable("Connection");
            modelBuilder.Entity<Stage>().ToTable("Stage");
            modelBuilder.Entity<Station>().ToTable("Station");
        }
    }
}
