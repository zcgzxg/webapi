using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    /// <summary>
    /// BaseCategory Model
    /// </summary>
    public class BaseCategory
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
    }
}