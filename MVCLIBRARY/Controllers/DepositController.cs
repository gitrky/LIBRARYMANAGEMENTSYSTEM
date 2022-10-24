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
    public class DepositController : Controller
    {
        // GET: Deposit
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        //public ActionResult Index()
        //{
        //   // var degerler = db.TBL_DEPOSIT.ToList();
        //    var degerler = db.TBL_DEPOSIT.Where(x => x.TRANSACTIONSTATUS == false).ToList();
        //    return View(degerler);
        //}
        public ActionResult Index(string p, int sayfa = 1)
        {
            var degerler = db.TBL_DEPOSIT.Where(x => x.TRANSACTIONSTATUS == false).ToList();
            //sayfa değerinden başlasın ve sayfada 3 tane değer göstersin

            var ktp = from k in db.TBL_DEPOSIT select k;

            if (!string.IsNullOrEmpty(p))
            {
                ktp = ktp.Where(m => m.TBL_BOOK.NAME.Contains(p) || m.TBL_USER.NAME.Contains(p) 
                ||m.TBL_EMPLOYEE.NAME.Contains(p) || m.TBL_LIBRARY.NAME.Contains(p) );

                return View(ktp.ToList().ToPagedList(sayfa, 10));
            }

            var sayfalama = degerler.ToPagedList(sayfa, 10);
            return View(sayfalama);
        }
        [HttpGet]
        public ActionResult DepositGive()
        {
            List<SelectListItem> deger1 = (from i in db.TBL_USER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME +" "+ i.SURNAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from i in db.TBL_BOOK.Where(x=>x.STATUS==true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            List<SelectListItem> deger3 = (from i in db.TBL_EMPLOYEE.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME + " " + i.SURNAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            List<SelectListItem> deger4 = (from i in db.TBL_LIBRARY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr4 = deger4;
            return View();
        }
        [HttpPost]
        public ActionResult DepositGive(TBL_DEPOSIT p)
        {
            var usr = db.TBL_USER.Where(k => k.ID == p.TBL_USER.ID).FirstOrDefault();
            var ktp = db.TBL_BOOK.Where(k => k.ID == p.TBL_BOOK.ID).FirstOrDefault();
            var prs = db.TBL_EMPLOYEE.Where(k => k.ID == p.TBL_EMPLOYEE.ID).FirstOrDefault();
            var lıb = db.TBL_LIBRARY.Where(k => k.ID == p.TBL_LIBRARY.ID).FirstOrDefault();

            p.TBL_USER = usr;
            p.TBL_BOOK = ktp;
            p.TBL_EMPLOYEE = prs;
            p.TBL_LIBRARY = lıb;
            db.TBL_DEPOSIT.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepositBack(TBL_DEPOSIT p)
        {
            //var back = db.TBL_DEPOSIT.Find(id);
            var back = db.TBL_DEPOSIT.Find(p.ID);
            DateTime d1 = DateTime.Parse(back.ENDTIME.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
           
            TimeSpan d3 = d2 - d1;

            ViewBag.dgr = d3.TotalDays;

            return View("DepositBack", back);
        }
        public ActionResult DepositUpdate(TBL_DEPOSIT p)
        {
            var dps = db.TBL_DEPOSIT.Find(p.ID);
            dps.USERGETTIME = p.USERGETTIME;
            dps.TRANSACTIONSTATUS= true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}