using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginMVC.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult TransferAmt()
        {
            // Money transfer logic goes here  
            return Content(Request.Form["amt"] + " has been transferred to account " + Request.Form["act"]);
        }
    }
}
