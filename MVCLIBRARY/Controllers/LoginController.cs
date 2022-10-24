using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;
using System.Web.Security;
namespace MVCLIBRARY.Controllers
{
    //tüm sayfalar authorize alırken aşşağıdaki kod ile bu sayfayı muaf tutmuş olduk
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(TBL_USER p)
        {
            var bilgiler = db.TBL_USER.FirstOrDefault(x => x.MAIL == p.MAIL && x.PASSWORD == p.PASSWORD);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                Session["Mail"] = bilgiler.MAIL.ToString();
                Session["Photo"] = bilgiler.PHOTOGRAPH;
                



                //tampdata ile veri çekme
                //TempData["id"] = bilgiler.ID.ToString();
                //TempData["Ad"] = bilgiler.NAME.ToString();
                //TempData["Soyad"] = bilgiler.SURNAME.ToString();
                //TempData["KullanıcıAdı"] = bilgiler.USERNAME.ToString();
                //TempData["Sifre"] = bilgiler.PASSWORD.ToString();
                //TempData["Okul"] = bilgiler.SCHOOL.ToString();
                return RedirectToAction("Index", "Panel");
            }
            else
            {
                return View();
            }

        }

    }
}