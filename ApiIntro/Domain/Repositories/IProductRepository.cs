using ApiIntro.Domain.Entity;

namespace ApiIntro.Domain.Repositories
{
  // Crud
  public interface IProductRepository
  {
    Product? findById(int productId);

    Product? findByName(string productName);
    List<Product> findAll();
    void add(Product product);
    void update(Product product);
    void remove(int productId);



  }
}
