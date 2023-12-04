using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class AddPostController : Controller
    {
        [HttpPost]
        public IActionResult NewPost()
        {
            return View();
        }
    }
}
