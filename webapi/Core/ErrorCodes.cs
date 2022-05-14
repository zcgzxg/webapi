using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApi.Base
{
    /// <summary>
    /// 返回给前端的错误代码
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ErrorCodes
    {
        /// <summary>
        /// 成功
        /// </summary>
        [EnumMember(Value = "000001")]
        Success = 1,
        /// <summary>
        /// 出现错误
        /// </summary>
        [EnumMember(Value = "000002")]
        Error = 2
    }
}