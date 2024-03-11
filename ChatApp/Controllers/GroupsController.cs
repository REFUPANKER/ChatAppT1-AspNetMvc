using ChatApp.DBM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatApp.Controllers
{
	[Authorize]
	public class GroupsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
