using AspNetCore_Social_Network_UI.Models;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class AccountController : Controller
    {
		public readonly IHttpClientFactory _httpClientFactory;

		public AccountController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(AccountViewModel model)
		{
			var jsonData = JsonConvert.SerializeObject(model);
			var http = _httpClientFactory.CreateClient();
			var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
			var result = await http.PostAsync("https://localhost:7091/api/Accounts/Register", content);
			var errorMessage = await result.Content.ReadAsStringAsync();
			if (result.IsSuccessStatusCode)
			{
				
				return RedirectToAction("Index");
			}
			ModelState.AddModelError("Error", errorMessage);

			return View(model);

		}
		[HttpPost]
		
		public async Task<IActionResult> Login(AccountViewModel model)
		{
			var jsonData = JsonConvert.SerializeObject(model);
			var http = _httpClientFactory.CreateClient();
			var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
			var result = await http.PostAsync("https://localhost:7091/api/Accounts/Login", content);
            var asdsadsd = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
			{
				var asdsad = await result.Content.ReadAsStringAsync();
				HttpContext.Session.SetString("user", await result.Content.ReadAsStringAsync());
				//var user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));  Her User çekildiğinde bu şekilde alınacak
				return RedirectToAction("Index", "Home");
			}
            
            return View(model);

		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(AccountViewModel model)
		{
            var jsonData = JsonConvert.SerializeObject(model);
            var http = _httpClientFactory.CreateClient();
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            var result = await http.PutAsync("https://localhost:7091/api/Accounts/ResetPassword", content);
            var errorMessage = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Error", errorMessage);

			return View(model);

        }
		[HttpGet]
		public async Task<List<UserViewModel>> GetAllUsers()
		{
            var http = _httpClientFactory.CreateClient();
            var result = await http.GetAsync("https://localhost:7091/api/Accounts/GetAllUsers");
			if (result.IsSuccessStatusCode)
			{
				var users = JsonConvert.DeserializeObject<List<UserViewModel>>(await result.Content.ReadAsStringAsync());
                return users ;
			}
			return null;
        }
	}
}
