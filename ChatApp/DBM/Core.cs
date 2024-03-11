using ChatApp.DBM.Items;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DBM
{
	public class Core : DbContext
	{
		public DbSet<UserItem>? Users { get; set; }
		public DbSet<GroupItem>? Groups { get; set; }
		public DbSet<GroupUserLinkItem>? GroupsOfUser { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=LAPTOPLENO;Initial Catalog=ChatApp;Integrated Security=True;TrustServerCertificate=true;");
		}
	}
}
