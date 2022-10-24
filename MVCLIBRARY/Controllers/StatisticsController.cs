using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;
namespace MVCLIBRARY.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult Index()
        {
            var deger = db.TBL_PUNISHMENT.Where(x=>x.PRICE>0).Sum(x => x.PRICE);
            var deger1 = db.TBL_USER.Count();
            var deger2 = db.TBL_BOOK.Count();
            var deger3 = db.TBL_BOOK.Where(x => x.STATUS == false).Count();
            ViewBag.dgr = deger;
            ViewBag.dgr1=deger1;
            ViewBag.dgr2=deger2;
            ViewBag.dgr3=deger3;
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PictureUpload(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
               
            }
            return RedirectToAction("Gallery");
        }
        public ActionResult LinqCard()
        {
            var deger1 = db.TBL_BOOK.Count();
            var deger2 = db.TBL_USER.Count();
            var deger3 = db.TBL_PUNISHMENT.Sum(x=>x.PRICE);
            var deger4 = db.TBL_BOOK.Where(x=>x.STATUS==false).Count();
            var deger5 = db.TBL_CATEGORY.Count();
            var deger8 = db.ENFAZLAKITAPYAZAR().FirstOrDefault();
            var deger9 = db.TBL_BOOK.GroupBy(x => x.TBL_PUBLISHER.NAME).OrderByDescending(z => z.Count()).Select(y=>y.Key  ).FirstOrDefault();
            var deger10 = db.TBL_CATEGORY.Count();
            var deger11 = db.TBL_CONTACT.Count();
            var deger12 = db.TBL_DEPOSIT.Count();
            var deger13 = db.TBL_DEPOSIT.GroupBy(x => x.TBL_BOOK.NAME).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            var deger14 = db.TBL_DEPOSIT.GroupBy(x => x.TBL_USER.NAME).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            var deger15 = db.TBL_DEPOSIT.GroupBy(x => x.TBL_EMPLOYEE.NAME).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();


            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            ViewBag.dgr5 = deger5;
            ViewBag.dgr8 = deger8;
            ViewBag.dgr9 = deger9;
            ViewBag.dgr10 = deger10;
            ViewBag.dgr11= deger11;
            ViewBag.dgr12= deger12;
            ViewBag.dgr13= deger13;
            ViewBag.dgr14= deger14;
            ViewBag.dgr15= deger15;
            return View();
        }
    }
}