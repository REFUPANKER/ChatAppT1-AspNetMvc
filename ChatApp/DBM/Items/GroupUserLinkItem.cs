using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.DBM.Items
{
	public class GroupUserLinkItem
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int groupId { get; set; }

        public int userId { get; set; }
    }
}
