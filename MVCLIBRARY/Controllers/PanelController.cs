using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCLIBRARY.Models.Entity;


namespace MVCLIBRARY.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {

        LIBRARYDBEntities db = new LIBRARYDBEntities();
        // GET: Panel

        [HttpGet]
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            var uyeid = db.TBL_USER.Where(x => x.MAIL == uyemail).Select(y => y.ID).FirstOrDefault();
            var degerler = db.TBL_NOTIFICATION.ToList();
            // var degerler = db.TBL_USER.FirstOrDefault(x => x.MAIL == uyemail);

            var d1 = db.TBL_USER.Where(x => x.MAIL == uyemail).Select(y => y.NAME).FirstOrDefault();
            ViewBag.d1 = d1;

            var d2 = db.TBL_USER.Where(x => x.MAIL == uyemail).Select(y => y.SURNAME).FirstOrDefault();
            ViewBag.d2 = d2;

            var d3 = db.TBL_USER.Where(x => x.MAIL == uyemail).Select(y => y.PHOTOGRAPH).FirstOrDefault();
            ViewBag.d3 = d3;

            var d4 = db.TBL_USER.Where(x => x.MAIL == uyemail).Select(y => y.USERNAME).FirstOrDefault();
            ViewBag.d4 = d4;

            var d5 = db.TBL_USER.Where(x => x.MAIL == uyemail).Select(y => y.SCHOOL).FirstOrDefault();
            ViewBag.d5 = d5;

            var d6 = db.TBL_USER.Where(x => x.MAIL == uyemail).Select(y => y.TELEPHONENUM).FirstOrDefault();
            ViewBag.d6 = d6;

            var d7 = db.TBL_USER.Where(x => x.MAIL == uyemail).Select(y => y.MAIL).FirstOrDefault();
            ViewBag.d7 = d7;

            var d8 = db.TBL_DEPOSIT.Where(x => x.USERID == uyeid).Count();
            ViewBag.d8 = d8;

            var d9 = db.TBL_MESSAGE.Where(x => x.TAKER == uyemail).Count();
            ViewBag.d9 = d9;

            var d10 = db.TBL_NOTIFICATION.Count();
            ViewBag.d10 = d10;


            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index(TBL_USER p)
        {
            var kullanici = (string)Session["Mail"];
            var uye = db.TBL_USER.FirstOrDefault(x => x.MAIL == kullanici);

            uye.NAME = p.NAME;
            uye.SURNAME = p.SURNAME;
            uye.USERNAME = p.USERNAME;
            uye.PASSWORD = p.PASSWORD;
            uye.PHOTOGRAPH = p.PHOTOGRAPH;
            uye.SCHOOL = p.SCHOOL;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MyBooks()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBL_USER.Where(x => x.MAIL == kullanici.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.TBL_DEPOSIT.Where(x => x.USERID == id).ToList();
            return View(degerler);
        }
        public ActionResult Notifications()
        {
            var duyurulist = db.TBL_NOTIFICATION.ToList();
            return View(duyurulist);
        }
        //çıkış yapmayı sağlıyor
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Login");
        }
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        public PartialViewResult Partial2()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBL_USER.Where(x => x.MAIL == kullanici).Select(y => y.ID).FirstOrDefault();
            var uyebul = db.TBL_USER.Find(id);
            return PartialView("Partial2", uyebul);
        }
    }

}