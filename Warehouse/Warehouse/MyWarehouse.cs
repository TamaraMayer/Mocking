using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Warehouse
{
    public class MyWarehouse : IWarehouse
    {
        public List<Product> products { get; set; }

        public MyWarehouse()
        {
            this.products = new List<Product>();
        }

        public void AddStock(string product, int amount)
        {
            if (string.IsNullOrWhiteSpace(product))
            {
                throw new ArgumentNullException("Product name must not be null nor empty.");
            }

            Product p = products.First((p) => p.ProductName == product);
            p.Amount += amount;
        }

        public int CurrentStock(string product)
        {
            if (string.IsNullOrWhiteSpace(product))
            {
                throw new ArgumentNullException("Product name must not be null nor empty.");
            }

            if (!HasProduct(product))
            {
                throw new NoSuchProductException();
            }

            Product p = products.First((p) => p.ProductName == product);

            return p.Amount;
        }

        public bool HasProduct(string product)
        {
            if (string.IsNullOrWhiteSpace(product))
            {
                throw new ArgumentNullException("Product name must not be null nor empty.");
            }

            Product p = products.FirstOrDefault((p) => p.ProductName == product);

            if (p == null)
            {
                return false;
            }

            return true;
        }

        public void TakeStock(string product, int amount)
        {
            if (string.IsNullOrWhiteSpace(product))
            {
                throw new ArgumentNullException("Product name must not be null nor empty.");
            }

            if (!HasProduct(product))
            {
                throw new NoSuchProductException();
            }

            Product p = products.First((p) => p.ProductName == product);

            if (p.Amount < amount)
            {
                throw new InsufficientStockException();
            }

            p.Amount -= amount;
        }
    }

    [Serializable]
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException()
        {
        }

        public InsufficientStockException(string message) : base(message)
        {
        }

        public InsufficientStockException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class NoSuchProductException : Exception
    {
        public NoSuchProductException()
        {
        }

        public NoSuchProductException(string message) : base(message)
        {
        }

        public NoSuchProductException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuchProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
