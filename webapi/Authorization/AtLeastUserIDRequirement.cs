using Microsoft.AspNetCore.Authorization;

namespace WebApi.Authorization
{
    /// <summary>
    /// AtLeastUserIDRequirement
    /// </summary>
    public class AtLeastUserIDRequirement : IAuthorizationRequirement
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
    }
}