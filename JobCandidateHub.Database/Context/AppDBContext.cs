using JobCandidateHub.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace JobCandidateHub.Database.Context;
public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
        //Code to apply Migration to specific folder: Add-Migration InitialCreate -OutputDir Migrations
        //Eg: Code to apply Migration to specific folder: Add-Migration InitialCreate -OutputDir Database\Migrations
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection")!);
            return new AppDBContext(optionsBuilder.Options);
        }
    }

    #region DbSet
    public DbSet<Candidate> Candidate { get; set; }
    #endregion DbSet

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var cascadeFKs = builder.Model.GetEntityTypes()
        .SelectMany(t => t.GetForeignKeys())
        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }
}

