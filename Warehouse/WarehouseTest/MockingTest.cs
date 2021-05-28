using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Warehouse;

namespace WarehouseTest
{
    [TestClass]
    public class MockingTest
    {
        private Mock<IWarehouse> warehouseMoq;

        [TestInitialize]
        public void Setup()
        {
            warehouseMoq = new Mock<IWarehouse>();
        }

        [DataTestMethod]
        [DataRow("A", 10, 8)]
        [DataRow("B", 15, 15)]
        public void Order_Correctly_Calls_IWarehouse_When_Checking_If_An_Order_Can_be_Filled_Returns_True(string product, int stock, int amount)
        {
            warehouseMoq.Setup(warehouse => warehouse.HasProduct(product)).Returns(true);
            warehouseMoq.Setup(warehouse => warehouse.CurrentStock(product)).Returns(stock);

            Order a = new Order(product, amount);

            Assert.IsTrue(a.CanFillOrder(warehouseMoq.Object));

            warehouseMoq.Verify(warehouse => warehouse.HasProduct(product), Times.Once);
            warehouseMoq.Verify(warehouse => warehouse.CurrentStock(product), Times.Once);
        }

        [DataTestMethod]
        [DataRow("A", 10, 15)]
        [DataRow("B", 15, 20)]
        public void Order_Correctly_Calls_IWarehouse_When_Checking_If_An_Order_Can_be_Filled_Returns_False(string product, int stock, int amount)
        {
            warehouseMoq.Setup(warehouse => warehouse.HasProduct(product)).Returns(true);
            warehouseMoq.Setup(warehouse => warehouse.CurrentStock(product)).Returns(stock);

            Order a = new Order(product, amount);

            Assert.IsFalse(a.CanFillOrder(warehouseMoq.Object));

            warehouseMoq.Verify(warehouse => warehouse.HasProduct(product), Times.Once);
            warehouseMoq.Verify(warehouse => warehouse.CurrentStock(product), Times.Once);
        }

        [DataTestMethod]
        [DataRow("A", 10)]
        [DataRow("B", 15)]
        public void Order_Correctly_Calls_IWarehouse_When_Filling_An_Order(string product, int amount)
        {
            Order a = new Order(product, amount);

            a.Fill(warehouseMoq.Object);

            Assert.IsTrue(a.IsFilled);
        }


        [DataTestMethod]
        [DataRow("A", 5)]
        [DataRow("B", 10)]
        public void Order_Correctly_Calls_IWarehouse_When_Filling_An_Order_And_Throws_Exception_When_Already_Filled(string product, int amount)
        {
            Order a = new Order(product, amount);

            a.Fill(warehouseMoq.Object);

            Assert.ThrowsException<OrderAlreadyFilled>(() => a.Fill(warehouseMoq.Object));
        }
    }
}
