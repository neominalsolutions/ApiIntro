using System.Text.Json.Serialization;

namespace ApiIntro.Application.Products.Reponses
{
  // DetailResponse Validation gerek yok
  public class ProductDetailResponse
  {
    [JsonPropertyName("productId")]
    public int Id { get; set; }

    [JsonPropertyName("productName")]
    public string? Name { get; set; }

    [JsonPropertyName("unitPrice")]
    public decimal Price { get; set; }

    [JsonPropertyName("unitsInStock")]
    public int Stock { get; set; }

  }
}
