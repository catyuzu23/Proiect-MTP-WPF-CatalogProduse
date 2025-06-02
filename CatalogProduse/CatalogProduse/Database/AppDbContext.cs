using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogProduse.Database
{
    class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Exercitiu> Exercitii { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;port=3306;database=GymFit;user=root;password=caty23mai";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
