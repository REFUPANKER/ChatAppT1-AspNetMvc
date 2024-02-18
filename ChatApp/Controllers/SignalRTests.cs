using ChatApp.DBM;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatApp.Controllers
{
	public class SignalRTests : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddHub(string hubName,int global)
		{
			Pool.dbm.AddGroup(hubName, global);
			return RedirectToAction("Index");
		}
	}
}
