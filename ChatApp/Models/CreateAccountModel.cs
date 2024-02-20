using ChatApp.DBM.Items;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
	public class CreateAccountModel
	{
		[Required(ErrorMessage = "Tell us what is your name?")]
		public string? name { get; set; }
		
		[Required(ErrorMessage = "Email must be valid")]
		public string? email { get; set; }

		[Required(ErrorMessage = "Password must be valid")]
		public string? password { get; set; }

		[Required(ErrorMessage = "user might be already existing,check fields")]
		public bool? success { get; set; }
	}
}
