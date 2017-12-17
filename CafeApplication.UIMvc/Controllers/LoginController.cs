using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeApplication.Dal.Concrate;
using CafeApplication.UIMvc.Models;
using CafeApplication.DAL.Concrate;
using CafeApplication.Entities.Concrate;

namespace CafeApplication.UIMvc.Controllers
{
    public class LoginController : Controller
    {
        EFGarsonDal efGarson = new EFGarsonDal();

        // GET: Login
        public ActionResult Login()
        {
            int control = SessionControl();
            if (control == 0)
                return View();
            else
                return RedirectToAction("Masalar", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Login")]
        public ActionResult LoginConfirm(LoginModel lm)
        {
            int control = efGarson.Login(lm.Username, lm.Password);
            if (control == 1)
            {
                Session["Kadi"] = lm.Username;
                return RedirectToAction("Masalar", "Home");
            }
            else
            {
                ViewData["Msg"] = "Kullanıcı Adınız ve/veya Şifreniz hatalıdır.";
                return View();
            }
        }


        int SessionControl()
        {
            if (Session["Kadi"] != null)
                return 1;
            else
                return 0;
        }


    }
}