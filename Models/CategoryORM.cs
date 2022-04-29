namespace webapi.Models
{
    /// <summary>
    /// Category Model
    /// </summary>
    public class CategoryORM : BaseCategory
    {
        /// <summary>
        /// 分类下的商品
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();
    }
}