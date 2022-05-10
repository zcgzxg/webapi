using Microsoft.AspNetCore.Authorization;

using System.Text.Json;

using WebApi.Models;

namespace WebApi.Authorization
{
    /// <summary>
    /// AtLeastUserIDHandler
    /// </summary>
    public class AtLeastUserIDHandler : AuthorizationHandler<AtLeastUserIDRequirement>
    {
        private readonly IToken _token;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AtLeastUserIDHandler(IToken token)
        {
            _token = token;
        }

        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AtLeastUserIDRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == JWTTokenConfig.ClaimType && _token.Payload.User.ID >= requirement.UserID))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}