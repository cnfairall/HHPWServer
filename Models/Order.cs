namespace HHPWServer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustName { get; set; }
        public string CustEmail { get; set; }
        public string PhoneNum { get; set; }
        public int OrderTypeId { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}
