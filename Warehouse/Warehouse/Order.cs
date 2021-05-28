using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Warehouse
{
    public class Order
    {
        private string product;
        private int amount;

        public Order(string product, int amount)
        {
            if (string.IsNullOrWhiteSpace(product))
            {
                throw new ArgumentNullException("Product name must not be null nor empty.");
            }

            if (amount < 1)
            {
                throw new ArgumentOutOfRangeException("Amount must not be smaller than 1");
            }

            this.product = product;
            this.amount = amount;
            this.IsFilled = false;
        }

        public bool IsFilled { get; set; }
        

        public bool CanFillOrder(IWarehouse warehouse)
        {
            if (warehouse.HasProduct(product))
            {
                int stock = warehouse.CurrentStock(product);

                if(stock >= amount)
                {
                    return true;
                }
            }

            return false;
        }

        public void Fill(IWarehouse warehouse)
        {
            if (this.IsFilled)
            {
                throw new OrderAlreadyFilled("This order has already been filled");
            }

            warehouse.TakeStock(product, amount);
            this.IsFilled = true;

        }
    }

    [Serializable]
    public class OrderAlreadyFilled : Exception
    {
        public OrderAlreadyFilled()
        {
        }

        public OrderAlreadyFilled(string message) : base(message)
        {
        }

        public OrderAlreadyFilled(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderAlreadyFilled(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
