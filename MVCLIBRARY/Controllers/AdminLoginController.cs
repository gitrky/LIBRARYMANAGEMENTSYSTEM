using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCLIBRARY.Models.Entity;
namespace MVCLIBRARY.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(TBL_EMPLOYEE e)
        {
            var bilgiler = db.TBL_EMPLOYEE.FirstOrDefault(x => x.MAIL == e.MAIL && x.PASSWORD == e.PASSWORD);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                Session["MAIL"] = bilgiler.MAIL.ToString();
                return RedirectToAction("LinqCard", "Statistics");
            }
            else
            {
                return View();
            }
            
        }

    }
}