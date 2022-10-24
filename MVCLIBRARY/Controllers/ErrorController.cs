using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLIBRARY.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [AllowAnonymous]
        public ActionResult Page400()
        {
            Response.StatusCode = 400;//hata sayfası kodu
            Response.TrySkipIisCustomErrors = true;//hata sayfasının gelmesi için
            return View();
        }

        public ActionResult Page403()
        {
            Response.StatusCode = 403;//hata sayfası kodu
            Response.TrySkipIisCustomErrors = true;//hata sayfasının gelmesi için
            return View();
        }

        public ActionResult Page404()
        {
            Response.StatusCode = 404;//hata sayfası kodu
            Response.TrySkipIisCustomErrors = true;//hata sayfasının gelmesi için
            return View();
        }
    }
}