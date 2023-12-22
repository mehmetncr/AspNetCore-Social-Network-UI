using AspNetCore_Social_Network_UI.Models;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace AspNetCore_Social_Network_UI.ViewComponents
{
    public class HomeFriendsAndMessagesViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeFriendsAndMessagesViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
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
            MessagesAndFriendsViewModel nullModel = new MessagesAndFriendsViewModel();
            return View(nullModel);
        }

    }
}
