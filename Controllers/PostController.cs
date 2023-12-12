using AspNetCore_Social_Network_UI.Models;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class PostController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PostController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddPost(PostViewModel model)
        {
            if(model.PostYoutubeUrl != null)
            {
                model.PostType = "Youtube";
            }
            else if (model.PostImageUrl != null) //IFromFile kontrolüne çevirilecek
            {
                model.PostType = "Image";
            }
            else if(model.PostVideoUrl != null)  //Video kontrolüne çevilecek
            {
                model.PostType = "Video";
            }
            else
            {
                model.PostType = "Text";
            }
            var user = HttpContext.Session.GetJsonUser();
            model.PostUserId = user.UserId;
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var jsondata = JsonConvert.SerializeObject(model);
			var content = new StringContent(jsondata, encoding: Encoding.UTF8, "application/json");
            var result = await http.PostAsync("https://localhost:7091/api/Post/AddPost", content);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
			var errorMessage = await result.Content.ReadAsStringAsync();
			return RedirectToAction("Index", "Home");
        }
        public async Task<CommentViewModel> AddComment(string comment, int postId)
        {
            var user = HttpContext.Session.GetJsonUser();
            CommentViewModel model = new CommentViewModel()
            {
                CommentContent = comment,
                CommentDate = DateTime.Now,
                CommentPostId = postId
            };
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var jsondata = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsondata, encoding: Encoding.UTF8, "application/json");
            var result = await http.PostAsync("https://localhost:7091/api/Post/AddComment", content);
            var errorMessage = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                return model;
            }
            return null;
        }
        public async Task<int> PostLike(int postId)
        {
			string token = HttpContext.Session.GetJsonUser().AccessToken;
			var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
			http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
			var result = await http.GetAsync("https://localhost:7091/api/Post/PostLike/"+postId);
            var value = await result.Content.ReadAsStringAsync();
			return Convert.ToInt32(value);
		}

	}
}
