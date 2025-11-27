using System.Text.Json.Serialization;

namespace ApiIntro.Application.Products.Reponses
{
  public class ProductCreateResponse
  {
    [JsonPropertyName("productId")]
    public int Id { get; set; }
  }
}
