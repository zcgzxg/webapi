using Microsoft.AspNetCore.Authorization;

using System.Text.Json;

using WebApi.Models;

namespace WebApi.Authorization
{
    /// <summary>
    /// AtLeastUserIDRequirement
    /// </summary>
    public class AtLeastUserIDRequirement : IAuthorizationRequirement, IAuthorizationHandler
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public uint UserID { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public AtLeastUserIDRequirement(uint userID)
        {
            UserID = userID;
        }

        /// <summary>
        /// Handle
        /// </summary>
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (context.User.HasClaim(c => c.Type == "User" && JsonSerializer.Deserialize<TokenPayload>(c.Value)?.User.ID >= UserID))
            {
                context.Succeed(this);
            }
            return Task.CompletedTask;
        }
    }
}