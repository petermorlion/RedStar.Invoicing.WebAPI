using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedStar.Invoicing.Models
{
    public class CommandsDbContext : DbContext
    {
        private static bool _created;

        public CommandsDbContext()
        {
            if (!_created)
            {
                Database.AsRelational().ApplyMigrations();
                _created = true;
            }
        }

        public DbSet<Command> Commands { get; set; }
    }
}
