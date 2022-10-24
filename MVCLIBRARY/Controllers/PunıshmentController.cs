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
    public class PunıshmentController : Controller
    {
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        // GET: Punıshment
        //public ActionResult Index()
        //{
        //    var degerler = db.TBL_PUNISHMENT.ToList();
        //    return View(degerler);
        //}
        public ActionResult Index(string p, int sayfa = 1)
        {
            var degerler = db.TBL_PUNISHMENT.Where(k=>k.PRICE >0 ).ToList();
            //sayfa değerinden başlasın ve sayfada 3 tane değer göstersin

            var ceza = from k in db.TBL_PUNISHMENT select k;

            if (!string.IsNullOrEmpty(p))
            {
                ceza = ceza.Where(m => m.TBL_USER.NAME.Contains(p));

                return View(ceza.ToList().ToPagedList(sayfa, 10));
            }

            var sayfalama = degerler.ToPagedList(sayfa, 10);
            return View(sayfalama);
        }
        public ActionResult PunıshmentGet(int id)
        {
            var pget = db.TBL_PUNISHMENT.Find(id);

            return View("PunıshmentGet",pget);
        }
        public ActionResult PunıshmentTill(TBL_PUNISHMENT p)
        {

            var dps = db.TBL_PUNISHMENT.Find(p.ID);
            if (p.PRICE > 0) { 
            dps.STATUS = true;
                db.SaveChanges();
            }
            
           
            return RedirectToAction("Index");
        }
    }
}