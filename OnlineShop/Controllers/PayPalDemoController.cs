using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using OnlineShop.PayPal;

namespace OnlineShop.Controllers
{
    public class PayPalDemoController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Success()
        {
            ViewBag.result = PDTHolder.Success(Request.QueryString.Get("tx"));
            return View("Success");
        }

    }
}
