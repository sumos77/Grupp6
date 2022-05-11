using Microsoft.AspNetCore.Mvc;

namespace hakimslivs.Controllers
{
    public class CartController : Controller
    {

        //[WebMethod]
        public static string GetCart(string JsonLocalStorageObj)
        {
            return JsonLocalStorageObj;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
