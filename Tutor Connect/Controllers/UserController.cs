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
            return View(db.Users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Profile()
        {
            if (Session["logged"] == null)
                return RedirectToAction("Login");

            int id = Convert.ToInt32(Session["LoggedUserId"]);
            User user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            Student studentToEdit = db.Students.Find(user.username);
            if (studentToEdit == null)
                return RedirectToAction("Index");
            return View(studentToEdit);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["logged"] == null)
                return RedirectToAction("Login");

            //int id = Convert.ToInt32(Session["LoggedUserId"]);
            //User user = db.Users.Find(id);
            //if (user == null)
            //    return HttpNotFound();

            //Student studentToEdit = db.Students.Find(user.username);
            //if (studentToEdit == null)
            //    return RedirectToAction("Login");

            return RedirectToAction("Edit", "Student");
        }

        //GET: User/Register
        public ActionResult Register()
        {
            return RedirectToAction("Register", "Student");
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

                var getUser = db.Users.Where(n => n.username == user.username && n.password == user.password).FirstOrDefault();


                if (getUser != null)
                {
                   

                    FormsAuthentication.SetAuthCookie(user.username, true);
                    Session["logged"] = true;
                    Session["LoggedUserId"] = getUser.userId.ToString();

                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["logged"] = null;
                    Session["LoggedUserId"] = -1;
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
            Session["LoggedUserId"] = -1;
            return RedirectToAction("Login");
        }

  
    }
}
