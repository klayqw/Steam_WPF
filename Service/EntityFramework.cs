using Microsoft.EntityFrameworkCore;
using Steam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Service;

public class EntityFramework : DbContext
{
    private const string connectionString = $"Server=localhost;Database=TestSteam;Trusted_Connection=True;TrustServerCertificate=True;";
    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserGames> UserGames { get; set; } 
    public DbSet<Game> Games { get; set; }  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(connectionString);
    }

}
