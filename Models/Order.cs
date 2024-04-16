namespace HHPWServer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustName { get; set; }
        public string CustEmail { get; set; }
        public string PhoneNum { get; set; }
        public int OrderTypeId { get; set; }
        public decimal Subtotal => Items.Select(i => i.Item.ItemPrice).ToList().Sum();
        public bool IsClosed { get; set; }
        public Payment Payment { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    }
}
