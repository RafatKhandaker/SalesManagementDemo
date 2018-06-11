using SalesManagement.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesManagement.UI.Controllers
{

    public class HomeController : Controller
    {
        private IDBService dbService;
        private bool isAdmin;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Transactions()
        {
            var user = 1;
            isAdmin = true;
            //  return (isAdmin)? View( dbService.RetrieveAllTransactions()) : View ( dbService.RetrieveUserTransactions( user ));

            return View();
        }
    }
}