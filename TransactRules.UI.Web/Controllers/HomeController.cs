using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransactRules.UI.Web.Models;

namespace TransactRules.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WithMasterPage()
        {
            NavigationModel.Current = MvcApplication.CreateTestNavigationModel();
            return View();
        }


    }
}
