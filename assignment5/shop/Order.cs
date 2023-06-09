using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop
{
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

}
