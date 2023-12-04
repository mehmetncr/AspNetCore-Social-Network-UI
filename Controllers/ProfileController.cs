using AspNetCore_Social_Network_UI.Models;
using Microsoft.AspNetCore.Mvc;
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
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/4");  //API adresi ve get isteği 
            if (respons.StatusCode == System.Net.HttpStatusCode.OK)  //gelen sonuç Ok ise  kodu ise
            {
                var jsonData = await respons.Content.ReadAsStringAsync();  //gelen datanın içindeki veriler çıkarılır
                var data = JsonConvert.DeserializeObject<UserViewModel>(jsonData);  //gelen Json Tipindeki data view modele deserilize edilir
                return View(data);
            }
            return View();
        }
		public IActionResult MyPhotos()
		{
			return View();
		}

		public IActionResult MyAbout()
		{
			return View();
		}
		public IActionResult MyVideos()
    {
			return View();
		}
		public IActionResult MyFriends()
		{
			return View();
		}
	}

}
