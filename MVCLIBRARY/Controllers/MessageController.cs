using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;

namespace MVCLIBRARY.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult Index()
        {
            // var mesajlar = db.TBL_MESSAGE.ToList();

            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBL_MESSAGE.Where(x => x.TAKER == uyemail.ToString()).ToList();

            
            return View(mesajlar);
        }

        public ActionResult SentMessage()
        {

            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBL_MESSAGE.Where(x => x.SENDER == uyemail.ToString()).ToList();
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(TBL_MESSAGE m)
        {
            var uyemail = (string)Session["Mail"].ToString();
            m.SENDER = uyemail.ToString();
            m.DATE = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBL_MESSAGE.Add(m);
            db.SaveChanges();
            return View();
        }
        public PartialViewResult PartialMessage1()
        {

            var uyemail = (string)Session["Mail"].ToString();
            var d1 = db.TBL_MESSAGE.Where(x => x.TAKER == uyemail).Count();
            ViewBag.d1 = d1;

            var d2 = db.TBL_MESSAGE.Where(x => x.SENDER == uyemail).Count();
            ViewBag.d2 = d2;

            return PartialView();
        }
    }
}