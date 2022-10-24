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
    public class NotificationController : Controller
    {
        // GET: Notification
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        //public ActionResult Index()
        //{
        //    var degerler = db.TBL_NOTIFICATION.ToList();
        //    return View(degerler);
        //}
        public ActionResult Index(string p, int sayfa = 1)
        {
            var degerler = db.TBL_NOTIFICATION.ToList();
            //sayfa değerinden başlasın ve sayfada 3 tane değer göstersin

            var duyuru = from k in db.TBL_NOTIFICATION select k;

            if (!string.IsNullOrEmpty(p))
            {
                duyuru = duyuru.Where(m => m.CATEGORY.Contains(p) || m.CONTENTS.Contains(p));

                return View(duyuru.ToList().ToPagedList(sayfa, 10));
            }

            var sayfalama = degerler.ToPagedList(sayfa, 10);
            return View(sayfalama);
        }
        [HttpGet]
        public ActionResult NotificationAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NotificationAdd(TBL_NOTIFICATION n)
        {
            db.TBL_NOTIFICATION.Add(n);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NotificationDelete(int id)
        {
            var duyuru = db.TBL_NOTIFICATION.Find(id);
            db.TBL_NOTIFICATION.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NotificationDetail(TBL_NOTIFICATION n)
        {
            var duyuru = db.TBL_NOTIFICATION.Find(n.ID);
            return View("NotificationDetail", duyuru);
        }
        public ActionResult NotificationUpdate(TBL_NOTIFICATION n)
        {
            var duyuru = db.TBL_NOTIFICATION.Find(n.ID);
            duyuru.CATEGORY = n.CATEGORY;
            duyuru.CONTENTS = n.CONTENTS;
            duyuru.DATE = n.DATE;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}