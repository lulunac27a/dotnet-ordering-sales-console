public class Customer
{
    public required int CustomerId { get; set; }
    public required string Name { get; set; }
    public required List<Order> Orders { get; set; }
}
