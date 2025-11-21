using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseItauDigitalAssetsBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseItauDigitalAssetsBank.Infra.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes => Set<Cliente>();
    }
}
