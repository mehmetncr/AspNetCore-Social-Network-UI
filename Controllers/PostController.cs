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
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddPost(PostViewModel model, IFormFile formFile, IFormFile formFile2)
        {
            if (HttpContext.Request.Form.Files.Count()>0)
            {
                var videoFile = HttpContext.Request.Form.Files[0];
                if (videoFile != null && videoFile.ContentType == "video/mp4")  //Video kontrolüne çevilecek
                {
                    model.PostType = "Video";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\postvideos", videoFile.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    videoFile.CopyTo(stream);
                    model.PostVideoUrl = "/images/postvideos/" + videoFile.FileName;
                }
            }

            if (model.PostYoutubeUrl != null)
            {
                model.PostType = "Youtube";
            }
            else if (formFile != null) //IFromFile kontrolüne çevirilecek
            {
                model.PostType = "Image";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\postimages", formFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                formFile.CopyTo(stream);
                model.PostImageUrl = "/images/postimages/" + formFile.FileName;
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
        public async Task<List<CommentViewModel>> AddComment(string comment, int postId, int commentId)
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            NewCommentAndReplyViewModel CommentAndReply = new NewCommentAndReplyViewModel();
            if (commentId != 0)
            {
                CommentAndReply.ReplyModel = new ReplyCommentViewModel()
                {
                    ReplyCommentCommentId = commentId,
                    ReplyCommentContent = comment,
                    ReplyCommentDate = DateTime.Now,
                    PostId = postId
                };
                var jsondata = JsonConvert.SerializeObject(CommentAndReply);
                var content = new StringContent(jsondata, encoding: Encoding.UTF8, "application/json");
                var result = await http.PostAsync("https://localhost:7091/api/Post/AddComment", content);
                var listdsads = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    var list = await result.Content.ReadAsStringAsync();
                    var jsonlist = JsonConvert.DeserializeObject<List<CommentViewModel>>(list);
                    return jsonlist;
                }
            }
            else
            {
                CommentAndReply.CommentModel = new CommentViewModel()
                {
                    CommentContent = comment,
                    CommentDate = DateTime.Now,
                    CommentPostId = postId
                };
                var jsondata = JsonConvert.SerializeObject(CommentAndReply);
                var content = new StringContent(jsondata, encoding: Encoding.UTF8, "application/json");
                var result = await http.PostAsync("https://localhost:7091/api/Post/AddComment", content);
                if (result.IsSuccessStatusCode)
                {
                    var list = await result.Content.ReadAsStringAsync();
                    var jsonlist = JsonConvert.DeserializeObject<List<CommentViewModel>>(list);
                    return jsonlist;
                }
            }
            return null;
        }
        public async Task<int> PostLike(int postId)
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var result = await http.GetAsync("https://localhost:7091/api/Post/PostLike/" + postId);
            var value = await result.Content.ReadAsStringAsync();
            return Convert.ToInt32(value);
        }
        public async Task<int> PostTakeBackLike(int postId)
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var result = await http.GetAsync("https://localhost:7091/api/Post/PostLike/" + postId);
            var value = await result.Content.ReadAsStringAsync();
            return Convert.ToInt32(value);
        }

    }
}
