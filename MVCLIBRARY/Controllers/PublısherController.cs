using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;


namespace MVCLIBRARY.Controllers
{
    public class PublısherController : Controller
    {
        // GET: Publısher
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult Index(string p)
        {
            var kategori = from k in db.TBL_PUBLISHER select k;

            if (!string.IsNullOrEmpty(p))
            {

                kategori = kategori.Where(m => m.NAME.Contains(p));

            }
            return View(kategori.ToList());
        }
        [HttpGet]
        // sayfa yüklendiğinde çalışırı ve sadece sayfayı gösterir
        //böylece boşluk olarak eklemeler engellenir
        public ActionResult PublısherAdd()
        {
            return View();
        }
        [HttpPost]
        // bir işlem yapıldığında sayfaya ekler
        public ActionResult PublısherAdd(TBL_PUBLISHER p)
        {
            db.TBL_PUBLISHER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PublısherDelete(int id)
        {
            var kategori = db.TBL_PUBLISHER.Find(id);
            db.TBL_PUBLISHER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PublısherGet(int id)
        {
            var ktg = db.TBL_PUBLISHER.Find(id);
            return View("PublısherGet", ktg);
        }
        public ActionResult PublısherUpdate(TBL_PUBLISHER p)
        {
            var ktg = db.TBL_PUBLISHER.Find(p.ID);
            ktg.NAME = p.NAME;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}