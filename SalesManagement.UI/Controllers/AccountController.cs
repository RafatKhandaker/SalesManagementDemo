
using System.Web.Mvc;
using SalesManagement.Models;
using SalesManagement.BLL.Contracts;

namespace SalesManagement.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDBService _DBService;


        public AccountController
        (
            IDBService _DBService
        )
        {
            this._DBService = _DBService;
        }

        [HttpGet]
        public ActionResult Transactions()
        {
            var user = Session["UserId"].ToString();

            return (_DBService.CheckAdminAccount( user )) ? 
                        View(_DBService.RetrieveAllTransactions()) : 
                        View(_DBService.RetrieveUserTransactions( user )
                        );
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

            return PaymentSuccess();
        }

        [HttpGet]
        public ActionResult PaymentSuccess() { return View(); }
    }
}