namespace WebApi.Models
{
    /// <summary>
    /// Authentication中间件根据Token决定是否需要重新生成新的Token,在TokenPayload改变时设置NeedRefresh=true可重新生成Token
    /// </summary>
    public class TokenPayload
    {
        /// <summary>
        /// 用户信息 Token
        /// </summary>
        public User User { get; set; } = new User();
    }
}