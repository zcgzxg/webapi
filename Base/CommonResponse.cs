

namespace webapi.Base
{
    /// <summary>
    /// 返回给前台的公共数据定义
    /// </summary>
    /// <typeparam name="TData">返回的Data数据类型</typeparam>
    public class CommonResponse<TData>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public TData? Data { get; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string? Description { get; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public ErrorCodes Type { get; }

        /// <summary>
        /// 初始化CommonActionResultData
        /// </summary>
        public CommonResponse(TData data, ErrorCodes type = ErrorCodes.Success, string description = "")
        {
            Description = description;
            Data = data;
            Type = type;
        }
    }
}