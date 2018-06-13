
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
        public ActionResult PaymentForm( int pID )
        {
            return View( _DBService.BuildTransactinForm( pID, new Transaction() ));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentForm( Transaction newTransaction )
        {
            _DBService.SaveTransactionWorkFlow( newTransaction );
            _DBService.UpdateSalesRecord("admin@gmail.com");

            return View();
        }
    }
}