namespace HHPWServer.Models
{
    public class Item
    {
        public int Id {  get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public List<OrderItem> Orders { get; set; } = new List<OrderItem>();
    }
}
