using ChatApp.DBM.Items;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json.Serialization;

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


		public List<GroupItem>? GetGroupsOfUser(string? userid)
		{
			if (GroupsOfUser != null)
			{
				var res = (from g in Groups
						   join gou in GroupsOfUser on g.id equals gou.groupId
						   where gou.userId == Convert.ToInt32(userid) && g.active == 1
						   select g).Distinct();
				return res.ToList();
			}
			return null;
		}
		public List<GroupItem>? GetClosedGroupsOfUser(string? userid)
		{
			if (GroupsOfUser != null)
			{
				var res = (from g in Groups
						   join gou in GroupsOfUser on g.id equals gou.groupId
						   where gou.userId == Convert.ToInt32(userid) && g.active == 0
						   select g).Distinct();
				return res.ToList();
			}
			return null;
		}

		public List<GroupItem>? GetGroupsOfUser(Controller controller)
		{
			string? userId = controller.User.Claims?.Where(x => x.Type.ToString() == "UserId").FirstOrDefault()?.Value;
			return GetGroupsOfUser(userId);
		}

		public List<GroupItem>? GetGroupsOfUser(HttpContext context)
		{
			string? userId = context.User.Claims?.Where(x => x.Type.ToString() == "UserId").FirstOrDefault()?.Value;
			return GetGroupsOfUser(userId);
		}

		public void AddGroup(int userid, string name, int global)
		{
			if (Groups != null && GroupsOfUser != null)
			{
				GroupItem groupItem = new GroupItem() { owner = userid, name = name, global = global };
				Groups.Add(groupItem);
				SaveChanges();
				GroupsOfUser.Add(new GroupUserLinkItem() { groupId = groupItem.id, userId = userid });
				SaveChanges();
			}
		}

		public void JoinGroup(int userid, string groupToken)
		{
			if (Groups != null && GroupsOfUser != null)
			{
				GroupItem? group = Groups?.Where(x => x.token == groupToken).FirstOrDefault();
				if (group != null)
				{
					GroupsOfUser.Add(new GroupUserLinkItem() { groupId = group.id, userId = userid });
					SaveChanges();
				}
			}
		}

		public void LeaveGroup(int userid, string groupToken)
		{
			if (Groups != null && GroupsOfUser != null)
			{
				GroupItem? group = Groups?.Where(x => x.token == groupToken).FirstOrDefault();
				if (group != null)
				{
					GroupUserLinkItem? guli = GroupsOfUser.Where(x => x.userId == userid && x.groupId == group.id).FirstOrDefault();
					if (guli != null)
					{
						GroupsOfUser.Remove(guli);
					}
					SaveChanges();
				}
			}
		}

		public void CloseGroup(int userid, string groupToken)
		{
			if (Groups != null)
			{
				GroupItem? group = Groups?.Where(x => x.token == groupToken).FirstOrDefault();
				if (group != null && group.owner == userid)
				{
					group.active = 0;
					SaveChanges();
				}
			}
		}

	}
}
