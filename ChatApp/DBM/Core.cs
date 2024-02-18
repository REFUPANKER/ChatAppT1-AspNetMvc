using ChatApp.DBM.Items;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ChatApp.DBM
{
	public class Core : DbContext
	{
		public DbSet<Msg>? Messages { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=LAPTOPLENO;Initial Catalog=ChatApp;Integrated Security=True;TrustServerCertificate=true;");
		}

		//public List<Msg>? GetMessages(int lastid)
		//{
		//	try
		//	{
		//		List<Msg>? msgs;
		//		msgs = Messages?.Select(x => x).OrderByDescending(x => x).Where(x => x.id > lastid).Take(5).OrderBy(x => x).ToList();
		//		if (msgs != null && msgs.Count > 0)
		//		{
		//			foreach (var item in msgs)
		//			{
		//				if (item.id > lastid)
		//				{
		//					lastid = item.id;
		//				}
		//			}
		//		}
		//		return msgs;
		//	}
		//	catch { }
		//	return null;
		//}

		//public List<Msg>? GetPreviousMessages(int max)
		//{
		//	try
		//	{
		//		List<Msg>? msgs = Messages?.Select(x => x).OrderByDescending(x => x).Where(x => x.id <= max).Take(5).ToList();
		//		return msgs;
		//	}
		//	catch { }
		//	return null;
		//}

		//public void SendMessage(string message)
		//{
		//	Messages?.Add(new Msg() { sender = Pool.uid, content = message });
		//	SaveChanges();
		//}

		//public void RemoveMessage(int msgId)
		//{
		//	Msg? m = Messages?.Where(x => x.id == msgId).First();
		//	if (m!=null && m.sender==Pool.uid)
		//	{
		//		Messages?.Remove(m);
		//	}
		//	SaveChanges();
		//}
	}
}
