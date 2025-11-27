using ApiIntro.Domain.Entity;
using ApiIntro.Domain.Repositories;
using ApiIntro.Infra.EF.Context;

namespace ApiIntro.Infra.EF.Repository
{
  public class EFProductRepository : IProductRepository
  {
    private readonly AppDbContext dbContext;

    public EFProductRepository(AppDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public void add(Product product)
    {
     dbContext.Products.Add(product);
      dbContext.SaveChanges();
    }

    public List<Product> findAll()
    {
      return dbContext.Products.ToList();
    }

    public Product? findById(int productId)
    {
      return dbContext.Products.Find(productId);
    }

    public Product? findByName(string productName)
    {
      return dbContext.Products.FirstOrDefault(p => p.Name == productName);
    }

    public void remove(int productId)
    {
      var product = dbContext.Products.Find(productId);
      if (product != null)
      {
        dbContext.Products.Remove(product);
        dbContext.SaveChanges();
      }
    }

    public void update(Product product)
    {
      dbContext.Products.Update(product);
      dbContext.SaveChanges();
    }

  }
}
