using ChatApp.DBM;
using ChatApp.DBM.Items;
using ChatApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ChatApp.Controllers
{
	public class AuthController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginModel lm)
		{
			if (lm.password == null || lm.email == null)
			{
				ModelState.AddModelError("email", "required");
				ModelState.AddModelError("password", "required");
				return View();
			}
			UserItem? user = Pool.dbm.Users?.Select(x => x).Where(x => x.email == lm.email && x.password == lm.password).FirstOrDefault();
			if (user != null)
			{
				InitCookie(user);
				return RedirectToAction("Index", "Groups");
			}
			else
			{
				ModelState.AddModelError("success", "required");
			}
			return View();
		}

		public IActionResult CreateAccount()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateAccount(CreateAccountModel cam)
		{
			if (cam.email == null || cam.name == null || cam.password == null)
			{
				ModelState.AddModelError("email", "required");
				ModelState.AddModelError("password", "required");
				ModelState.AddModelError("name", "required");
				return View();
			}
			string? user = Pool.dbm.Users?.Select(x => x.email).Where(x => x == cam.email).FirstOrDefault();
			if (string.IsNullOrEmpty(user))
			{
				UserItem newuser = new UserItem() { name = cam.name, email = cam.email, password = cam.password };
				Pool.dbm.Users?.Add(newuser);
				Pool.dbm.SaveChanges();
				InitCookie(newuser);
				return RedirectToAction(actionName: "Index", controllerName: "Groups");
			}
			else
			{
				ModelState.AddModelError("success", "required");
			}
			return View();
		}

		private void InitCookie(UserItem user)
		{
			HttpContext.SignOutAsync();
			var claims = new List<Claim>() {
			new Claim("UserId",user.id.ToString()),
			};
			var claimsIdentify = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
												  new ClaimsPrincipal(claimsIdentify),
												  new AuthenticationProperties());
			RedirectToAction("Index", "Groups");
		}

		public ActionResult Logout()
		{
			HttpContext.SignOutAsync();
			return RedirectToAction("Index");
		}


		public JsonResult? GetUserById(int id)
		{
			UserItem? uit = Pool.dbm.Users?.Select(x => new UserItem() { id = x.id, name = x.name, token = x.token }).Where(x => x.id == id)?.FirstOrDefault();
			return Json(new { uit?.id, uit?.name, uit?.token });
		}
	}
}
