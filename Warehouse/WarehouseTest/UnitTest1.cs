using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Warehouse;

namespace WarehouseTest
{
    [TestClass]
    public class UnitTest1
    {
        public MyWarehouse warehouse;

        [TestInitialize]
        public void Setup()
        {
            warehouse = new MyWarehouse();
            warehouse.products.Add(new Product("A", 5));
            warehouse.products.Add(new Product("B", 50));
        }

        [TestMethod]
        public void HasProduct_Should_Throw_Exception_Because_Of_Invalid_Product()
        {
            Assert.ThrowsException<ArgumentNullException>(() => warehouse.HasProduct(""));
        }

        [TestMethod]
        public void CurrentStock_Should_Throw_Exception_Because_Of_Invalid_Product()
        {
            Assert.ThrowsException<ArgumentNullException>(() => warehouse.CurrentStock(""));
        }

        [TestMethod]
        public void AddStock_Should_Throw_Exception_Because_Of_Invalid_Product()
        {
            Assert.ThrowsException<ArgumentNullException>(() => warehouse.AddStock("",0));
        }

        [TestMethod]
        public void TakeStock_Should_Throw_Exception_Because_Of_Invalid_Product()
        {
            Assert.ThrowsException<ArgumentNullException>(() => warehouse.TakeStock("",0));
        }

        [DataTestMethod]
        [DataRow("Z")]
        [DataRow("Nicht vorhanden")]
        public void CurrentStock_Should_Throw_NoSuchProductException(string product)
        {
            Assert.ThrowsException<NoSuchProductException>(() => warehouse.CurrentStock(product));
        }

        [DataTestMethod]
        [DataRow("Y")]
        [DataRow("Nicht vorhanden")]
        public void TakeStock_Should_Throw_NoSuchProductException(string product)
        {
            Assert.ThrowsException<NoSuchProductException>(() => warehouse.TakeStock(product,0));
        }

        [DataTestMethod]
        [DataRow("A",10)]
        [DataRow("B",51)]
        public void TakeStock_Should_Throw_InsuffiecentStockException(string product, int amount)
        {
            Assert.ThrowsException<InsufficientStockException>(() => warehouse.TakeStock(product, amount));
        }

        [DataTestMethod]
        [DataRow("A", 5)]
        [DataRow("B", 10)]
        public void Fill_Should_Throw_OrderAlreadyFilledException_When_Fill_Is_Called_Multiple_Times(string product, int amount)
        {
            Order order = new Order(product, amount);

            order.Fill(warehouse);

            Assert.ThrowsException<OrderAlreadyFilled>(() => order.Fill(warehouse));
        }

        [TestMethod]
        public void Constructor_Should_Throw_Exception_Because_Of_Invalid_Product()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Order("",0));
        }

        [TestMethod]
        public void Constructor_Should_Throw_Exception_Because_Of_Invalid_Amount()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Order("P", 0));
        }

        [DataTestMethod]
        [DataRow("A", 5)]
        [DataRow("B", 50)]
        public void CurrentStock_Should_Return_Current_Stock(string product, int expectedNewAmount)
        {
            Assert.AreEqual(expectedNewAmount, warehouse.CurrentStock(product));
        }

        [DataTestMethod]
        [DataRow ("A",5,10)]
        [DataRow ("B",5,55)]
        public void AddStock_Should_Add_Stock_To_A_Product(string product, int amount, int expectedNewAmount)
        {
            warehouse.AddStock(product, amount);

            Assert.AreEqual(expectedNewAmount, warehouse.CurrentStock(product));
        }

        [DataTestMethod]
        [DataRow("A", 5, 0)]
        [DataRow("B", 5, 45)]
        public void TakeStock_Should_Take_Stock_From_A_Product(string product, int amount, int expectedNewAmount)
        {
            warehouse.TakeStock(product, amount);

            Assert.AreEqual(expectedNewAmount, warehouse.CurrentStock(product));
        }

        [DataTestMethod]
        [DataRow ("A",5,0)]
        [DataRow("B", 40, 10)]
        public void Fill_Should_Take_Stock_From_The_Warehouse_And_Set_Filled_To_True(string product, int amount, int expectedNewAmount)
        {
            Order a = new Order(product, amount);

            a.Fill(warehouse);

            Assert.AreEqual(expectedNewAmount, warehouse.CurrentStock(product));

            Assert.IsTrue(a.IsFilled);
        }

        [DataTestMethod]
        [DataRow("A", 5)]
        [DataRow("B", 40)]
        public void CanFillOrder_Should_Return_True_When_Order_Be_Filled(string product, int amount)
        {
            Order a = new Order(product, amount);

            Assert.IsTrue(a.CanFillOrder(warehouse));
        }

        [DataTestMethod]
        [DataRow("A", 10)]
        [DataRow("B", 55)]
        public void CanFillOrder_Should_Return_False_When_Order_Be_Filled(string product, int amount)
        {
            Order a = new Order(product, amount);

            Assert.IsFalse(a.CanFillOrder(warehouse));
        }
    }
}
