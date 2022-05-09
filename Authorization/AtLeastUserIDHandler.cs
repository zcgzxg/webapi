using Microsoft.AspNetCore.Authorization;

using System.Text.Json;

using webapi.Models;

namespace webapi.Authorization
{
    /// <summary>
    /// AtLeastUserIDHandler
    /// </summary>
    public class AtLeastUserIDHandler : AuthorizationHandler<AtLeastUserIDRequirement>
    {
        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AtLeastUserIDRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "User" && JsonSerializer.Deserialize<User>(c.Value)?.ID >= requirement.UserID))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}