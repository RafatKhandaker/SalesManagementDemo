
using System.Web.Mvc;
using SalesManagement.Models;
using SalesManagement.BLL.Contracts;


namespace SalesManagement.UI.Controllers
{
    public class AccountController : Controller
    {
        bool isAdmin;

        private readonly IDBService _DBService;


        public AccountController
        (
            IDBService _DBService
        )
        {
            this._DBService = _DBService;
            isAdmin = true;
        }

        [HttpGet]
        public ActionResult Transactions()
        {
            return (isAdmin) ? View(_DBService.RetrieveAllTransactions()) : View(_DBService.RetrieveUserTransactions(0));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transactions(Transaction transaction)
        {
            return View();
        }

        [HttpGet]
        public ActionResult PaymentForm()
        {
            return View( new Transaction() );
        }

        [HttpPost]
        public ActionResult PaymentForm( Transaction newTransaction )
        {
            return View( newTransaction );
        }
    }
}