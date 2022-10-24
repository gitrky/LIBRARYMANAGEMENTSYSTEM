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
    public class BookController : Controller
    {
        // GET: Book
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        //public ActionResult Index(string p)
        //{
        //    //arama tuşuna yazılanlara göre dbden veri listesini çeker ve sunar
        //    var kitaplar = from k in db.TBL_BOOK select k;

        //    if (!string.IsNullOrEmpty(p))
        //    {
        //        kitaplar = kitaplar.Where(m => m.NAME.Contains(p) || m.TBL_LIBRARY.NAME.Contains(p)
        //        || m.TBL_LANGUAGE.NAME.Contains(p) || m.TBL_AUTHOR.NAME.Contains(p)
        //        || m.TBL_PUBLISHER.NAME.Contains(p) || m.TBL_CATEGORY.NAME.Contains(p));
        //        //kitaplar = kitaplar.Where(m => m.TBL_LIBRARY.NAME.Contains(p));
        //    }

        //    //var kitaplar = db.TBL_BOOK.ToList();
        //    return View(kitaplar.ToList());
        //}
        public ActionResult Index(string p, int sayfa = 1)
        {
            var degerler = db.TBL_BOOK.ToList();
            //sayfa değerinden başlasın ve sayfada 3 tane değer göstersin

            var kitaplar = from k in db.TBL_BOOK select k;

            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m => m.NAME.Contains(p) || m.TBL_LIBRARY.NAME.Contains(p)
               //        || m.TBL_LANGUAGE.NAME.Contains(p) || m.TBL_AUTHOR.NAME.Contains(p)
               //        || m.TBL_PUBLISHER.NAME.Contains(p) || m.TBL_CATEGORY.NAME.Contains(p));
               );

                return View(kitaplar.ToList().ToPagedList(sayfa, 10));
            }

            var sayfalama = degerler.ToPagedList(sayfa, 10);
            return View(sayfalama);
        }
        [HttpGet]
        public ActionResult BookAdd()
        {
            // sayfa yüklendiğinde içinde veri bulunmasını sağlıyor
            //liste şeklnde sunuyor kategori listesini
            List<SelectListItem> deger1 = (from i in db.TBL_CATEGORY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBL_AUTHOR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME + ' ' + i.SURNAME,
                                               Value = i.ID.ToString()

                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from i in db.TBL_PUBLISHER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()

                                           }).ToList();
            ViewBag.dgr3 = deger3;

            List<SelectListItem> deger4 = (from i in db.TBL_LANGUAGE.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()

                                           }).ToList();
            ViewBag.dgr4 = deger4;

            List<SelectListItem> deger5 = (from i in db.TBL_LIBRARY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()

                                           }).ToList();
            ViewBag.dgr5 = deger5;
            return View();
        }

        [HttpPost]
        public ActionResult BookAdd(TBL_BOOK p)
        {
            var ktg = db.TBL_CATEGORY.Where(k => k.ID == p.TBL_CATEGORY.ID).FirstOrDefault();
            var yzr = db.TBL_AUTHOR.Where(y => y.ID == p.TBL_AUTHOR.ID).FirstOrDefault();
            var pbl = db.TBL_PUBLISHER.Where(pb => pb.ID == p.TBL_PUBLISHER.ID).FirstOrDefault();
            var lng = db.TBL_LANGUAGE.Where(l => l.ID == p.TBL_LANGUAGE.ID).FirstOrDefault();
            var lbr = db.TBL_LIBRARY.Where(lb => lb.ID == p.TBL_LIBRARY.ID).FirstOrDefault();

            p.TBL_CATEGORY = ktg;
            p.TBL_AUTHOR = yzr;
            p.TBL_PUBLISHER = pbl;
            p.TBL_LANGUAGE = lng;
            p.TBL_LIBRARY = lbr;

            db.TBL_BOOK.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult BookDelete(int id)
        {
            var kitap = db.TBL_BOOK.Find(id);
            db.TBL_BOOK.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BookGet(int id)
        {
            var ktp = db.TBL_BOOK.Find(id);
            //liste şeklnde sunuyor kategori listesini
            //TABLODAKİ DEĞERLERİ i çekiyor text kısmına ismini atıyor değere çekilen değerin ıdsi atanıyor
            //Hepsi liste olarak sunulmuş oluyor
            //BookGet viewında dropdownlistfor yerine alan kullanılırsa liste çıkmıyor manuel bilgi girişi istiyor. yanlış bilgi girilirse hata veriyor. 
            List<SelectListItem> deger1 = (from i in db.TBL_CATEGORY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBL_AUTHOR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME + ' ' + i.SURNAME,
                                               Value = i.ID.ToString()

                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from i in db.TBL_PUBLISHER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;

            List<SelectListItem> deger4 = (from i in db.TBL_LANGUAGE.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr4 = deger4;
            List<SelectListItem> deger5 = (from i in db.TBL_LIBRARY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.NAME,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr5 = deger5;

            return View("BookGet", ktp);
        }

        public ActionResult BookUpdate(TBL_BOOK k)
        {
            var ktp = db.TBL_BOOK.Find(k.ID);
            ktp.NAME = k.NAME;
            ktp.PRINTYEAR = k.PRINTYEAR;
            ktp.PAGE = k.PAGE;
            ktp.DESCRIPTION = k.DESCRIPTION;
            ktp.STATUS = true;



            var ktg = db.TBL_CATEGORY.Where(p => p.ID == k.TBL_CATEGORY.ID).FirstOrDefault();
            var yzr = db.TBL_AUTHOR.Where(y => y.ID == k.TBL_AUTHOR.ID).FirstOrDefault();
            var pbl = db.TBL_PUBLISHER.Where(x => x.ID == k.TBL_PUBLISHER.ID).FirstOrDefault();
            var lng = db.TBL_LANGUAGE.Where(l => l.ID == k.TBL_LANGUAGE.ID).FirstOrDefault();
            var lbr = db.TBL_LIBRARY.Where(lb => lb.ID == k.TBL_LIBRARY.ID).FirstOrDefault();

            ktp.CATEGORYID = ktg.ID;
            ktp.AUTHORID = yzr.ID;
            ktp.PUBLISHERID = pbl.ID;
            ktp.LANGUAGEID = lng.ID;
            ktp.TBL_LIBRARY = lbr;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult BookDescription(int id)
        {
            var ktp = db.TBL_BOOK.Find(id);


            return View("BookDescription", ktp);
        }
    }
}
