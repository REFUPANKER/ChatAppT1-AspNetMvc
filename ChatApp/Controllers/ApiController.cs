using ChatApp.DBM;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
	public class ApiController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public int UserId { get => Convert.ToInt32(User.Claims?.Where(x => x.Type.ToString() == "UserId").FirstOrDefault()?.Value); }
		public JsonResult GetUserGroups()
		{
			return Json(Pool.dbm.GetGroupsOfUser(UserId + ""));
		}

		int lim = 3;
		public JsonResult GetNextGroupsByLimit(int start)
		{
			return Json(Pool.dbm.Groups?.Where(x => x.id >= start).Take(lim));
		}
		public JsonResult GetPreGroupsByLimit(int end)
		{
			return Json(Pool.dbm.Groups?.Where(x => x.id <= end && x.id >= end - lim).Take(lim));
		}
	}
}
