using ChatApp.DBM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
	[Authorize]
	public class RoomController : Controller
	{
		public IActionResult Index(string token)
		{
			return View(model: Pool.dbm.GetGroupsOfUser(this)?.Select(x => x).Where(x => x?.token == token).First());
		}
	}
}
