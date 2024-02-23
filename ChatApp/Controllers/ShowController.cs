using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatApp.Controllers
{
	public class ShowController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Groups()
		{
			return View();
		}

		public IActionResult Profile(string token)
		{
			return PartialView(model: token);
		}
	}
}
