using System.Collections.Generic;
using System.Web.Script.Serialization;
using Apoteket.UppgiftBE.Web.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace Apoteket.UppgiftBE.Web.Data
{
	public interface IProductList
	{
		IReadOnlyList<Product> Get();
	}

	public class ProductList : IProductList
	{
		IReadOnlyList<Product> _products;

		public ProductList()
		{
            // Hämta produktlistan med hjälp av RestSharp och lagra i Products
            // Observera att listan ändras en gång per timme
            _products = new List<Product>();

            var client = new RestClient("http://apoteket-uppgift-be.ginzburg.it");
            var request = new RestRequest("api/products", Method.GET);
            request.AddHeader("X-Key", "qwerty");
            //new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var queryResult = client.Execute<List<Product>>(request).Data;

            _products = queryResult;            
		}



		public IReadOnlyList<Product> Get() => _products;
	}
}