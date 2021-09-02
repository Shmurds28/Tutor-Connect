using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutor_Connect.Models;
using System.Data.Entity;
using System.Data.OleDb;
using System.Configuration;
using System.Web.Security;


namespace Tutor_Connect.Controllers
{
    public class UserController : Controller
    {
        DataModel db = new DataModel();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //GET: User/Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: User/Login
        [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            try
            {
                // TODO: Add insert logic here

                var getUser = db.Users.Where(n => n.username == user.username && n.password == user.username).FirstOrDefault();


                if (getUser != null)
                {
                   

                    FormsAuthentication.SetAuthCookie(user.username, true);
                    Session["logged"] = true;

                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["logged"] = null;
                    ViewBag.msg = "Invalid username/Password";
                    return RedirectToAction("Login");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["logged"] = null;
            return RedirectToAction("Login");
        }


        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


  
    }
}
