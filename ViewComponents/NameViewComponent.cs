﻿using AspNetCore_Social_Network_UI.Models;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_Social_Network_UI.ViewComponents
{
	public class NameViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			UserViewModel user = HttpContext.Session.GetJsonUser();
			string username = user.UserFirstName+" "+user.UserLastName;
			return View("Default",	username);
		}
		
		
	}
}
