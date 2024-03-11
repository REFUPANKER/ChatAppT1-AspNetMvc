using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
	[Authorize]
	public class ClosedRoomController : Controller
	{
		[HttpGet]
		public IActionResult Index(string token)
		{
			return View(model:token);
		}
	}
}
