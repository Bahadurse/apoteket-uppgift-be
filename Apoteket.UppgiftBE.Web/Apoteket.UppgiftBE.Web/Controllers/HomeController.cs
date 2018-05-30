using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Apoteket.UppgiftBE.Web.Data;
using Apoteket.UppgiftBE.Web.Models;
using System.Linq;
using System.Timers;

namespace Apoteket.UppgiftBE.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly IProductList _productList;

        public HomeController(IProductList productList)
        {
            _productList = productList;
        }

        public ActionResult Index()
        {
            // Lista de produkter som finns i _productList
            var products = (from p in _productList.Get()
                      where p.Id != null
                      where p.Name != null
                      select p
                      ).ToList();

            return View(products);
        }       
    }
}