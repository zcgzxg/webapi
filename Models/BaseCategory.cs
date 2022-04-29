using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace webapi.Models
{
    /// <summary>
    /// BaseCategory Model
    /// </summary>
    [Table("categories")]
    public class BaseCategory
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        [Dapper.Contrib.Extensions.Key]
        public UInt32 CategoryId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [StringLength(48, MinimumLength = 1)]
        public string CategoryName { get; set; } = "";
    }
}