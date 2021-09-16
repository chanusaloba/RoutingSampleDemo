using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace RoutingSample.Controllers
{
    public class Product : Controller
    {
        [Host("contoso.com", "adventure-works.com")]
        public class ProductController : Controller
        {
            public IActionResult Index()
            {
                //return ControllerContext.MyDisplayRouteInfo();
                return null;
            }

            [Host("example.com:8080")]
            public IActionResult Privacy()
            {
                //return ControllerContext.MyDisplayRouteInfo();
                return null;
            }
        }
    }
}