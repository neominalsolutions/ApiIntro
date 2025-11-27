using ApiIntro.Application.Products.Reponses;
using ApiIntro.Domain.Repositories;

namespace ApiIntro.Application.Products.RequestHandlers
{
  public class ProductGetByIdRequestHandler
  {
    private readonly IProductRepository productRepository;

    public ProductGetByIdRequestHandler(IProductRepository productRepository)
    {
      this.productRepository = productRepository;
    }

    public ProductDetailResponse Handle(int id)
    {
      var product = this.productRepository.findById(id);
      if (product == null) 
        return null;

      return new ProductDetailResponse
      {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        Stock = product.Stock
      };
    }

  }
}
