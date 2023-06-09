using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop
{
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

}
