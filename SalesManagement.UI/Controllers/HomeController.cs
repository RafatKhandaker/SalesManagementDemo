using SalesManagement.Data;
using SalesManagement.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace SalesManagement.UI.Controllers
{

    public class HomeController : Controller
    {
        SalesManagementDemoEntities _obj = new SalesManagementDemoEntities();

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
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(User _user)
        {
            if (ModelState.IsValid)
            {
                var Result = _obj.Users.Where(x => x.UserName == _user.UserName && x.PassWord == _user.PassWord).FirstOrDefault();
                if (Result != null)
                {
                    Session["UserId"] = Result.Id;
                    Session["UserName"] = Result.UserName;
                    // FormsAuthentication.SetAuthCookie(_user.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }



        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.RoleId = _obj.Roles;
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDetailsModel _UserDetailsModel)
        {
            if (ModelState.IsValid)
            {
                var UserTable = new User { UserName = _UserDetailsModel.UserName, PassWord = _UserDetailsModel.PassWord, RoleId = _UserDetailsModel.RoleId };

                var UserDetails = new User_Details();

                using (var context = _obj)
                {
                    context.Users.Add(UserTable);
                    context.SaveChanges();
                    // var id = _obj.Users.Where(x => x.UserName == _UserDetailsModel.UserName && x.PassWord == _UserDetailsModel.PassWord && x.RoleId == _UserDetailsModel.RoleId);

                    UserDetails.UserId = UserTable.Id;
                    UserDetails.FirstName = _UserDetailsModel.FirstName;
                    UserDetails.LastName = _UserDetailsModel.LastName;
                    UserDetails.Email = _UserDetailsModel.Email;

                    context.User_Details.Add(UserDetails);
                    //etc add your other classes
                    context.SaveChanges();
                    return RedirectToAction("Login", "Home");
                }
            }
            return View();
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }


    }
}