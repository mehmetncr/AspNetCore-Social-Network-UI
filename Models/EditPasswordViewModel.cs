using System.ComponentModel.DataAnnotations;

namespace AspNetCore_Social_Network_UI.Models
{
    public class EditPasswordViewModel
    {
        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; }
        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        [Compare("NewPassword", ErrorMessage = "Şifreler Uyuşmuyor")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set;}
    }
}
