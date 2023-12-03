using System.ComponentModel.DataAnnotations;

namespace AspNetCore_Social_Network_UI.Models
{
	public class AccountViewModel
	{
        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        public string FirstName { get; set; }
		[Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")] 
		public string Email { get; set; }
		[Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
		public string Gender { get; set; }
		[Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        [Compare("Password",ErrorMessage ="Şifreler Uyuşmuyor")]
		[DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
		public bool RememberMe { get; set; }

    }
}

