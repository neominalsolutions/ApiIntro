namespace ApiIntro.Middleewares
{

  public class CustomErrorMiddleware:IMiddleware
  {
    // InvokeAsync özel bir isimdir runtimeda çalışır. ismi değiştiremeyiz.
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      // Multi Tread sample
      Parallel.Invoke(() => Console.WriteLine("middleware before next"), () => Console.WriteLine("Logging işleme başladı"));

      // normal sekron foreach paralel proccessing sample
      Parallel.For(0, 5, i =>
      {
        Console.WriteLine($"Logging işleme devam ediyor... {i}");
      });




      try
      {
        await next(context);
      }
      catch (Exception ex)
      {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { Message = "Uygulamada beklenmedik bir hata meydana geldi" });
      }
    }
  }
}
