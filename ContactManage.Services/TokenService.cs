using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
public class TokenService : ITokenService 
{
    private readonly IConfiguration _configuration; 
    public TokenService(IConfiguration configuration) 
    {
        _configuration = configuration; 
    }
    public string CreateToken(IdentityUser user) 
    { 
        var secretKey = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]); 
        var tokenHandler = new JwtSecurityTokenHandler(); 
        int expiryHours = 1; 
        int.TryParse(_configuration["JwtSettings:ExpiryInHours"], out expiryHours); 
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email), 
        };
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expiryHours), 
            Issuer = _configuration["JwtSettings:Issuer"], 
            Audience = _configuration["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
            SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token); } 
}