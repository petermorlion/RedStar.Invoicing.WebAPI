using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedStar.Invoicing.Models
{
    public class CommandsDbContext : DbContext
    {
        public DbSet<Command> Commands { get; set; }
    }
}
