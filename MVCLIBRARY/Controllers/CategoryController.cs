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
    public class CategoryController : Controller
    {
        // GET: Category
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        //public ActionResult Index(string p)
        //{
        //    var kategori = from k in db.TBL_CATEGORY.Where(x => x.STATUS == true) select k;

        //    if (!string.IsNullOrEmpty(p))
        //    {

        //        kategori = kategori.Where(m => m.NAME.Contains(p));

        //    }
        //    return View(kategori.ToList());
        //    //var degerler = db.TBL_CATEGORY.ToList();
        //    //return View(degerler);
        //}
        public ActionResult Index(string p, int sayfa = 1)
        {
            var degerler = db.TBL_CATEGORY.ToList();
            //sayfa değerinden başlasın ve sayfada 3 tane değer göstersin

            var kategori = from k in db.TBL_CATEGORY select k;

            if (!string.IsNullOrEmpty(p))
            {
                kategori = kategori.Where(m => m.NAME.Contains(p));
               

                return View(kategori.ToList().ToPagedList(sayfa, 10));
            }

            var sayfalama = degerler.ToPagedList(sayfa, 10);
            return View(sayfalama);
        }
        [HttpGet]
        // sayfa yüklendiğinde çalışırı ve sadece sayfayı gösterir
        //böylece boşluk olarak eklemeler engellenir
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]// bir işlem yapıldığında sayfaya ekler
        public ActionResult CategoryAdd(TBL_CATEGORY p)
        {
            db.TBL_CATEGORY.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CategoryDelete(int id)
        {
            var kategori = db.TBL_CATEGORY.Find(id);
            //db.TBL_CATEGORY.Remove(kategori);
            kategori.STATUS = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CategoryGet(int id)
        {
            var ktg = db.TBL_CATEGORY.Find(id);
            return View("CategoryGet", ktg);
        }
        public ActionResult CategoryUpdate(TBL_CATEGORY p)
        {
            var ktg = db.TBL_CATEGORY.Find(p.ID);
            ktg.NAME = p.NAME;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}