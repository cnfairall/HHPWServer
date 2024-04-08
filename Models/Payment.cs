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
                    var itemPrices = Order.Items.Select(i => i.Item.ItemPrice).ToList();
                    var total = itemPrices.Sum() + TipAmount;
                    return total;
                }
            }
        public decimal TipAmount { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
