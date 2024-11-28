public class Customer
{
    public required int CustomerId { get; set; } //customer ID
    public required string Name { get; set; } //customer name
    public required List<Order> Orders { get; set; } //list of orders in customer
}
