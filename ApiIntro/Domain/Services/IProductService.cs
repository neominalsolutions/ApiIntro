namespace ApiIntro.Domain.Services
{
  public interface IProductService
  {
     void StockIn(int productId, int quantity);
      void Create(Entity.Product product);
      void Update(Entity.Product product);
      void Delete(int productId);
      
  }
}
