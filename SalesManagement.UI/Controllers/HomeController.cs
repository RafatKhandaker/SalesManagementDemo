using SalesManagement.BLL;
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
        bool isAdmin;

        private readonly IDBService _DBService;

        
        public HomeController
        (
            IDBService _DBService
        )
        {
            this._DBService = _DBService;
        }


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
            isAdmin = true;
            return (isAdmin)? View(_DBService.RetrieveAllTransactions() ) : View(_DBService.RetrieveUserTransactions(0));
        }
    }
}