using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiIntro.Application.Products.Requests
{
  // şu ürün için stokları artır
  public class ProductStockInRequest
  {
    [JsonPropertyName("productId")]
    [Required(ErrorMessage ="Product Id is required")]
    public int? ProductId { get; set; }

    [JsonPropertyName("quantity")]
    [Range(1,10,ErrorMessage ="Max 10 quantity")]
    [Required(ErrorMessage = "Quantity is required")]
    public int? Quantity { get; set; }
  }
}
