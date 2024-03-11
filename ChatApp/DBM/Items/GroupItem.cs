using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChatApp.DBM.Items
{
	public class GroupItem
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public string? token { get; set; }
		
		public int owner { get; set; }

		public string? name { get; set; }
		
		[NotNull]
		public int global { get; set; }
		
		[NotNull]
		[DefaultValue(1)]
		public int active { get; set; }
	}
}
