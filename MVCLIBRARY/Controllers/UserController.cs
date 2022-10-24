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

    public class UserController : Controller
    {
        // GET: User
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult Index(string p,int sayfa = 1)
        {
            var degerler = db.TBL_USER.ToList();
            //sayfa değerinden başlasın ve sayfada 3 tane değer göstersin

            var uyeler = from k in db.TBL_USER select k;

            if (!string.IsNullOrEmpty(p))
            {
                uyeler = uyeler.Where(m => m.NAME.Contains(p) || m.SURNAME.Contains(p)
                || m.MAIL.Contains(p) || m.ADDRESS.Contains(p) || m.USERNAME.Contains(p)
                || m.SCHOOL.Contains(p)
                );

                return View(uyeler.ToList().ToPagedList(sayfa, 3));
            }
          
            var sayfalama = degerler.ToPagedList(sayfa, 3);
            return View(sayfalama);
        }
        [HttpGet]
        public ActionResult UserAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserAdd(TBL_USER p)
        {
            if (!ModelState.IsValid)
            {
                return View("UserAdd");
            }

            db.TBL_USER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserDelete(int id)
        {
            var uye = db.TBL_USER.Find(id);
            db.TBL_USER.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserGet(int id)
        {
            var usr = db.TBL_USER.Find(id);
            return View("UserGet", usr);
        }
        public ActionResult UserUpdate(TBL_USER p)
        {
            var uye = db.TBL_USER.Find(p.ID);
            uye.NAME = p.NAME;
            uye.SURNAME = p.SURNAME;
            uye.MAIL = p.MAIL;
            uye.TELEPHONENUM = p.TELEPHONENUM;
            uye.SCHOOL = p.SCHOOL;
            uye.USERIDENTITY = p.USERIDENTITY;
            uye.USERNAME = p.USERNAME;
            uye.PASSWORD = p.PASSWORD;
            uye.PHOTOGRAPH = p.PHOTOGRAPH;
            uye.ADDRESS = p.ADDRESS;
            

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserHistory(int id)
        {
            var ktpgcms = db.TBL_DEPOSIT.Where(x => x.USERID == id).ToList();
            var uye = db.TBL_USER.Where(y => y.ID == id).
                Select(z => z.NAME + " " + z.SURNAME).FirstOrDefault();
            ViewBag.u1 = uye;
            return View(ktpgcms);
        }
    }
}