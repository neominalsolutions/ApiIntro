using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiIntro.Application.Products.Requests
{
  public class ProductUpdateRequest
  {
    [JsonPropertyName("productId")]
    [Required(ErrorMessage = "Product Id is required.")]
    public int? Id { get; set; }


    [JsonPropertyName("productName")]
    [Required(ErrorMessage = "Product name is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "unitPrice is required.")]
    [JsonPropertyName("unitPrice")]
    [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive numeric value.")]
    public decimal? Price { get; set; }

    [JsonPropertyName("unitsInStock")]
    [Required(ErrorMessage = "unitsInStock is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Stock must be a positive integer.")]
    public int? Stock { get; set; }

  }
}
