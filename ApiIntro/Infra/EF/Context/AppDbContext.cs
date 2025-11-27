using ApiIntro.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiIntro.Infra.EF.Context
{
  public class AppDbContext:DbContext
  {
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {

    }


  }
}
