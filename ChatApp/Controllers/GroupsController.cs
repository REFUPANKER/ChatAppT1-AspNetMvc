using ChatApp.DBM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
	[Authorize]
	public class GroupsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public int UserId { get => Convert.ToInt32(User.Claims?.Where(x => x.Type.ToString() == "UserId").FirstOrDefault()?.Value); }
		public IActionResult AddHub(string hubName, int global)
		{
			Pool.dbm.AddGroup(UserId, hubName, global);
			return RedirectToAction("Index");
		}
		public IActionResult JoinGroup(string groupToken)
		{
			Pool.dbm.JoinGroup(UserId, groupToken);
			return RedirectToAction("Index");
		}
		public IActionResult LeaveGroup(string groupToken)
		{
			Pool.dbm.LeaveGroup(UserId, groupToken);
			return RedirectToAction("Index");
		}
	}
}
