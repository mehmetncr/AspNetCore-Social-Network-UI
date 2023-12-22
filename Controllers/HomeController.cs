using AspNetCore_Social_Network_UI.Models;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var userProfilPicture = HttpContext.Session.GetJsonUser().UserProfilePicture;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);    
            var respons = await http.GetAsync("https://localhost:7091/api/Homes");  //API adresi ve get isteği 
            if (respons.StatusCode == System.Net.HttpStatusCode.OK)  //gelen sonuç Ok ise  kodu ise
            {
                var jsonData = await respons.Content.ReadAsStringAsync();  //gelen datanın içindeki veriler çıkarılır
                var data = JsonConvert.DeserializeObject<HomeViewModel>(jsonData);  //gelen Json Tipindeki data view modele deserilize edilir
                var response = await http.GetAsync("https://localhost:7091/api/Messages/GetAllMessages");
                var res = respons.Content.ReadAsStringAsync();
                if (respons.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var secJsonData = await response.Content.ReadAsStringAsync();
                    var secData = JsonConvert.DeserializeObject<MessagesAndFriendsViewModel>(secJsonData);
                    secData.UserProfilPic = HttpContext.Session.GetJsonUser().UserProfilePicture;
                    ViewBag.userId = HttpContext.Session.GetJsonUser().UserId;
                    ViewBag.token = token;
                    ViewBag.profilPicture = userProfilPicture;
                    HomeAndMessageViewModel model = new HomeAndMessageViewModel()
                    {
                        HomeViewModel = data,
                        MessagesAndFriendsViewModel = secData,
                    };
                    return View(model);
                }
            }
            return View();

        }
    }
}
