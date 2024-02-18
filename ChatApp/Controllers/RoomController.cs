using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
	public class RoomController : Controller
	{
		public IActionResult Index(string name)
		{
			return View(model: name);
		}
	}
}
