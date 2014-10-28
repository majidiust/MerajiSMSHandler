using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerajiSMS.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        MerajiSMS.Models.SMSDataContext db = new Models.SMSDataContext();
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetSms(string to, string body, string from)
        {
            try
            {
                MerajiSMS.Models.SM sms = new MerajiSMS.Models.SM();
                if (from[0] == '0')
                    from = from.Substring(1);
                sms.From = from;
                sms.To = to;
                sms.Msg = body;
                sms.Status = 0;
                sms.Date = DateTime.Now;
                db.SMs.InsertOnSubmit(sms);
                db.SubmitChanges();
                return Json(new { Status = true, Message = "Sms sent to Inbox" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
