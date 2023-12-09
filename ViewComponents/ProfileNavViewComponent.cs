using AspNetCore_Social_Network_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetCore_Social_Network_UI.ViewComponents
{
    public class ProfileNavViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var user =  JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));
            return View(user);
        }
    }
}
