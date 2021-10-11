using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutor_Connect.Models;
using System.Web.Security;
using System.Data.Entity;

namespace Tutor_Connect.Controllers
{
    public class StudentController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Student
        public ActionResult Index()
        {
            return View(db.Students.ToList());
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

                //Student student = new Student();
                //student.StudNumber = newStudent.StudNumber;
                //student.Surname = newStudent.Surname;
                //student.PhoneNumber = newStudent.PhoneNumber;
                //student.Firstname = newStudent.Firstname;
                //student.fieldOfStudy = newStudent.fieldOfStudy;
                //student.Email = newStudent.Email;
                //student.yearOfStudy = newStudent.yearOfStudy;
                //student.Image = newStudent.Image;

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
            catch(Exception e)
            {
                ViewBag.NotAdded = e.Message.Length;
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit()
        {
            if (Session["logged"] == null)
                return RedirectToAction("Login", "User");

            int id = Convert.ToInt32(Session["LoggedUserId"]);
            User user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            Student studentToEdit = db.Students.Find(user.username);
            if (studentToEdit == null)
                return RedirectToAction("Index");
            return View(studentToEdit);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(Student studentToEdit)
        {
            try
            {
                // TODO: Add update logic here

                db.Entry(studentToEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile", "User");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Profile/5
        public ActionResult viewProfile(String id)
        {
            Student studentProfile = db.Students.Where(n => n.StudNumber == id).FirstOrDefault();
            if(studentProfile == null)
            {
                return HttpNotFound();
            }
            return View(studentProfile);
        }

      
    }
}
