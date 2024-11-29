internal class Program
{
    private static void Main(string[] args)
    {
        List<Product> products = new List<Product>(); //list of products
        List<Customer> customers = new List<Customer>(); //list of customers
        List<Order> orders = new List<Order>(); //list of orders
        int productCounter = 1; //product ID counter
        int customerCounter = 1; //customer ID counter
        int orderCounter = 1; //order ID counter
        while (true)
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Add Customer");
            Console.WriteLine("3. Add Order");
            Console.WriteLine("4. View Products");
            Console.WriteLine("5. View Customers");
            Console.WriteLine("6. View Orders");
            if (!int.TryParse(Console.ReadLine(), out int option))
            { //input option value
                Console.WriteLine("Invalid input. Please enter a valid number.");
                continue;
            }
            switch (option)
            {
                case 1: //add product
                    Console.WriteLine("Enter product name:");
                    string? productName = Console.ReadLine();
                    if (productName != null)
                    {
                        Console.WriteLine("Enter product price:");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal productPrice))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid product price.");
                            continue;
                        }
                        productPrice = decimal.Round(productPrice, 2); //round product price to 2 decimal places
                        Console.WriteLine("Enter product quantity:");
                        if (!int.TryParse(Console.ReadLine(), out int productQuantity))
                        {
                            Console.WriteLine(
                                "Invalid input. Please enter a valid product quantity."
                            );
                            continue;
                        }
                        products.Add(
                            new Product
                            {
                                ProductId = productCounter++,
                                ProductName = productName,
                                Price = productPrice,
                                Quantity = productQuantity,
                            }
                        ); //add new product with entered values
                    }
                    break;
                case 2: //add customer
                    Console.WriteLine("Enter customer name:");
                    string? customerName = Console.ReadLine();
                    if (customerName != null)
                    {
                        customers.Add(
                            new Customer
                            {
                                CustomerId = customerCounter++,
                                Name = customerName,
                                Orders = new List<Order>(),
                            }
                        ); //add new customer with entered values
                    }
                    break;
                case 3: //add order
                    List<Product> orderProducts = new List<Product>(); //initialize order products list
                    foreach (Customer customer in customers)
                    { //repeat for each customer in customer list
                        Console.WriteLine($"{customer.CustomerId}. {customer.Name}");
                    }
                    Console.WriteLine("Enter customer ID:");
                    if (!int.TryParse(Console.ReadLine(), out int customerId))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid customer ID.");
                        continue;
                    }
                    Console.WriteLine("Enter quantity:");
                    if (!int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid quantity.");
                        continue;
                    }
                    while (true)
                    { //repeat until product ID input is empty or blank
                        foreach (var product in products)
                        { //repeat for each product in product list
                            Console.WriteLine($"{product.ProductId}. {product.ProductName}");
                        }
                        Console.WriteLine("Enter product ID or press Enter to finish:");
                        string? enteredProductId = Console.ReadLine();
                        if (string.IsNullOrEmpty(enteredProductId))
                        { //if product ID input is empty or blank
                            break; //end the input process
                        }
                        if (!int.TryParse(enteredProductId, out int productId))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid product ID.");
                            continue;
                        }
                        var selectedProduct = products.FirstOrDefault(p =>
                            p.ProductId == productId
                        ); //match product ID with product ID in product list
                        if (selectedProduct != null)
                        { //if product ID is found in product list
                            orderProducts.Add(selectedProduct); //add new order product with selected product ID in product list
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
                    ); //add new order with entered values
                    break;
                case 4: //view products
                    decimal totalProductPrice = 0; //total price of all products
                    decimal individualProductPrice = 0; //price of individual product
                    foreach (Product product in products)
                    { //repeat for each product in product list
                        individualProductPrice = product.Price * product.Quantity;
                        Console.WriteLine(
                            $"Product: {product.ProductName}, Price: {product.Price:N2}, Quantity: {product.Quantity:N0}"
                        );
                        totalProductPrice += individualProductPrice; //add price of individual product to total product price
                    }
                    Console.WriteLine($"Total Price of All Products: {totalProductPrice:N2}");
                    break;
                case 5: //view customers
                    foreach (Customer customer in customers)
                    {
                        decimal totalCustomerPrice = 0; //total price of all customers
                        foreach (Order order in orders)
                        { //repeat for each order in order list
                            if (order.CustomerId == customer.CustomerId)
                            { //if order customer ID matches customer ID in customer list
                                foreach (Product product in order.Products)
                                { //repeat for each product in order list with order customer ID matches customer ID in customer list
                                    totalCustomerPrice += product.Price * product.Quantity; //add customer price to total price of all customers
                                }
                            }
                        }
                        Console.WriteLine(
                            $"Customer: {customer.Name}, Total Price: {totalCustomerPrice:N2}"
                        );
                    }
                    break;
                case 6: //view orders
                    decimal totalAllOrdersPrice = 0; //total price of all orders
                    foreach (Order order in orders)
                    { //repeat for each order in order list
                        Console.WriteLine($"Order: {order.OrderId}, Quantity: {order.Quantity:N0}");
                        decimal totalOrderPrice = 0; //total price of order
                        decimal orderProductPrice = 0; //price of order product
                        foreach (Product product in order.Products)
                        { //repeat for each product in order product list
                            orderProductPrice = product.Price * product.Quantity * order.Quantity;
                            Console.WriteLine(
                                $"- Product: {product.ProductName}, Price: {product.Price:N2}, Quantity: {product.Quantity:N0}, Product Price: {orderProductPrice:N2}"
                            );
                            totalOrderPrice += orderProductPrice; //add order product price to total order price
                        }
                        Console.WriteLine($"Total Price: {totalOrderPrice:N2}");
                        totalAllOrdersPrice += totalOrderPrice; //add total order price to total price of all orders
                    }
                    Console.WriteLine($"Total Price of All Orders: {totalAllOrdersPrice:N2}");
                    break;
                default: //invalid option
                    Console.WriteLine("Invalid option.");
                    return;
            }
        }
    }
}
