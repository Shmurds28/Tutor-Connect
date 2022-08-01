using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutor_Connect.Controllers;
using Tutor_Connect.Models;
using System.Web.Security;

namespace Tutor_Connect.Controllers
{
    public class AdminController : Controller
    {
        private DataModel model = new DataModel();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Report()
        {

            ViewData["department"] = " successfully added!";

            return View();
        }


        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create

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

                var getUser = model.Users.Where(n => n.username == user.username && n.password == user.password).FirstOrDefault();


                if (getUser != null)
                {


                    FormsAuthentication.SetAuthCookie(user.username, true);
                    Session["logged"] = true;
                    Session["LoggedUserId"] = getUser.userId.ToString();

                    if (getUser.type.ToString() == "tutor     ")
                        Session["isTutor"] = true;

                    if (getUser.type.ToString() == "admin     ")
                        Session["isAdmin"] = true;

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
        public ActionResult AddDepartment()
        {



            return View();
        }
        // POST: Admin/Create
        [HttpPost]
        public ActionResult AddDepartment([Bind(Exclude = "Id")] Department tutorToCreate, string deptname)
        {
            try
            {


                // TODO: Add insert logic here
                Department cur = model.Departments.Where(x => x.deptName == deptname).FirstOrDefault();

                ViewData["deptment"] = "Failed already exist!";
                model.Departments.Add(tutorToCreate);
                model.SaveChanges();
                ModelState.Clear();


                return RedirectToAction("Report");


            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddModule()
        {
            var getSession = model.Departments.ToList();
            MultiSelectList list = new MultiSelectList(getSession, "depId", "depId");
            ViewBag.SelectSession = list;
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult AddModule(Module tutorToCreate, string name)

        {

            try
            {

                // TODO: Add insert logic here
                model.Modules.Add(tutorToCreate);
                model.SaveChanges();
                return RedirectToAction("Report");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ApproveTutor(string id)
        {
            //Tutor cur = model.Tutors.Where(x => x.TutorId == Convert.ToInt32(System.Web.HttpContext.Current.Request.Form[id])).FirstOrDefault();
            //return View(cur);
            TutorModule cur = model.TutorModules.Where(x => x.studNumber == id).FirstOrDefault();
            return View(cur);
        }
        [HttpPost]
        public ActionResult ApproveTutor([Bind(Exclude = "Id")] TutorModule newT, int id)
        {
            try
            {
                if (newT.passmark >= 70)
                {



                    Tutor newOne = new Tutor();

                    newOne.TutorId = id;
                    newOne.tutorType = "Volunteer";
                    model.Tutors.Add(newOne);

                    model.SaveChanges();
                    ModelState.Clear();

                    Applicant cur = model.Applicants.Where(x => x.studNumber == id.ToString()).FirstOrDefault();
                    model.Applicants.Remove(cur);

                    model.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("ListTutors");
                }
                else
                {
                    ViewData["Invalid"] = "Does not meet requirements";
                    return View();
                }


            }
            catch
            {
                return View("Index");
            }

        }


        public ActionResult ListTutors()
        {
            return View(model.Applicants.ToList());
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
