using Microsoft.IdentityModel.Tokens;
using OpenTodo.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
namespace OpenTodo.Auth
{
    public class Auth(/*IConfiguration configuration*/)
    {
        //private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(UserSchema user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dadsadqweqeqewfrredwcsg42r 34r5t44r5tg4rfeds23r4rf23edsxacg342rf"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7167",
                audience: "http://localhost:5173", //_configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public UserSchema GetUserContext(HttpContext context)
        {
            var user = new UserSchema();
            var token = context.Request.Cookies["Authorization"];
            if(string.IsNullOrEmpty(token)) return user;   
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;           
            if (userId is null) return user;
            user.Id = int.Parse(userId);
            user.FirstName = context.User.FindFirstValue(ClaimTypes.Name) ?? "";
            return user;
        }

        public string GenerateHash(string password) =>  BCrypt.Net.BCrypt.HashPassword(password, 10);

        public bool PasswordCompare(string hash, string password) => BCrypt.Net.BCrypt.Verify(password, hash);

        public string GenerateJwtToken(UserSchema user)
        {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("dadsadqweqeqewfrredwcsg42r 34r5t44r5tg4rfeds23r4rf23edsxacg342rf");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "")
            }),
            Expires = DateTime.UtcNow.AddDays(30),
            Issuer = "https://localhost:7167",
            Audience = "http://localhost:5173",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
        }
    }
}
