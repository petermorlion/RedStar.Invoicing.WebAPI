using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedStar.Invoicing.Models
{
    public class InvoicesDbContext : DbContext
    {
        private static bool _created;

        public InvoicesDbContext()
        {
            if (!_created)
            {
                Database.AsRelational().ApplyMigrations();
                _created = true;
            }
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
    }
}
