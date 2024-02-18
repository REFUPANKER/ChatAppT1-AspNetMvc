using ChatApp.DBM;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
	public class RoomController : Controller
	{
		public IActionResult Index(string token)
		{
			return View(model: Pool.dbm.JoinedGroups.Select(x => x).Where(x => x?.token == token).First());
		}
	}
}
