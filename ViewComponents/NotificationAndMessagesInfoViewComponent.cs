using AspNetCore_Social_Network_UI.Models;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace AspNetCore_Social_Network_UI.ViewComponents
{
    public class NotificationAndMessagesInfoViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NotificationAndMessagesInfoViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {      

            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient(); 
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Notifications/GetAllNotification");  
            if (respons.IsSuccessStatusCode) 
            {
                var jsonData = await respons.Content.ReadAsStringAsync();  
                var data = JsonConvert.DeserializeObject<List<NotificationViewModel>>(jsonData); 
               ViewBag.token=token;
                return View(data);
            }
    
            return View("Default", token);
        }
    }
}
