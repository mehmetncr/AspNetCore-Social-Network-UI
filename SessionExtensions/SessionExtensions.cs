using AspNetCore_Social_Network_UI.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace AspNetCore_Social_Network_UI.SessionExtensions
{
	public static class SessionExtensions
	{

		public static UserViewModel GetJsonUser(this ISession session)
		{
			var value = session.GetString("user");
			return value == null ? default : JsonConvert.DeserializeObject<UserViewModel>(value);
		}
	}
}
