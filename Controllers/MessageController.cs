using AspNetCore_Social_Network_UI.Models;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> MyMessages()
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Messages/GetAllMessages");
            var res = respons.Content.ReadAsStringAsync();
            if (respons.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MessagesAndFriendsViewModel>(jsonData);
                data.UserProfilPic = HttpContext.Session.GetJsonUser().UserProfilePicture;
                ViewBag.userId = HttpContext.Session.GetJsonUser().UserId;
                ViewBag.token = token;
                return View(data);
            }
            return View();
        }
        [HttpPost]
        public async Task<string> StartNewChat(int userId)
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var result = await http.GetAsync("https://localhost:7091/api/Messages/StartNewChat/" + userId);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
                
            }      
            return await result.Content.ReadAsStringAsync();
        }
    }
}
