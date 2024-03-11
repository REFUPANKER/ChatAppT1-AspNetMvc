using ChatApp.DBM;
using ChatApp.DBM.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
	[Authorize]
	public class ApiController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public int UserId { get => Convert.ToInt32(User.Claims?.Where(x => x.Type.ToString() == "UserId").FirstOrDefault()?.Value); }

		public JsonResult GetUserGroups()
		{
			if (Pool.dbm.GroupsOfUser != null)
			{
				var r = (from g in Pool.dbm.Groups
						 join gou in Pool.dbm.GroupsOfUser on g.id equals gou.groupId
						 where gou.userId == Convert.ToInt32(UserId + "") && g.active == 1
						 select g).Distinct();
				return Json(r);
			}
			return Json(null);
		}

		public JsonResult GetUserClosedGroups()
		{
			Core c = new Core();
			if (c.GroupsOfUser != null)
			{
				var r = (from g in c.Groups
						 join gou in c.GroupsOfUser on g.id equals gou.groupId
						 where gou.userId == Convert.ToInt32(UserId + "") && g.active == 0
						 select g).Distinct();
				return Json(r);
			}
			return Json(null);
		}


		int lim = 3;
		public JsonResult GetNextGroupsByLimit(int start)
		{
			return Json(Pool.dbm.Groups?.Where(x => x.id >= start && x.active == 1 && x.global == 1).Take(lim));
		}

		public JsonResult GetPreGroupsByLimit(int end)
		{
			return Json(Pool.dbm.Groups?.Where(x => x.id <= end && x.id >= end - lim && x.active == 1 && x.global == 1).Take(lim));
		}

		public ActionResult AddGroup(string name, string global)
		{
			if (Pool.dbm.Groups != null && Pool.dbm.GroupsOfUser != null)
			{
				GroupItem groupItem = new GroupItem() { owner = UserId, name = name, active = 1, global = global == "on" ? 1 : 0, };
				Pool.dbm.Groups.Add(groupItem);
				Pool.dbm.SaveChanges();
				Pool.dbm.GroupsOfUser.Add(new GroupUserLinkItem() { groupId = groupItem.id, userId = UserId });
				Pool.dbm.SaveChanges();
			}
			return RedirectToAction("Index", "Groups");
		}

		public ActionResult JoinGroup(string groupToken)
		{
			if (Pool.dbm.Groups != null && Pool.dbm.GroupsOfUser != null)
			{
				GroupItem? group = Pool.dbm.Groups?.Where(x => x.token == groupToken).FirstOrDefault();
				if (group != null)
				{
					Pool.dbm.GroupsOfUser.Add(new GroupUserLinkItem() { groupId = group.id, userId = UserId });
					Pool.dbm.SaveChanges();
				}
			}
			return RedirectToAction("Index", "Groups");
		}

		public ActionResult LeaveGroup(string groupToken)
		{
			if (Pool.dbm.Groups != null && Pool.dbm.GroupsOfUser != null)
			{
				GroupItem? group = Pool.dbm.Groups?.Where(x => x.token == groupToken).FirstOrDefault();
				if (group != null)
				{
					GroupUserLinkItem? guli = Pool.dbm.GroupsOfUser.Where(x => x.userId == UserId && x.groupId == group.id).FirstOrDefault();
					if (guli != null)
					{
						Pool.dbm.GroupsOfUser.Remove(guli);
					}
					Pool.dbm.SaveChanges();
				}
			}
			return RedirectToAction("Index", "Groups");
		}

		public ActionResult CloseGroup(string groupToken)
		{
			if (Pool.dbm.Groups != null)
			{
				GroupItem? group = Pool.dbm.Groups?.Where(x => x.token == groupToken).FirstOrDefault();
				if (group != null && group.owner == UserId)
				{
					group.active = 0;
					Pool.dbm.SaveChanges();
				}
			}
			return RedirectToAction("Index", "Groups");
		}

		public ActionResult OpenGroup(string groupToken)
		{
			if (Pool.dbm.Groups != null)
			{
				GroupItem? group = Pool.dbm.Groups?.Where(x => x.token == groupToken).FirstOrDefault();
				if (group != null && group.owner == UserId)
				{
					group.active = 1;
					Pool.dbm.SaveChanges();
				}
			}
			return RedirectToAction("Index", "Groups");
		}
	}
}
