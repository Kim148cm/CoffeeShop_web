using System.Web.Mvc;

namespace CoffeeShop_web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Chưa đăng nhập
            if (Session["Username"] == null)
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary(
                            new { controller = "Auth", action = "Login" }
                        )
                    );
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
