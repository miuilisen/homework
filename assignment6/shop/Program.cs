using System;
using System.Collections.Generic;
using System.Linq;
using shop;
class Program
{
    static void Main(string[] args)
    {
        // 创建订单服务实例
        OrderService orderService = new OrderService();

        Order order1 = new Order("001", "Customer1");
        order1.AddOrderDetails(new OrderDetail("Product1", 10, 2));
        order1.AddOrderDetails(new OrderDetail("Product2", 20, 1));
        orderService.AddOrder(order1);

        Order order2 = new Order("002", "Customer2");
        order2.AddOrderDetails(new OrderDetail("Product1", 10, 3));
        orderService.AddOrder(order2);

        // 显示所有订单
        orderService.DisplayAllOrders();

        // 按订单号查询订单
        Order ordertem = orderService.GetOrderByOrderNumber("001");
        if (ordertem != null)
        {
            Console.WriteLine("Queried Order: " + ordertem);
        }
        else
        {
            Console.WriteLine("Order not found.");
        }

        // 按客户查询订单
        List<Order> ordersByCustomer = orderService.QueryOrdersByCustomer("Customer2");
        if (ordersByCustomer.Count > 0)
        {
            Console.WriteLine("Queried Orders by Customer: ");
            foreach (Order order in ordersByCustomer)
            {
                Console.WriteLine(order);
            }
        }
        else
        {
            Console.WriteLine("No orders found for the customer.");
        }

        // 按商品名称查询订单
        List<Order> ordersByProductName = orderService.QueryOrdersByProductName("Product1");
        if (ordersByProductName.Count > 0)
        {
            Console.WriteLine("Queried Orders by Product Name: ");
            foreach (Order order in ordersByProductName)
            {
                Console.WriteLine(order);
            }
        }
        else
        {
            Console.WriteLine("No orders found for the product.");
        }
    }
}
