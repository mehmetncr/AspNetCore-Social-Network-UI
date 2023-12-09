using AspNetCore_Social_Network_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net.Http;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> MyProfile()
        {
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles");  //API adresi ve get isteği 
            if (respons.StatusCode == System.Net.HttpStatusCode.OK)  //gelen sonuç Ok ise  kodu ise
            {
                var jsonData = await respons.Content.ReadAsStringAsync();  //gelen datanın içindeki veriler çıkarılır
                var data = JsonConvert.DeserializeObject<ProfileViewModel>(jsonData);  //gelen Json Tipindeki data view modele deserilize edilir
                return View(data);
            }
            return View();
        }
        public async Task<IActionResult> MyPhotos()
        {
            var http = _httpClientFactory.CreateClient(); 
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/Images");  
            if (respons.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                var jsonData = await respons.Content.ReadAsStringAsync(); 
                var data = JsonConvert.DeserializeObject<List<PostViewModel>>(jsonData); 
                return View(data);
            }
            return View();
        }

        public async Task<IActionResult> MyAbout()
        {
			var user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));
			return View(user);
        }
        public async Task<IActionResult> MyVideos()
        {
            var http = _httpClientFactory.CreateClient(); 
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/Videos"); 
            if (respons.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await respons.Content.ReadAsStringAsync(); 
                var data = JsonConvert.DeserializeObject<List<PostViewModel>>(jsonData);  
                return View(data);
            }
            return View();
        }
        public async Task<IActionResult> MyFriends()
        {
			var http = _httpClientFactory.CreateClient(); 
			var respons = await http.GetAsync("https://localhost:7091/api/Profiles/Friends"); 
			if (respons.StatusCode == System.Net.HttpStatusCode.OK)  
			{
				var jsonData = await respons.Content.ReadAsStringAsync(); 
				var data = JsonConvert.DeserializeObject<ProfileFriendsViewModel>(jsonData); 
				return View(data);
			}
			return View();
		}
    }

}
