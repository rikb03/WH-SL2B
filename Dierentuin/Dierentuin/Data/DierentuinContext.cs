using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dierentuin.Models;

namespace Dierentuin.Data
{
    public class DierentuinContext : DbContext
    {
        public DierentuinContext (DbContextOptions<DierentuinContext> options)
            : base(options)
        {
        }

        public DbSet<Dierentuin.Models.Animal> Animal { get; set; } = default!;
        public DbSet<Dierentuin.Models.Category> Category { get; set; }
        public DbSet<Dierentuin.Models.Enclosure> Enclosure { get; set; }
    }
}
