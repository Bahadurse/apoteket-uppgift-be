using Apoteket.UppgiftBE.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Apoteket.UppgiftBE.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

            GetLatestListOfItems();
        }

        static void GetLatestListOfItems()
        {
            HttpRuntime.Cache.Add("ScheduledTaskTrigger",
                                  string.Empty,
                                  null,
                                  Cache.NoAbsoluteExpiration,
                                  TimeSpan.FromMinutes(60),
                                  CacheItemPriority.NotRemovable,
                                  new CacheItemRemovedCallback(PerformScheduledTasks));
        }

        static void PerformScheduledTasks(string key, Object value, CacheItemRemovedReason reason)
        {
            ProductList productList = new ProductList();
            productList.Get();
            GetLatestListOfItems();
        }
    }
}
