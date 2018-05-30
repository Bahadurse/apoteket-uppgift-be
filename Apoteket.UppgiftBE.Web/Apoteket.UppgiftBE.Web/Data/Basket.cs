using System.Collections.Generic;
using Apoteket.UppgiftBE.Web.Models;
using LinqToDB.Data;
using LinqToDB;
using LinqToDB.Common;
using System.Configuration;
using DataModel;
using System.Linq;
using System;
using Apoteket.UppgiftBE.Web.OrderService;

namespace Apoteket.UppgiftBE.Web.Data
{
    public interface IBasket
    {
        IReadOnlyList<Models.Product> GetProducts();
        void AddProduct(int itemId);
        void RemoveProduct(int itemId);
        string Checkout();
    }

    public class Basket : IBasket
    {
        public IReadOnlyList<Models.Product> GetProducts()
        {
            // Hämta från DB med hjälp av linq2db (https://github.com/linq2db/linq2db)
            using (var db = new DataModel.NorthwindDB())
            {
                var query = (from p in db.Products
                             select new Models.Product
                             {
                                 Id = p.ProductID,
                                 Name = p.ProductName,
                                 Price = p.UnitPrice.ToString()
                             }).ToList();


                return query;
            }
        }

        public void AddProduct(int productId)
        {
            // Spara till DB med hjälp av linq2db (https://github.com/linq2db/linq2db)
            ProductList productList = new ProductList();
            var product = productList.Get()
                            .Where(p => p.Id == productId).SingleOrDefault();

            using (var db = new NorthwindDB())
            {
                db.Products
                    .Value(p => p.ProductName, product.Name)
                    .Value(p => p.UnitPrice, Convert.ToDecimal(product.Price.Replace(".", ",")))
                    .Insert();
            }
        }

        public void RemoveProduct(int productId)
        {
            // Spara till DB med hjälp av linq2db (https://github.com/linq2db/linq2db)
            using (var db = new NorthwindDB())
            {
                db.Products
                    .Where(p => p.ProductID == productId)
                    .Delete();
            }
        }

        public string Checkout()
        {
            // Checkout the basket and return the id provided by order service
            var items = GetProducts();
            string[] ItemIds = items.Select(d => d.Id.ToString()).ToArray();
            var Total = items.Sum(s => decimal.Parse(s.Price));

            OrderServiceClient client = new OrderServiceClient();
            OrderService.Order order = new OrderService.Order
            {
                CustomerName = "Apoteket",
                ItemIds = ItemIds,
                Total = Total
            };
            string ordernumber = string.Empty;

            try
            {
                ordernumber = client.Place(order);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ordernumber;
        }
    }
}