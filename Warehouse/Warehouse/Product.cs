namespace Warehouse
{
    public class Product
    {
        public Product(string product, int amount)
        {
            ProductName = product;
            Amount = amount;
        }

        public string ProductName { get; private set; }
        public int Amount { get; set; }
    }
}