using ApiIntro.Application.Products.RequestHandlers;
using ApiIntro.Domain.Repositories;
using ApiIntro.Domain.Services;
using ApiIntro.Infra.EF.Context;
using ApiIntro.Infra.EF.Repository;
using ApiIntro.Middleewares;
using ApiIntro.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// swagerda login ol artýk her istek otomatik token gönder
builder.Services.AddSwaggerGen(opt =>
{

  var securityScheme = new OpenApiSecurityScheme()
  {
    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT" // Optional
  };

  var securityRequirement = new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "bearerAuth"
            }
        },
        new string[] {}
    }
};

  opt.AddSecurityDefinition("bearerAuth", securityScheme);
  opt.AddSecurityRequirement(securityRequirement);
});




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

// Cors Settings- > React Client uygulamaya cors açma

builder.Services.AddCors(options =>
{
  options.AddPolicy("Cors",
      policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// custom middleware i DI konteynerýna ekleyelim
builder.Services.AddTransient<CustomErrorMiddleware>();

var app = builder.Build();

// Middleware yazdýðýmýz kod bloðu

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Cors");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// kendi middlewarelerimizi genelde burada konumlandýrýrýz.

// sistem bu middleware i her istekde tüketsin.
app.UseMiddleware<CustomErrorMiddleware>();

//app.Use(async (context, next) =>
//{

//  try
//  {
//    await next(); // hata olmadýðý takdirde kod akýþýna devam etsin. 
//  }
//  catch (Exception)
//  {

//    // mvcde de ayný mantýklý kullanýlýp genelde yada HTML response verilir yada redirect edilerek error page yönelndirilir.
//   context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//   await context.Response.WriteAsJsonAsync(new { Message = "Uygulamada beklenmedik bir hata meydana geldi" });
//  }

//});


app.Run();

// buradan sonraki kod bloðuna middleware yazamayýz çalýþmaz