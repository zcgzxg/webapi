using WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Authentication
{
    /// <summary>
    /// 从JWT中获取用户信息,将用户信息写入JWT中
    /// </summary>
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 构造函数
        /// </summary>
        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        public async Task InvokeAsync(HttpContext context, IToken token)
        {
            context.Response.OnStarting(() =>
            {
                if (token.NeedRefresh)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(JWTTokenConfig.ClaimType, System.Text.Json.JsonSerializer.Serialize(token.Payload))
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var jwtToken = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["Jwt:Expires"])),
                        signingCredentials: signIn);
                    context.Response.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwtToken));
                }
                return Task.CompletedTask;
            });

            TokenPayload? payload = null;
            if (context.User.HasClaim(c => c.Type == JWTTokenConfig.ClaimType && ((payload = System.Text.Json.JsonSerializer.Deserialize<TokenPayload>(c.Value)) is not null)))
            {
                token.Payload = payload!;
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
        public static IApplicationBuilder UseJWTMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JWTMiddleware>();
        }
    }
}
