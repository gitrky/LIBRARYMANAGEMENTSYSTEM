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
    public class ProcessController : Controller
    {
        // GET: Process
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        //public ActionResult Index()
        //{
        //    var degerler = db.TBL_DEPOSIT.Where(x => x.TRANSACTIONSTATUS == true).ToList();
        //    return View(degerler);
        //}
        public ActionResult Index(string p, int sayfa = 1)
        {
            var degerler = db.TBL_DEPOSIT.Where(x => x.TRANSACTIONSTATUS == true).ToList();
            //sayfa değerinden başlasın ve sayfada 3 tane değer göstersin

            var pro = from k in db.TBL_DEPOSIT select k;

            if (!string.IsNullOrEmpty(p))
            {
                pro = pro.Where(m => m.TBL_BOOK.NAME.Contains(p) || m.TBL_USER.NAME.Contains(p)
                || m.TBL_EMPLOYEE.NAME.Contains(p) || m.TBL_LIBRARY.NAME.Contains(p));

                return View(pro.ToList().ToPagedList(sayfa, 10));
            }


            var sayfalama = degerler.ToPagedList(sayfa, 10);
            return View(sayfalama);
}
    }
}