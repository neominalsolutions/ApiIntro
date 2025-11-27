using ApiIntro.Application.Products.Reponses;
using ApiIntro.Application.Products.Requests;
using ApiIntro.Domain.Entity;
using ApiIntro.Domain.Services;

namespace ApiIntro.Application.Products.RequestHandlers
{
  public class ProductCreateRequestHandler
  {
    // DIP
    private readonly IProductService productService;

    public ProductCreateRequestHandler(IProductService productService)
    {
      this.productService = productService;
    }

    // Simulate product creation and return a response with a new product ID

    public ProductCreateResponse Handle(ProductCreateRequest request)
    {
      var entity = new Product
      {
        Name = request.Name,
        Price = request.Price ?? 0,
        Stock = request.Stock ?? 0
      };

      this.productService.Create(entity);

      return new ProductCreateResponse
      {
        Id = entity.Id
      };
    }

  }
}
