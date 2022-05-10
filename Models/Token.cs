namespace WebApi.Models
{
    /// <summary>
    /// Token
    /// </summary>
    /// <remarks>
    /// <paragraph>
    /// Authentication中间件根据Token决定是否需要重新生成新的Token,在TokenPayload改变时设置NeedRefresh=true可重新生成Token
    /// </paragraph>
    /// <paragraph>
    /// TokenPayload中的数据自动从JWT Token中获取
    /// </paragraph>
    /// </remarks>
    public interface IToken
    {
        /// <summary>
        /// Claim Type
        /// </summary>
        /// <seealso cref="System.Security.Claims.Claim"/>
        public string ClaimType { get; }

        /// <summary>
        /// 指示是否需要根据Token对象重新生成JWT Token
        /// </summary>
        public bool NeedRefresh { get; set; }

        /// <summary>
        /// 要写入JWT Token的信息
        /// </summary>
        public TokenPayload Payload { get; set; }
    }

#pragma warning disable CS1591
    public class Token : IToken
    {
        public bool NeedRefresh { get; set; } = false;
        public TokenPayload Payload { get; set; } = new TokenPayload();
        public string ClaimType => "TokenPayload";
    }

    /// <summary>
    /// TokenExtensions
    /// </summary>
    public static class TokenExtensions
    {
        /// <summary>
        /// 注入Token
        /// </summary>
        public static void AddToken(this IServiceCollection service)
        {
            service.AddScoped<IToken, Token>();
        }
    }
}