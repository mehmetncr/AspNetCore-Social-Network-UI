using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_Social_Network_UI.ViewComponents
{
    public class NotificationAndMessagesInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            return View("Default", token);
        }
    }
}
