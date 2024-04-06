namespace HHPWServer.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }
        public int PaymentTypeId { get; set; }
        public virtual decimal OrderTotal
            { get
                {
                    return Order.Items.Sum(x => x.ItemPrice) + TipAmount;
                }
            }
        public decimal TipAmount { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
