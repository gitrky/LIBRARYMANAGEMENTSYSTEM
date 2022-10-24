using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;
namespace MVCLIBRARY.Controllers
{
    public class ConfigController : Controller
    {
        // GET: Config
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult Index()
        {
            var kullanicilar = db.TBL_EMPLOYEE.ToList();
            return View(kullanicilar);
        }
        //public ActionResult Index2()
        //{
        //    var kullanicilar = db.TBL_EMPLOYEE.ToList();
        //    return View(kullanicilar);
        //}
        [HttpGet]
        public ActionResult NewEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewEmployee(TBL_EMPLOYEE e)
        {
            db.TBL_EMPLOYEE.Add(e);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteEmployee(int id)
        {
            var per = db.TBL_EMPLOYEE.Find(id);
            db.TBL_EMPLOYEE.Remove(per);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetEmployee(int id)
        {
            var per = db.TBL_EMPLOYEE.Find(id);
            return View("GetEmployee",per);
        }
        public ActionResult UpdateEmployee(TBL_EMPLOYEE p)
        {
            var prs = db.TBL_EMPLOYEE.Find(p.ID);
            prs.NAME = p.NAME;
            prs.SURNAME = p.SURNAME;
            prs.MAIL = p.MAIL;
            prs.TELEPHONENUM = p.TELEPHONENUM;
            prs.ADDRESS = p.ADDRESS;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}