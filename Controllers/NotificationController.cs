using AspNetCore_Social_Network_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Text;
using System.Reflection;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NotificationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task AcceptReq(string notificationId)
        {

            var jsonData = JsonConvert.SerializeObject(notificationId);
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient(); 
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            var result = await http.PostAsync("https://localhost:7091/api/Notifications/AcceptFriendReq", content);
            var errorMessage = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
               
            }
            ModelState.AddModelError("Error", errorMessage);

        }
        [HttpPost]
        public async Task RejectReq(string notificationId)
        {

            var jsonData = JsonConvert.SerializeObject(notificationId);
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient(); 
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            var result = await http.PostAsync("https://localhost:7091/api/Notifications/RejectReq", content);
            var errorMessage = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {

            }
            ModelState.AddModelError("Error", errorMessage);

        }
    }
}
