using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChatApp.DBM.Items
{
	public class GroupUserLinkItem
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [NotNull]
        public int groupId { get; set; }

		[NotNull]
		public int userId { get; set; }
    }
}
