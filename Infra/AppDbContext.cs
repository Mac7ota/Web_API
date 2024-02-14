using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Infra;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("");
}