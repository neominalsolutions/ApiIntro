using ApiIntro.Application.Products.Reponses;
using ApiIntro.Application.Products.Requests;

namespace ApiIntro.Application.Products.RequestHandlers
{
  public class ProductCreateRequestHandler
  {

    // Simulate product creation and return a response with a new product ID

    public ProductCreateResponse Handle(ProductCreateRequest request)
    {
      return new ProductCreateResponse
      {
        Id = 1
      };
    }

  }
}
