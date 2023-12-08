using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddPost()
        {
            return View();
        }
    }
}
