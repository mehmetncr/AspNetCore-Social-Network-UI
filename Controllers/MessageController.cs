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
                ViewBag.userId = HttpContext.Session.GetJsonUser().UserId;
                ViewBag.token = token;
                return View(data);
            }
            return View();
        }
        //public async Task<IActionResult> StartNewChat(int userId)
        //{          
        //          string token = HttpContext.Session.GetJsonUser().AccessToken;
        //          var http = _httpClientFactory.CreateClient(); 
        //          http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);      
        //          var result = await http.PostAsync("https://localhost:7091/api/Post/AddPost/"+ userId);
        //          if (result.IsSuccessStatusCode)
        //          {
        //              return RedirectToAction("Index", "Home");
        //          }
        //          var errorMessage = await result.Content.ReadAsStringAsync();
        //          return RedirectToAction("Index", "Home");
        //      }
    }
}
