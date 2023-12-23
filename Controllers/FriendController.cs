using AspNetCore_Social_Network_UI.Models;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class FriendController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FriendController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpPost]
        public async Task<string> AddFriend(int userId)
        {

            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var result = await http.GetAsync("https://localhost:7091/api/Friends/AddFriend/" + userId);
            var errorMessage = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                if (data=="Ok")
                {
                    return "Ok";
                }
                else
                {
                    return "AlreadySend";
                }
                
            }
            ModelState.AddModelError("Error", errorMessage);

            return "No";


        }
    }
}
