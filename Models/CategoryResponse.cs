namespace webapi.Models
{
    /// <summary>
    /// CategoryResponse Model
    /// </summary>
    public class CategoryResponse : Category
    {
        /// <summary>
        /// 分类下的商品
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();
    }
}