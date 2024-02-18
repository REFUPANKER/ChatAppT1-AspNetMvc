using ChatApp.DBM.Items;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ChatApp.DBM
{
	public class Core : DbContext
	{
		//public DbSet<Msg>? Messages { get; set; }
		public DbSet<UserItem>? Users { get; set; }
		public DbSet<GroupItem>? Groups { get; set; }
		public DbSet<GroupUserLinkItem>? GroupsOfUser { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=LAPTOPLENO;Initial Catalog=ChatApp;Integrated Security=True;TrustServerCertificate=true;");
		}
		public Core()
		{
			GetGroupsOfUser();
		}

		public List<GroupItem?> JoinedGroups = new List<GroupItem?>();
		public void GetGroupsOfUser()
		{
			if (GroupsOfUser != null)
			{
				JoinedGroups.Clear();
				var res = (from g in Groups
						   join gou in GroupsOfUser on g.id equals gou.groupId
						   where gou.userId == Pool.uid
						   select g).Distinct();
				JoinedGroups = res.ToList();
			}
		}

		public void AddGroup(string name, int global)
		{
			if (Groups != null && GroupsOfUser != null)
			{
				GroupItem groupItem = new GroupItem() { name = name, global = global };
				JoinedGroups.Add(groupItem);
				Groups.Add(groupItem);
				SaveChanges();
				GroupsOfUser.Add(new GroupUserLinkItem() { groupId = groupItem.id, userId = Pool.uid });
				SaveChanges();
			}
		}
	}
}
