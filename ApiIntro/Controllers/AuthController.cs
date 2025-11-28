using ApiIntro.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiIntro.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {

    public class AccessTokenRequest
    {
      public string? Username { get; set; }
      public string? Password { get; set; }
    }

    public class AccessTokenResponse
    {
      public string? AccessToken { get; set; }
    }


    [HttpPost("token")]
    public async Task<IActionResult> GenerateTokenAsync([FromBody] AccessTokenRequest request)
    {
      // Token üretimi yapacağımız kod.

      if(request.Username == "testuser" && request.Password == "P@ssword1")
      {
        // Claim nesnesi token içerisinde saklanacak olan bilgileri temsil eder

        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, "testuser"));
        claims.Add(new Claim(ClaimTypes.Role, "admin"));
        claims.Add(new Claim(ClaimTypes.Role, "manager"));


        var identity = new ClaimsIdentity(claims);

        // JWT Bearer paketinden token üretimi için gerekli servisleri kullanıyoruz
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
          Subject = identity,
          Expires = DateTime.UtcNow.AddMinutes(30),
          SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JwtKeys.SharedKey)),
            SecurityAlgorithms.HmacSha512Signature
          ),
          Audience = "react client app",
          Issuer = "net core web api"
        });

            var AccessToken = tokenHandler.WriteToken(token);
            return Ok(new AccessTokenResponse { AccessToken = AccessToken });
      }
      else
      {
        return BadRequest("Invalid User Account");
      }
        
    }


  }
}
