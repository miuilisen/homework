using System;
using System.Collections.Generic;
using System.Linq;

// 订单类
public class Order : IEquatable<Order>
{
    public string OrderNumber { get; set; }
    public string Customer { get; set; }
    public List<OrderDetails> OrderDetails { get; set; }

    public decimal TotalAmount => OrderDetails.Sum(od => od.Amount);

    public Order(string orderNumber, string customer)
    {
        OrderNumber = orderNumber;
        Customer = customer;
        OrderDetails = new List<OrderDetails>();
    }

    public void AddOrderDetails(OrderDetails orderDetails)
    {
        if (!OrderDetails.Contains(orderDetails))
        {
            OrderDetails.Add(orderDetails);
        }
        else
        {
            throw new ArgumentException("Order details already exist.");
        }
    }

    public void RemoveOrderDetails(OrderDetails orderDetails)
    {
        if (OrderDetails.Contains(orderDetails))
        {
            OrderDetails.Remove(orderDetails);
        }
        else
        {
            throw new ArgumentException("Order details not found.");
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals(obj as Order);
    }

    public bool Equals(Order other)
    {
        return other != null && OrderNumber == other.OrderNumber;
    }

    public override int GetHashCode()
    {
        return OrderNumber.GetHashCode();
    }

    public override string ToString()
    {
        return $"Order Number: {OrderNumber}, Customer: {Customer}, Total Amount: {TotalAmount}";
    }
}

// 订单明细类
public class OrderDetails : IEquatable<OrderDetails>
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Amount => Price * Quantity;

    public OrderDetails(string productName, decimal price, int quantity)
    {
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals(obj as OrderDetails);
    }

    public bool Equals(OrderDetails other)
    {
        return other != null && ProductName == other.ProductName;
    }

    public override int GetHashCode()
    {
        return ProductName.GetHashCode();
    }

    public override string ToString()
    {
        return $"Product Name: {ProductName}, Price: {Price}, Quantity: {Quantity}, Amount: {Amount}";
    }
}

// 订单服务类
public class OrderService
{
    private List<Order> orders;

    public OrderService()
    {
        orders = new List<Order>();
    }

    public void AddOrder(Order order)
    {
        if (!orders.Contains(order))
        {
            orders.Add(order);
        }
        else
        {
            throw new ArgumentException("Order already exists.");
        }
    }

    public void RemoveOrder(Order order)
    {
        if (orders.Contains(order))
        {
            orders.Remove(order);
        }
        else
        {
            throw new ArgumentException("Order not found.");
        }
    }

    public void ModifyOrder(Order order)
    {
        int index = orders.FindIndex(o => o.Equals(order));

        if (index >= 0)
        {
            orders[index] = order;
        }
        else
        {
            throw new ArgumentException("Order not found.");
        }
    }

    public Order GetOrderByOrderNumber(string orderNumber)
    {
        return orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
    }

    public List<Order> QueryOrdersByCustomer(string customer)
    {
        return orders.Where(o => o.Customer == customer).OrderBy(o => o.TotalAmount).ToList();
    }

    public List<Order> QueryOrdersByProductName(string productName)
    {
        return orders.Where(o => o.OrderDetails.Any(od => od.ProductName == productName)).OrderBy(o => o.TotalAmount).ToList();
    }

    public List<Order> QueryOrdersByAmount(decimal amount)
    {
        return orders.Where(o => o.TotalAmount == amount).OrderBy(o => o.TotalAmount).ToList();
    }

    public void SortOrdersByOrderNumber()
    {
        orders = orders.OrderBy(o => o.OrderNumber).ToList();
    }

    public void SortOrdersByTotalAmount()
    {
        orders = orders.OrderBy(o => o.TotalAmount).ToList();
    }

    public void DisplayAllOrders()
    {
        foreach (Order order in orders)
        {
            Console.WriteLine(order);
            foreach (OrderDetails orderDetails in order.OrderDetails)
            {
                Console.WriteLine(orderDetails);
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 创建订单服务实例
        OrderService orderService = new OrderService();

        // 添加测试订单数据
        Order order1 = new Order("001", "Customer1");
        order1.AddOrderDetails(new OrderDetails("Product1", 10, 2));
        order1.AddOrderDetails(new OrderDetails("Product2", 20, 1));
        orderService.AddOrder(order1);

        Order order2 = new Order("002", "Customer2");
        order2.AddOrderDetails(new OrderDetails("Product1", 10, 3));
        orderService.AddOrder(order2);

        // 显示所有订单
        orderService.DisplayAllOrders();

        // 按订单号查询订单
        Order queriedOrder = orderService.GetOrderByOrderNumber("001");
        if (queriedOrder != null)
        {
            Console.WriteLine("Queried Order: " + queriedOrder);
        }
        else
        {
            Console.WriteLine("Order not found.");
        }

        // 按客户查询订单
        List<Order> queriedOrdersByCustomer = orderService.QueryOrdersByCustomer("Customer2");
        if (queriedOrdersByCustomer.Count > 0)
        {
            Console.WriteLine("Queried Orders by Customer: ");
            foreach (Order order in queriedOrdersByCustomer)
            {
                Console.WriteLine(order);
            }
        }
        else
        {
            Console.WriteLine("No orders found for the customer.");
        }

        // 按商品名称查询订单
        List<Order> queriedOrdersByProductName = orderService.QueryOrdersByProductName("Product1");
        if (queriedOrdersByProductName.Count > 0)
        {
            Console.WriteLine("Queried Orders by Product Name: ");
            foreach (Order order in queriedOrdersByProductName)
            {
                Console.WriteLine(order);
            }
        }
        else
        {
            Console.WriteLine("No orders found for the product.");
        }

        // 按订单金额查询订单
        List<Order> queriedOrdersByAmount = orderService.QueryOrdersByAmount(30);
        if (queriedOrdersByAmount.Count > 0)
        {
            Console.WriteLine("Queried Orders by Amount: ");
            foreach (Order order in queriedOrdersByAmount)
            {
                Console.WriteLine(order);
            }
        }
        else
        {
            Console.WriteLine("No orders found for the amount.");
        }
    }
}
