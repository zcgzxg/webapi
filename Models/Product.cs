using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{

    /// <summary>
    /// Product Model
    /// </summary>{
    public class Product
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public UInt32 ProductId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [StringLength(48, MinimumLength = 1)]
        public string ProductName { get; set; } = "";
        /// <summary>
        /// 商品分类ID
        /// </summary>
        public UInt32 CategoryId { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        [JsonIgnore]
        public CategoryResponse? Category { get; set; }
    }
}