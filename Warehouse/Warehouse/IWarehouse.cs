using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public interface IWarehouse
    {
        bool HasProduct(string product);
        int CurrentStock(string product);
        void AddStock(string product, int amount);
        void TakeStock(string procut, int amount);
    }
}
