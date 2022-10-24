using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;
using MVCLIBRARY.Models.MyClasses;
namespace MVCLIBRARY.Controllers
{
    [AllowAnonymous]
    public class WindowController : Controller
    {
       
        // GET: Window
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Deger1 = db.TBL_BOOK.ToList();
            cs.Deger2 = db.TBL_ABOUTME.ToList();
            //var degerler = db.TBL_BOOK.ToList();
            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(TBL_CONTACT c)
        {
            db.TBL_CONTACT.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}