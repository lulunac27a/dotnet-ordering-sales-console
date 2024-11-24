public class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public List<Product> Products { get; set; }
    public int Quantity { get; set; }
}