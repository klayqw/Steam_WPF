using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Service;

internal class EntityFramework : DbContext
{
    private const string connectionString = $"Server=localhost;Database=TestProj;User Id=admin;Password=admin;TrustServerCertificate=True;";
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(connectionString);
    }

}
