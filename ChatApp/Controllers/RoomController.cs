using ChatApp.DBM;
using ChatApp.DBM.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
	[Authorize]
    public class RoomController : Controller
    {
		public IActionResult Index(string token)
        {
            GroupItem? getgroup = Pool.dbm.Groups?.Where(x => x.token == token).FirstOrDefault();
            if (getgroup != null && Pool.dbm.GroupsOfUser?.Select(x => x).Where(x => x.groupId == getgroup.id).FirstOrDefault() != null)
            {
                return View(getgroup);
            }
            else
            {
                return RedirectToAction("Index","ClosedRoom");
            }
        }
    }
}
