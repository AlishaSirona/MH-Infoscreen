using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInfoscreenLibrary;

public class DbInfoscreenContext : DbContext
{
    private const string connectionString = "server=localhost;port=3306;database=Infoscreen;user=root;password=geheim";
    readonly ServerVersion serverVersion = MySqlServerVersion.AutoDetect(connectionString);

    public DbSet<Pages> Pages { get; set; } = null!;
    public DbSet<VersandDaten> VersandDaten { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<VersandDaten>()
            .HasIndex(x => x.Date)
            .IsUnique();
    }
}
