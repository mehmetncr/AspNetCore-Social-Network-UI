using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_Social_Network_UI.Controllers
{
	public class ProfileController : Controller
	{
		public IActionResult MyProfile()
		{
			return View();
		}
		public IActionResult MyPhotos()
		{
			return View();
		}
	}
}
