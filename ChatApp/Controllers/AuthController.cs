using ChatApp.DBM;
using ChatApp.DBM.Items;
using ChatApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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
			UserItem? user = Pool.dbm.Users?.Select(x => x).Where(x => x.email == lm.email && x.password == lm.password).FirstOrDefault();
			if (user != null)
			{
				InitCookie(user);
				return RedirectToAction("Index", "Groups");
			}
			if (lm.password==null)
			{
				ModelState.AddModelError("password", "password required");
				return View();
			}
			if (lm.email == null)
			{
				ModelState.AddModelError("email", "email required");
				return View();
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
			if (cam.email == null)
			{
				ModelState.AddModelError("email", "email required");
				return View();
			}
			if (cam.name == null)
			{
				ModelState.AddModelError("email", "email required");
				return View();
			}
			if (cam.password == null)
			{
				ModelState.AddModelError("email", "email required");
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
			return View(model: new UserItem());
		}

		private void InitCookie(UserItem user)
		{
			HttpContext.SignOutAsync();
			var claims = new List<Claim>() {
			new Claim("UserId",user.id.ToString()),
			new Claim("UserName",user.name),
			new Claim("UserEmail",user.email),
			new Claim("UserToken",user.token),
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
	}
}
