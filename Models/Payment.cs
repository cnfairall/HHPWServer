namespace HHPWServer.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal OrderTotal => Order.Subtotal + TipAmount;    
        public decimal TipAmount { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
