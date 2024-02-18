using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChatApp.DBM.Items
{
	public class UserItem
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int id { get; set; }

		[AllowNull]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public string token { get; set; }

		[AllowNull]
		public string name { get; set; }

		[AllowNull]
		public string email { get; set; }

		[AllowNull]
		public string password { get; set; }
	}
}
