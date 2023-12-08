using AspNetCore_Social_Network_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_Social_Network_UI.ViewComponents
{
    public class NewPostViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            PostViewModel model = new PostViewModel();
            return View(model);
        }
    }
}
