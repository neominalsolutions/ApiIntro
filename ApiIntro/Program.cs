using ApiIntro.Application.Products.RequestHandlers;
using ApiIntro.Domain.Repositories;
using ApiIntro.Domain.Services;
using ApiIntro.Infra.EF.Context;
using ApiIntro.Infra.EF.Repository;
using ApiIntro.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ProductCreateRequestHandler>();
builder.Services.AddScoped<ProductGetByIdRequestHandler>();

// DBContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// JWT Token Validation Service

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
  opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
  {
    ValidateLifetime = true,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JwtKeys.SharedKey)),
    ValidIssuer = "net core web api",
    ValidAudience = "react client app"
  };

  opt.Events = new JwtBearerEvents()
  {
    OnAuthenticationFailed = async (context) =>
    {
      // kiþi token göndermez ise veya token doðru formatta deðilse valid deðilse buraya düþeriz.
      Console.WriteLine(context.Exception.Message);
    },
    OnForbidden = async (context) =>
    {
      // eðer token doðru ama yetksi yoksa bu event çalýþýr
      Console.WriteLine($"forbiden {context.Result}");
    },
    OnTokenValidated = async (context) =>
    {
      // Token valid olan kullanýcý hesap bilgisini ekrana yazdýrdýk.
      Console.WriteLine($"token validated {context.Principal.Identity.Name}");
    }
  };

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
