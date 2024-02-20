using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
	public class LoginModel
	{
		[Required(ErrorMessage = "email must be valid")]
		public string? email { get; set; }

		[Required(ErrorMessage = "password must be valid")]
		public string? password { get; set; }

		[Required(ErrorMessage = "failed to login,check the fields")]
		public bool? success { get; set; }
	}
}
