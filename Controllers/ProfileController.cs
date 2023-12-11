using AspNetCore_Social_Network_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net.Http;
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
            var http = _httpClientFactory.CreateClient();  //HttpClient döndürür
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/MyProfile");  //API adresi ve get isteği 
            if (respons.StatusCode == System.Net.HttpStatusCode.OK)  //gelen sonuç Ok ise  kodu ise
            {
                var jsonData = await respons.Content.ReadAsStringAsync();  //gelen datanın içindeki veriler çıkarılır
                var data = JsonConvert.DeserializeObject<ProfileViewModel>(jsonData);  //gelen Json Tipindeki data view modele deserilize edilir
                return View(data);
            }
            return View();
        }
        public async Task<IActionResult> MyPhotos()
        {
            var http = _httpClientFactory.CreateClient(); 
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/Images");  
            if (respons.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                var jsonData = await respons.Content.ReadAsStringAsync(); 
                var data = JsonConvert.DeserializeObject<List<PostViewModel>>(jsonData); 
                return View(data);
            }
            return View();
        }

        public async Task<IActionResult> MyAbout()
        {
			var user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));
			return View(user);
        }
        public async Task<IActionResult> MyVideos()
        {
            var http = _httpClientFactory.CreateClient(); 
            var respons = await http.GetAsync("https://localhost:7091/api/Profiles/Videos"); 
            if (respons.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await respons.Content.ReadAsStringAsync(); 
                var data = JsonConvert.DeserializeObject<List<PostViewModel>>(jsonData);  
                return View(data);
            }
            return View();
        }
        public async Task<IActionResult> MyFriends()
        {
			var http = _httpClientFactory.CreateClient(); 
			var respons = await http.GetAsync("https://localhost:7091/api/Profiles/Friends"); 
			if (respons.StatusCode == System.Net.HttpStatusCode.OK)  
			{
				var jsonData = await respons.Content.ReadAsStringAsync(); 
				var data = JsonConvert.DeserializeObject<ProfileFriendsViewModel>(jsonData); 
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
            string date = year+"-"+month+"-"+day;
            user.UserFirstName=model.UserFirstName;
            user.UserLastName=model.UserLastName;
            user.UserGender = gender;
            user.UserBirthDate= DateTime.Parse(date);
            user.UserLocation = model.UserLocation;
            user.UserBiography = model.UserBiography;
            user.UserPhoneNumber = model.UserPhoneNumber;

            var jsonData = JsonConvert.SerializeObject(user);
            var http = _httpClientFactory.CreateClient();
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            var result = await http.PutAsync("https://localhost:7091/api/Profiles/update", content);
            var errorMessage = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("user", await result.Content.ReadAsStringAsync());
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

            var jsonData = JsonConvert.SerializeObject(user);
            var http = _httpClientFactory.CreateClient();
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            var result = await http.PutAsync("https://localhost:7091/api/Profiles/update", content);
            var errorMessage = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("user", await result.Content.ReadAsStringAsync());
                return View(model);
            }
            ModelState.AddModelError("Error", errorMessage);

            return View(model);
        }



		[HttpGet]
		public IActionResult EditUserInterest()
		{
			var user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));
			return View(user);
		}
        [HttpPost]
        public IActionResult EditUserIntrest(string intrest)
        {
            var user = intrest;    //ekleme çıkarna işlemlri ajax yapılacak
            return View(user);
        }
        [HttpGet]
        public IActionResult EditUserSettings()
        {
            var user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));
            return View(user);
        }
		[HttpGet]
		public IActionResult EditUserPassword()
		{
			var user = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("user"));
			return View(user);
		}
	}

}
