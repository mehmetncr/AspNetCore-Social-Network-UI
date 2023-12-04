using AspNetCore_Social_Network_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class HomeController : Controller

    {
        public IActionResult Index()
        {         

            return View();

	{
		private readonly IHttpClientFactory _httpClientFactory;

		public HomeController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
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
    }
}
