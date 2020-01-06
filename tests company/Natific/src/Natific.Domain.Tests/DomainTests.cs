using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Natific.Domain.Entities;

namespace Natific.Domain.Tests
{
    [TestClass]
    public class DomainTests
    {
        [TestMethod]
        public void ShouldCreateProductAndDecreaseStock()
        {
            var product1 = new Product("Keyboard Ryzer", 50, "Keyboard in English Layout", 2, 10);

            //i will withdraw 5 items, so quantity should be 5
            product1.DecreaseQuantity(5);
            Assert.AreEqual(5, product1.QuantityOnHand);
        }

        [TestMethod]
        public void ShouldCreateProductAndIncreaseStock()
        {
            var product1 = new Product("Keyboard Ryzer", 50, "Keyboard in English Layout", 2, 10);

            //i will entry 5 items, so quantity should be 15
            product1.IncreaseQuantity(5);
            Assert.AreEqual(15, product1.QuantityOnHand);
        }
    }
}
