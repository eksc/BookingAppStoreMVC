using BookingAppStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookingAppStoreMVC.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext userContext = new UserContext())
                {
                    user = userContext.Users.FirstOrDefault(u => u.Email == model.Name);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Такого пользователя не сущетсвует");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext userContext = new UserContext())
                {
                    user = userContext.Users.FirstOrDefault(u => u.Email == model.Name);
                }

                if (user == null)
                {
                    using (UserContext userContext = new UserContext())
                    {
                        userContext.Users.Add(new User { Email = model.Name, Password = model.Password, Age = model.Age, RoleId = 2 });
                        userContext.SaveChanges();

                        user = userContext.Users.FirstOrDefault(u => u.Email == model.Name);
                    }

                    if (user != null)
                    {
                        //Аутентификационные куки
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким Email уже существует");
                }
            }

            return View(model);
        }
    }
}