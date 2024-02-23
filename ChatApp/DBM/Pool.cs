using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatApp.DBM
{
	public class Pool
	{
		public static Random rnd = new Random();
		public static Core dbm = new Core();
	}
}
