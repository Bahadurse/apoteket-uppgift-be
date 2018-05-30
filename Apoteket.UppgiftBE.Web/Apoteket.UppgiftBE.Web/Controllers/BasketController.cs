using System.Web.Mvc;
using Apoteket.UppgiftBE.Web.Data;
using Apoteket.UppgiftBE.Web.Models;
using Apoteket.UppgiftBE.Web.OrderService;

namespace Apoteket.UppgiftBE.Web.Controllers
{
	public class BasketController : Controller
	{
		readonly IBasket _basket;

		public BasketController(IBasket basket)
		{
			_basket = basket;
		}

		public ActionResult Index()
		{
            var products = _basket.GetProducts();

            return View(products);
		}

		public ActionResult Add(int id)
		{
            // Add item to basket
            _basket.AddProduct(id);

			return RedirectToAction("Index", "Home");
		}

		public ActionResult Remove(int id)
		{
            // Remove item from basket
            _basket.RemoveProduct(id);

			return RedirectToAction("Index", "Basket");
		}

		public ActionResult Checkout()
		{
            // Checka ut korgen och visa erhållet ordernummer
            string orderNumber = _basket.Checkout();
            TempData["OrderNumber"] = orderNumber;

            return RedirectToAction("Index", "Basket");
		}
	}
}