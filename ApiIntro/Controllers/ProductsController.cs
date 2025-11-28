using ApiIntro.Application.Products.Reponses;
using ApiIntro.Application.Products.RequestHandlers;
using ApiIntro.Application.Products.Requests;
using ApiIntro.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiIntro.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class ProductsController : ControllerBase
  {
    private readonly ProductCreateRequestHandler productCreateRequestHandler;
    private readonly ProductGetByIdRequestHandler productGetByIdRequestHandler;
    private readonly IProductRepository productRepository;

    public ProductsController(ProductCreateRequestHandler productCreateRequestHandler, ProductGetByIdRequestHandler productGetByIdRequestHandler, IProductRepository productRepository)
    {
      this.productCreateRequestHandler = productCreateRequestHandler;
      this.productGetByIdRequestHandler = productGetByIdRequestHandler;
      this.productRepository = productRepository; 
    }

    // GET status Code 200

    // GET: api/v1/Products -> Get all products
    // Not: eğer dbden çekilecek olan veriler üzerinde herhangi bir logic yoksa, bu yöntem veri çekmek için praktiktir. Ancak, daha karmaşık işlemler için servis katmanı ve application uygulama katmanı veya sorgu işleyicileri kullanmak daha uygun olabilir.
    [HttpGet]
    public IActionResult Get()
    {
      var response = this.productRepository.findAll().Select(p=> new ProductDetailResponse 
      { 
        Id= p.Id,
        Name= p.Name,
        Price= p.Price,
        Stock= p.Stock
      }).ToList();

      return Ok(response);
    }

    [HttpGet("{id}")]
    // GET: api/v1/Products/{id} -> Get product by id
    public IActionResult GetById(int id)
    {
      // diğer örnek ise application katmanında çağırmaktır.
      var response = productGetByIdRequestHandler.Handle(id);

      return Ok(response);
    }

    // Status Code 201
    // POST: api/v1/Products -> Create a new product
    // sadece post yetkisi admin rolüne verilmiş.
    // Roles ="user,admin" şekilde tanımlarsak or gibi yani ya admin yada user yetkisi yeterli olur.
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="user,admin")]
    public IActionResult Post([FromBody] ProductCreateRequest request)
    {

      var response = productCreateRequestHandler.Handle(request);

      // yeni açılan kaynağın URI'si ve oluşturulan kaynağın bilgileri döner
      return Created($"/api/v1/products/{response.Id}", response);
    }

    // Status Code 204
    [HttpPut("{id}")]
    // PUT: api/v1/Products/{id} -> Update product by id
    public IActionResult Put(int id, [FromBody] ProductUpdateRequest request)
    {
      if(id!= request.Id)
        return BadRequest();

      return NoContent();
    }

    // Status Code 204
    [HttpDelete("{id}")]
    // DELETE: api/v1/Products/{id} -> Delete product by id
    public IActionResult Delete(int id)
    {

      return NoContent();
    }

    // Status Code 204
    [HttpPatch("{id}/stockIn")]
    // PATCH: api/v1/Products/{id}/stockIn -> Partially update product stock by id
    public IActionResult Patch(int id, [FromBody] ProductStockInRequest request)
    {
      if (id != request.ProductId)
        return BadRequest();


      return NoContent();

    }

  }
}
