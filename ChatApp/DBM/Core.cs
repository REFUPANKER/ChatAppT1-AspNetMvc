﻿using ChatApp.DBM.Items;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
						   where gou.userId == Convert.ToInt32(userid)
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

		public void AddGroup(int userid, string name, int global)
		{
			if (Groups != null && GroupsOfUser != null)
			{
				GroupItem groupItem = new GroupItem() { name = name, global = global };
				Groups.Add(groupItem);
				SaveChanges();
				GroupsOfUser.Add(new GroupUserLinkItem() { groupId = groupItem.id, userId = userid });
				SaveChanges();
			}
		}
	}
}
