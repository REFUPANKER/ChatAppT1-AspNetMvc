using ChatApp.DBM;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
	public class SignalRTests : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddHub(string hubName)
		{
			//Pool.AddNewHub(hubName);
			//TODO: fix adding new hub
			return RedirectToAction("Index");
		}
	}
}
