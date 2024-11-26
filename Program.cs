internal class Program
{
    private static void Main(string[] args)
    {
        var products = new List<Product>();
        var customers = new List<Customer>();
        var orders = new List<Order>();
        int productCounter = 1;
        int customerCounter = 1;
        int orderCounter = 1;
        while (true)
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Add Customer");
            Console.WriteLine("3. Add Order");
            Console.WriteLine("4. View Products");
            Console.WriteLine("5. View Customers");
            Console.WriteLine("6. View Orders");
            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter product name:");
                    string productName = Console.ReadLine();
                    Console.WriteLine("Enter product price:");
                    decimal productPrice = decimal.Parse(Console.ReadLine());
                    productPrice = decimal.Round(productPrice, 2);
                    Console.WriteLine("Enter product quantity:");
                    int productQuantity = int.Parse(Console.ReadLine());
                    products.Add(
                        new Product
                        {
                            ProductId = productCounter++,
                            ProductName = productName,
                            Price = productPrice,
                            Quantity = productQuantity,
                        }
                    );
                    break;
                case 2:
                    Console.WriteLine("Enter customer name:");
                    string customerName = Console.ReadLine();
                    customers.Add(
                        new Customer { CustomerId = customerCounter++, Name = customerName }
                    );
                    break;
                case 3:
                    List<Product> orderProducts = new List<Product>();
                    Console.WriteLine("Enter customer ID:");
                    int customerId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter quantity:");
                    int quantity = int.Parse(Console.ReadLine());
                    while (true)
                    {
                        foreach (var product in products)
                        {
                            Console.WriteLine($"{product.ProductId}. {product.ProductName}");
                        }
                        Console.WriteLine("Enter product ID or press Enter to finish:");
                        string enteredProductId = Console.ReadLine();
                        if (string.IsNullOrEmpty(enteredProductId))
                        {
                            break;
                        }
                        int productId = int.Parse(enteredProductId);
                        var selectedProduct = products.FirstOrDefault(p =>
                            p.ProductId == productId
                        );
                        if (selectedProduct != null)
                        {
                            orderProducts.Add(selectedProduct);
                        }
                    }
                    orders.Add(
                        new Order
                        {
                            OrderId = orderCounter++,
                            CustomerId = customerId,
                            Products = orderProducts,
                            Quantity = quantity,
                        }
                    );
                    break;
                case 4:
                    decimal totalPrice = 0;
                    decimal totalProductPrice = 0;
                    foreach (Product product in products)
                    {
                        totalProductPrice = product.Price * product.Quantity;
                        Console.WriteLine(
                            $"Product: {product.ProductName}, Price: {product.Price:F2}, Quantity: {product.Quantity:N0}"
                        );
                        totalPrice += totalProductPrice;
                    }
                    Console.WriteLine("Total Price: {totalPrice:F2}");
                    break;
                case 5:
                    foreach (Customer customer in customers)
                    {
                        decimal totalCustomerPrice = 0;
                        foreach (Order order in orders)
                        {
                            if (order.CustomerId == customer.CustomerId)
                            {
                                foreach (Product product in order.Products)
                                {
                                    totalCustomerPrice += product.Price * product.Quantity;
                                }
                            }
                        }
                        Console.WriteLine(
                            $"Customer: {customer.Name}, Total Price: {totalCustomerPrice:F2}"
                        );
                    }
                    break;
                case 6:
                    foreach (Order order in orders)
                    {
                        Console.WriteLine($"Order: {order.OrderId}, Quantity: {order.Quantity:N0}");
                        decimal totalOrderPrice = 0;
                        decimal orderProductPrice = 0;
                        foreach (Product product in order.Products)
                        {
                            orderProductPrice = product.Price * product.Quantity * order.Quantity;
                            Console.WriteLine(
                                $"- Product: {product.ProductName}, Price: {product.Price:F2}, Quantity: {product.Quantity:N0}, Product Price: {orderProductPrice:F2}"
                            );
                            totalOrderPrice += orderProductPrice;
                        }
                        Console.WriteLine($"Total Price: {totalOrderPrice:F2}");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }
        }
    }
}
