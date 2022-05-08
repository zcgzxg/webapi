using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace webapi.Base
{
    /// <summary>
    /// 返回给前端的错误代码
    /// </summary>
    [JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public enum ErrorCodes
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 出现错误
        /// </summary>
        Error = 2
    }
}