using Apoteket.UppgiftBE.Web.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteket.UppgiftBE.Web.Tests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestGetproducts()
        {
            var productList = new ProductList();

            var response = productList.Get();

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void TestAddItems()
        {
            var basket = new Basket();

            int productId = 1;

            basket.AddProduct(productId);
        }

        [TestMethod]
        public void TestRemoveItems()
        {
            var basket = new Basket();

            int productId = 1;

            basket.RemoveProduct(productId);
        }

        [TestMethod]
        public void TestCheckout()
        {
            var basket = new Basket();

            var response = basket.Checkout();

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void TestGetproductsDB()
        {
            var basket = new Basket();

            var response = basket.GetProducts();

            Assert.IsNotNull(response);
        }
    }
}
