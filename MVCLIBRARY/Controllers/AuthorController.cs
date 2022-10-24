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
    public class AuthorController : Controller
    {
        // GET: Author
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        //public ActionResult Index(string p)
        //{
        //    var yazar = from k in db.TBL_AUTHOR select k;

        //    if (!string.IsNullOrEmpty(p))
        //    {
        //        yazar = yazar.Where(m => m.NAME.Contains(p) || m.SURNAME.Contains(p));

        //    }
        //    return View(yazar.ToList());
        //    //var degerler = db.TBL_AUTHOR.ToList();
        //    //return View(degerler);
        //}
        public ActionResult Index(string p, int sayfa = 1)
        {
            var degerler = db.TBL_AUTHOR.ToList();
            //sayfa değerinden başlasın ve sayfada 3 tane değer göstersin

            var yazar = from k in db.TBL_AUTHOR select k;

            if (!string.IsNullOrEmpty(p))
            {
                yazar = yazar.Where(m => m.NAME.Contains(p) || m.SURNAME.Contains(p));

                return View(yazar.ToList().ToPagedList(sayfa, 10));
            }

            var sayfalama = degerler.ToPagedList(sayfa, 10);
            return View(sayfalama);
        }
        [HttpGet]
        public ActionResult AuthorAdd()
        {
            return View();
        }

        public ActionResult AuthorAdd(TBL_AUTHOR p)
        {
            db.TBL_AUTHOR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AuthorDelete(int id)
        {
            var yzr = db.TBL_AUTHOR.Find(id);
            db.TBL_AUTHOR.Remove(yzr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AuthorGet(int id)
        {
            var yzr = db.TBL_AUTHOR.Find(id);
            return View("AuthorGet", yzr);
        }
        public ActionResult AuthorUpdate(TBL_AUTHOR y)
        {
            var yzr = db.TBL_AUTHOR.Find(y.ID);
            yzr.NAME = y.NAME;
            yzr.SURNAME = y.SURNAME;
            yzr.DETAIL = y.DETAIL;
            db.SaveChanges();
            return RedirectToAction("Index");


        }
        public ActionResult AuthorBook(int id)
        {
            var yazar = db.TBL_BOOK.Where(x => x.AUTHORID == id).ToList();
            var yzrad = db.TBL_AUTHOR.Where(x => x.ID == id).
                Select(z => z.NAME + " " + z.SURNAME).FirstOrDefault();
            ViewBag.y1 = yzrad;

            return View(yazar);
        }
    }
}