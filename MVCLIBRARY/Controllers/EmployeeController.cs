using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using MVCLIBRARY.Models.Entity;

namespace MVCLIBRARY.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        //public ActionResult Index(string p)
        //{
        //    var personeller = from k in db.TBL_EMPLOYEE select k;

        //    if (!string.IsNullOrEmpty(p))
        //    {
        //        personeller = personeller.Where(m => m.NAME.Contains(p) || m.SURNAME.Contains(p)
        //        || m.MAIL.Contains(p) || m.ADDRESS.Contains(p)
        //        || m.TELEPHONENUM.Contains(p) || m.TBL_LIBRARY.NAME.Contains(p));

        //    }
        //    return View(personeller.ToList());
        //    //var degerler = db.TBL_EMPLOYEE.ToList();
        //    //return View(degerler);
        //}
        public ActionResult Index(string p, int sayfa = 1)
        {
            var degerler = db.TBL_EMPLOYEE.ToList();
            //sayfa değerinden başlasın ve sayfada 3 tane değer göstersin

            var personel = from k in db.TBL_EMPLOYEE select k;

            if (!string.IsNullOrEmpty(p))
            {
                personel = personel.Where(m => m.NAME.Contains(p) || m.SURNAME.Contains(p)
                || m.MAIL.Contains(p) || m.ADDRESS.Contains(p)
                || m.TELEPHONENUM.Contains(p) || m.TBL_LIBRARY.NAME.Contains(p));
             

                return View(personel.ToList().ToPagedList(sayfa, 10));
            }

            var sayfalama = degerler.ToPagedList(sayfa, 10);
            return View(sayfalama);
        }
        [HttpGet]
        public ActionResult EmployeeAdd()
        {
            // sayfa yüklendiğinde içinde veri bulunmasını sağlıyor
            //liste şeklnde sunuyor kütüphane listesini
            List<SelectListItem> deger1 = (from i in db.TBL_LIBRARY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeAdd(TBL_EMPLOYEE p)
        {
            if (!ModelState.IsValid)
            {
                return View("EmployeeAdd");
            }

            var lbr = db.TBL_LIBRARY.Where(lb => lb.ID == p.TBL_LIBRARY.ID).FirstOrDefault();
            p.TBL_LIBRARY = lbr;

            db.TBL_EMPLOYEE.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EmployeeDelete(int id)
        {
            var personel = db.TBL_EMPLOYEE.Find(id);
            db.TBL_EMPLOYEE.Remove(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EmployeeGet(int id)
        {
            var prs = db.TBL_EMPLOYEE.Find(id);

            List<SelectListItem> deger1 = (from i in db.TBL_LIBRARY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            return View("EmployeeGet", prs);
        }
        public ActionResult EmployeeUpdate(TBL_EMPLOYEE p)
        {
            var prs = db.TBL_EMPLOYEE.Find(p.ID);
            prs.NAME = p.NAME;
            prs.SURNAME = p.SURNAME;
            prs.MAIL = p.MAIL;
            prs.TELEPHONENUM = p.TELEPHONENUM;
            prs.ADDRESS  = p.ADDRESS;
            prs.POWER = p.POWER;

            var lbr = db.TBL_LIBRARY.Where(l => l.ID == p.TBL_LIBRARY.ID).FirstOrDefault();
            prs.LIBRARYID = lbr.ID;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}