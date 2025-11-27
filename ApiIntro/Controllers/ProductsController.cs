using ApiIntro.Application.Products.Reponses;
using ApiIntro.Application.Products.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiIntro.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    // GET status Code 200

    // GET: api/v1/Products -> Get all products
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(new List<ProductDetailResponse>());
    }

    [HttpGet("{id}")]
    // GET: api/v1/Products/{id} -> Get product by id
    public IActionResult GetById(int id)
    {
      return Ok(new ProductDetailResponse());
    }

    // Status Code 201
    // POST: api/v1/Products -> Create a new product
    [HttpPost]
    public IActionResult Post([FromBody] ProductCreateRequest request)
    {
      // yeni açılan kaynağın URI'si ve oluşturulan kaynağın bilgileri döner
      return Created("/api/v1/products/1", new ProductCreateResponse());
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
