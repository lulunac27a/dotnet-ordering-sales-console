public class Order
{
    public required int OrderId { get; set; } //order ID
    public required int CustomerId { get; set; } //customer ID
    public required List<Product> Products { get; set; } //list of products in the order
    public required int Quantity { get; set; } //order quantity
}
