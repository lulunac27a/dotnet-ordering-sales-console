public class Order {
    public required int OrderId {get; set;}
    public required int CustomerId {get; set;}
    public required List<Product> Products {get; set;}
    public required int Quantity {get; set;}
}