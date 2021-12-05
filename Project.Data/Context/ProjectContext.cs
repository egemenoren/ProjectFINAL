using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base("ProjectConnString")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<WateringHistory> WateringHistories { get; set; }
        public DbSet<HumidityHistory> HumidityHistories { get; set; }
    }
}
