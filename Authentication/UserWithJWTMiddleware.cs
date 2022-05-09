using webapi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace webapi.Authentication
{
    /// <summary>
    /// 从JWT中获取用户信息,将用户信息写入JWT中
    /// </summary>
    public class UserFromJWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserFromJWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        public async Task InvokeAsync(HttpContext context, User user)
        {
            context.Response.OnStarting(() =>
            {
                if (user.ID > 0)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("User", System.Text.Json.JsonSerializer.Serialize(user))
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["Jwt:Expires"])),
                        signingCredentials: signIn);
                    context.Response.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));
                }
                return Task.CompletedTask;
            });

            foreach (var claim in context.User.Claims)
            {
                if (claim.Type == "User")
                {
                    var _user = System.Text.Json.JsonSerializer.Deserialize<User>(claim.Value);
                    if (_user is not null)
                    {
                        user.ID = _user.ID;
                        user.Name = _user.Name;
                    }
                }
            }
            await _next(context);
        }
    }

    /// <summary>
    /// 注册UserWithJWTMiddleware
    /// </summary>
    public static class UserFromJWTMiddlewareExtensions
    {
        /// <summary>
        /// 注册UserWithJWTMiddleware
        /// </summary>
        public static IApplicationBuilder UseUserWithJWTMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UserFromJWTMiddleware>();
        }
    }
}
