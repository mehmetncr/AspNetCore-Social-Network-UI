using AspNetCore_Social_Network_UI.Models;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
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
            if (model.PostImageUrl != null) //IFromFile kontrolüne çevirilecek
            {
                model.PostType = "Image";
            }
            if(model.PostVideoUrl != null)  //Video kontrolüne çevilecek
            {
                model.PostType = "Video";
            }
            else
            {
                model.PostType = "Text";
            }
            var user = HttpContext.Session.GetJsonUser();
            model.PostUserId = user.UserId;
            var http = _httpClientFactory.CreateClient();
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
        public async Task AddComment(string comment, int postId)
        {
            var user = HttpContext.Session.GetJsonUser();
            CommentViewModel model = new CommentViewModel()
            {
                CommentContent = comment,
                CommentDate = DateTime.Now,
                CommentPostId = postId,
                CommentUserId = user.UserId
            };
            var http = _httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsondata, encoding: Encoding.UTF8, "application/json");
            var result = await http.PostAsync("https://localhost:7091/api/Post/AddComment", content);
            var errorMessage = await result.Content.ReadAsStringAsync();
        }
    }
}
