using Microsoft.AspNetCore.Mvc;

namespace RoutingSample.Controllers
{
    public class GadgetController : Controller
    {
        public IActionResult Index()
        {
            var url = Url.Action("Edit", new { id = 17, });
            return Content(url);
        }

        public IActionResult Edit(int id)
        {
            //return ControllerContext.MyDisplayRouteInfo(id);
            return null;
        }
    }
}
