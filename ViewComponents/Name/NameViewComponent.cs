using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_Social_Network_UI.ViewComponents.Name
{
    public class NameViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
