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
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorCodes
    {
        /// <summary>
        /// 成功
        /// </summary>
        [EnumMember(Value = "000001")]
        [Display(Name = "000001")]
        [Description("000001")]
        Success = 1,
        /// <summary>
        /// 出现错误
        /// </summary>
        [EnumMember(Value = "000002")]
        [Display(Name = "000002")]
        [Description("000002")]
        Error = 2
    }
}