using System.ComponentModel.DataAnnotations;
using DapperExtensions;

namespace webapi.Models
{
    /// <summary>
    /// Category Model
    /// </summary>
    public class Category
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        [Key]
        public UInt32 CategoryId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [StringLength(48, MinimumLength = 1)]
        public string CategoryName { get; set; } = "";
        /// <summary>
        /// 分类下的商品
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();
    }
}