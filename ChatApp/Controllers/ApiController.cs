using ChatApp.DBM;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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

		public JsonResult GetUserClosedGroups()
		{
			if (Pool.dbm.GroupsOfUser != null)
			{
				var r = (from g in Pool.dbm.Groups
						 join gou in Pool.dbm.GroupsOfUser on g.id equals gou.groupId
						 where gou.userId == Convert.ToInt32(UserId + "") && g.active == 0
						 select g).Distinct();
				return Json(r);
			}
			return Json(null);
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
