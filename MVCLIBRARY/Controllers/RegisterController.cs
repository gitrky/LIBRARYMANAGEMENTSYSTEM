using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;
namespace MVCLIBRARY.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
       

        // GET: Register
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        [HttpGet]
        public ActionResult Record()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Record(TBL_USER p)
        {
            if (!ModelState.IsValid)
            {
                return View("Record");
            }
            db.TBL_USER.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}