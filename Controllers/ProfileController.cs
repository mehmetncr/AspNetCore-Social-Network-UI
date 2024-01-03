using AspNetCore_Social_Network_UI.Models;
using AspNetCore_Social_Network_UI.SessionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using NuGet.Configuration;
using NuGet.Protocol;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> MyProfile()
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/MyProfile");  //API adresi ve get isteği 
            if (respons.IsSuccessStatusCode)  //gelen sonuç Ok ise  kodu ise
            {
                var jsonData = await respons.Content.ReadAsStringAsync();  //gelen datanın içindeki veriler çıkarılır
                var data = JsonConvert.DeserializeObject<ProfileViewModel>(jsonData);  //gelen Json Tipindeki data view modele deserilize edilir
                ViewBag.profilPicture = data.User.UserProfilePicture;
                return View(data);
            }
            return View();
        }
        public async Task<IActionResult> MyPhotos()
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/Images");
            if (respons.IsSuccessStatusCode)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<PostViewModel>>(jsonData);
                return View(data);
            }
            return View();
        }

        public IActionResult MyAbout()
        {
            var user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));
            return View(user);
        }
        public async Task<IActionResult> MyVideos()
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/Videos");
            if (respons.IsSuccessStatusCode)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<PostViewModel>>(jsonData);
                return View(data);
            }
            return View();
        }
        public async Task<IActionResult> MyFriends()
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/Friends");
            if (respons.IsSuccessStatusCode)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ProfileFriendsViewModel>(jsonData);
                return View(data);
            }
            return View();
        }
        public async Task<IActionResult> MyNotifications()
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Notifications/AllNotification");
            if (respons.IsSuccessStatusCode)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<NotificationViewModel>>(jsonData);
                return View(data);
            }
            return View();
        }


        [HttpGet]
        public IActionResult EditUserInfo()
        {
            var user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserInfo(UserViewModel model, string month, string day, string year, string gender)
        {
            UserViewModel user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));  //user bilgileri alınır ve ekrandan gelen bilgiler ile değişşerek apiye gönderilir
            string date = year + "-" + month + "-" + day;
            user.UserFirstName = model.UserFirstName;
            user.UserLastName = model.UserLastName;
            user.UserGender = gender;
            user.UserBirthDate = DateTime.Parse(date);
            user.UserLocation = model.UserLocation;
            user.UserBiography = model.UserBiography;
            user.UserPhoneNumber = model.UserPhoneNumber;
            user.UserLanguage1 = model.UserLanguage1;
            user.UserLanguage2 = model.UserLanguage2;
            user.UserLanguage3 = model.UserLanguage3;

            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var jsonData = JsonConvert.SerializeObject(user);
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            var result = await http.PutAsync("https://localhost:7091/api/Profiles/update", content);
            var errorMessage = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                UserViewModel newUser = JsonConvert.DeserializeObject<UserViewModel>(await result.Content.ReadAsStringAsync());
                newUser.AccessToken = token;
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(newUser));
                return View(model);
            }
            ModelState.AddModelError("Error", errorMessage);

            return View(model);
        }



        [HttpGet]
        public IActionResult EditUserJobEdu()
        {
            var user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserJobEdu(UserViewModel model)
        {
            UserViewModel user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));
            user.UserEducationInfo = model.UserEducationInfo;
            user.UserJobInfo = model.UserJobInfo;

            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var jsonData = JsonConvert.SerializeObject(user);
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            var result = await http.PutAsync("https://localhost:7091/api/Profiles/update", content);
            var errorMessage = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                UserViewModel newUser = JsonConvert.DeserializeObject<UserViewModel>(await result.Content.ReadAsStringAsync());
                newUser.AccessToken = token;
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(newUser));
                return View(model);
            }
            ModelState.AddModelError("Error", errorMessage);

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> EditUserInterest()   //ilgi alanları sayfası için usera ait ilgi alanlarını çeker
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/GetInterest");
            if (respons.IsSuccessStatusCode)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<InterestViewModel>>(jsonData);
                data.Reverse();
                return View(data);
            }
            return View();


        }
        [HttpPost]
        public async Task<List<InterestViewModel>> AddUserIntrest(string interest)    //ilgi alanı ekleme
        {
            var user = HttpContext.Session.GetJsonUser();
            string token = user.AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var jsondata = JsonConvert.SerializeObject(interest);
            var content = new StringContent(jsondata, encoding: Encoding.UTF8, "application/json");
            var result = await http.PostAsync("https://localhost:7091/api/Profiles/AddInterest", content);
            if (result.IsSuccessStatusCode)
            {
                var jsonData = await result.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<InterestViewModel>>(jsonData);
                data.Reverse();
                return data;
            }
            var errorMessage = await result.Content.ReadAsStringAsync();
            List<InterestViewModel> model = new List<InterestViewModel>();
            return model;
        }
        [HttpPost]
        public async Task<List<InterestViewModel>> UpdateUserInterest(string interest)
        {
            var user = HttpContext.Session.GetJsonUser();
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var jsondata = JsonConvert.SerializeObject(interest);
            var content = new StringContent(jsondata, encoding: Encoding.UTF8, "application/json");
            var result = await http.PutAsync("https://localhost:7091/api/Profiles/UpdateInterest", content);
            if (result.IsSuccessStatusCode)
            {
                var jsonData = await result.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<InterestViewModel>>(jsonData);
                data.Reverse();
                return data;
            }
            var errorMessage = await result.Content.ReadAsStringAsync();
            List<InterestViewModel> model = new List<InterestViewModel>();
            return model;
        }


        [HttpGet]
        public async Task<IActionResult> EditUserSettings()
        {
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/UserSettings");
            if (respons.IsSuccessStatusCode)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<PrivacySettingsViewModel>(jsonData);
                return View(data);
            }
            return View();
        }
        [HttpPost]
        public async Task<PrivacySettingsViewModel> UpdateSettings(string settingName)
        {

            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var jsondata = JsonConvert.SerializeObject(settingName);
            var content = new StringContent(jsondata, encoding: Encoding.UTF8, "application/json");
            var result = await http.PutAsync("https://localhost:7091/api/Profiles/UpdateSettigs", content);
            if (result.IsSuccessStatusCode)
            {
                var jsonData = await result.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<PrivacySettingsViewModel>(jsonData);
                return data;
            }
            var errorMessage = await result.Content.ReadAsStringAsync();
            PrivacySettingsViewModel model = new PrivacySettingsViewModel();
            return model;
        }



        [HttpGet]
        public IActionResult EditUserPassword()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditUserPassword(EditPasswordViewModel model)
        {

            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            var result = await http.PutAsync("https://localhost:7091/api/Accounts/EditPassword", content);
            var errorMessage = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {

                ModelState.AddModelError("", "Şifre Değiştirme Başarılı");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ModelState.AddModelError("", "Hay Aksi Birşeyler Ters Gitti");

            }
            else
            {
                ModelState.AddModelError("", "Mevcut Şifre Yanlış");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> OtherProfile(int id)
        {

            int userId = HttpContext.Session.GetJsonUser().UserId;
            if (id == userId)
            {
                return RedirectToAction("MyProfile");
            }
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/GetOtherProfile/" + id);
            if (respons.IsSuccessStatusCode)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserViewModel>(jsonData);
                ViewBag.UserId = userId;
                return View(data);
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> OtherPhotos(int id)
        {
            int userId = HttpContext.Session.GetJsonUser().UserId;
            if (id == userId)
            {
                return RedirectToAction("MyPhotos");
            }
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/GetOtherPhotos/" + id);
            if (respons.IsSuccessStatusCode)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserViewModel>(jsonData);
                ViewBag.UserId = userId;
                return View(data);
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> OtherVideos(int id)
        {
            int userId = HttpContext.Session.GetJsonUser().UserId;
            if (id == userId)
            {
                return RedirectToAction("MyVideos");
            }
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/GetOtherVideos/" + id);
            if (respons.IsSuccessStatusCode)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserViewModel>(jsonData);
                ViewBag.UserId = userId;
                return View(data);
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> OtherFriends(int id)
        {
            int userId = HttpContext.Session.GetJsonUser().UserId;
            if (id == userId)
            {
                return RedirectToAction("MyFriends");
            }
            string token = HttpContext.Session.GetJsonUser().AccessToken;
            var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/GetOtherFriends/" + id);
            if (respons.IsSuccessStatusCode)
            {
                var jsonData = await respons.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<OtherFriendsViewModel>(jsonData);
                ViewBag.UserId = userId;
                return View(data);
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> EditProfilePhoto(IFormFile formFile)
        {
            if (formFile != null)
            {
                var user = HttpContext.Session.GetJsonUser();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\profilpictures", formFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                formFile.CopyTo(stream);
                UserViewModel newUser = user;
                newUser.UserProfilePicture = "/images/profilpictures/" + formFile.FileName;

                var http = _httpClientFactory.CreateClient();
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, user.AccessToken);
                var jsonData = JsonConvert.SerializeObject(newUser);
                var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
                var result = await http.PutAsync("https://localhost:7091/api/Profiles/update", content);
                var errorMessage = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("MyProfile");
                }
            }
            return RedirectToAction("MyProfile");
        }
        [HttpGet]
        public async Task<IActionResult> TurnOnlineOfflineUser(string type)
        {
            var user = HttpContext.Session.GetJsonUser();
            var http = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(type);
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, user.AccessToken);
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            var result = await http.PutAsync("https://localhost:7091/api/Profiles/TurnOnlineOfflineUser", content);
            if (result.IsSuccessStatusCode)
            {
                
                if (type == "Online")
                {
                    user.UserIsOnline = true;
                    var newUser = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("user", newUser);
                }
                else
                {
                    user.UserIsOnline = false;
                    var newUser = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("user", newUser);
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }

}
