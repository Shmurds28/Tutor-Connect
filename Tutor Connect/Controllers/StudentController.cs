using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutor_Connect.Models;
using System.Web.Security;

namespace Tutor_Connect.Controllers
{
    public class StudentController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Student
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: Student/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Register([Bind(Exclude = "Id")] Student newStudent)
        {
            try
            {
                // TODO: Add insert logic here
                //newStudent.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(newStudent.Password, "MD5");

                var getStudent = db.Students.Where(n => n.StudNumber == newStudent.StudNumber).FirstOrDefault();

                if(getStudent  != null)
                {
                    ViewBag.NotAdded = "Student already registered. Login instead.";
                    return View();
                }

                db.Students.Add(newStudent);
                db.SaveChanges();

                User newUser = new User();
                newUser.username = newStudent.StudNumber;
                newUser.password = newStudent.Password;
                newUser.type = "student";
                db.Users.Add(newUser);
                db.SaveChanges();

                ModelState.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ViewBag.NotAdded = "An error occurred";
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
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

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
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
