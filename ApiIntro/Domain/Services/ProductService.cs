using ApiIntro.Domain.Entity;
using ApiIntro.Domain.Repositories;

namespace ApiIntro.Domain.Services
{
  public class ProductService: IProductService
  {
    // EFProductRepository alt yapısı ile Program.cs dosyasında çalışacak.
    private readonly IProductRepository productRepository;
    public ProductService(IProductRepository productRepository)
    {
      this.productRepository = productRepository;
    }

    public void StockIn(int productId, int quantity)
    {
      var product = productRepository.findById(productId);
      if (product == null)
      {
        throw new Exception("Product not found");
      }
      product.Stock += quantity;
      productRepository.update(product);
    }

    public void Create(Product product)
    {
      // find if a product with the same name already exists
      var existingProduct = productRepository.findByName(product.Name);
      if (existingProduct != null)
      {
        throw new Exception("Product with the same name already exists");
      }

      productRepository.add(product);
    }

    public void Update(Product product)
    {
      var existingProduct = productRepository.findById(product.Id);
      if (existingProduct == null)
      {
        throw new Exception("Product not found");
      }

      productRepository.update(product);
    }

    public void Delete(int productId)
    {
      var existingProduct = productRepository.findById(productId);
      if (existingProduct == null)
      {
        throw new Exception("Product not found");
      }
      productRepository.remove(productId);
    }

  }
}
