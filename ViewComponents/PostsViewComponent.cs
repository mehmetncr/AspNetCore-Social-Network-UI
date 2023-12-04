using AspNetCore_Social_Network_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AspNetCore_Social_Network_UI.ViewComponents
{
	public class PostsViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{

			return View();
		}
	}
}
