namespace webapi.Models
{
    public class Product
    {
        public UInt32 ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public UInt32 CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}