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
using System.Net.Mail;
using System.Net;


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

            Student student = db.Students.Find(user.username);
            if (student == null)
                return RedirectToAction("Index");

            List<TutorModule> tutorModules = new List<TutorModule>();

            tutorModules = db.TutorModules.ToList();
            ViewBag.tutorModules = tutorModules;

            Student tutor = new Student();
            List<Slot> slots = new List<Slot>();
            tutor = db.Students.Single(t => t.StudNumber == student.StudNumber);

            slots = db.Slots.Where(s => s.tutorId == student.StudNumber).ToList();
            ViewBag.slots = slots;

            return View(student);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["logged"] == null)
                return RedirectToAction("Login");

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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["logged"] = null;
            Session["LoggedUserId"] = -1;
            return RedirectToAction("Login");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string studNumber)
        {
            Student student = db.Students.Find(studNumber);

            if (student == null)
            {
                return RedirectToAction("ForgotPassword");
            }
            else
            {

                try
                {
                    var sendermail = new MailAddress("tutorconnectnmu@gmail.com", "Tutor Connect");
                    var receivermail = new MailAddress(student.Email, student.Firstname);
                    var password = "thirdyearproject";
                    var sub = "Password Request";
                    var body = "Current password: " + student.Password;


                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sendermail.Address, password)
                    };
                    using (var mess = new MailMessage(sendermail, receivermail)
                    {
                        Subject = sub,
                        Body = body
                    })
                        smtp.Send(mess);

                    ViewData["Success"] = "Booking notification sent successfully.";
                    return View("passwordSent");
                }
                catch
                {
                    ViewData["Fail"] = "Error: Booking notification could not be sent.";
                    return View();
                }
            }


        }

        public ActionResult passwordSent()
        {
            return View();
        }

        public ActionResult Apply()
        {
            if (Session["logged"] == null)
            {
                return RedirectToAction("Login");
            }
            var getSession = db.Modules.ToList();
            MultiSelectList list = new MultiSelectList(getSession, "moduleCode", "moduleName");
            ViewBag.SelectSession = list;
            return View();
        }

        public ActionResult view()
        {
            ViewData["Success"] = "Application Submitted Successfully.";
            return View();
        }

        // POST: Tutor/Create
        [HttpPost]
        public ActionResult Apply([Bind(Exclude = "Id")] Applicant newApplicant)
        {
            if (newApplicant.moduleCode == null)
            {
                ViewData["Fail"] = "ERROR: Application could not be submitted. Module selection is required.";
            }
            else
            {
                try
                {
                    int id = Convert.ToInt32(Session["LoggedUserId"]);
                    User user = db.Users.Find(id);
                    // TODO: Add insert logic here
                    newApplicant.studNumber = user.username;
                    newApplicant.applicationDate = DateTime.Now.ToString("dd/MM/yyyy");
                    db.Applicants.Add(newApplicant);

                    TutorModule cur = new TutorModule();
                    cur.studNumber = newApplicant.studNumber;
                    cur.moduleCode = newApplicant.moduleCode;
                    db.TutorModules.Add(cur);
                    db.SaveChanges();

                    ModelState.Clear();
                    return RedirectToAction("view");

                }
                catch
                {
                    return View();
                }
            }
            return View("view");
        }

        public ActionResult UpdateSlots(int id)
        {
            Slot slot = new Slot();
            slot = db.Slots.Where(s => s.slotId == id).FirstOrDefault();
            return View(slot);
        }

        [HttpPost]
        public ActionResult UpdateSlots(Slot newSlot)
        {

            try
            {
                if (newSlot.endTime <= newSlot.startTime)
                {
                    ViewData["Invalid"] = "Invalid Time Slot";
                }
                else
                {
                    db.Entry(newSlot).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewData["Success"] = "Data Saved Successfully.";
                    ModelState.Clear();
                    return RedirectToAction("Profile");
                }
                return View(newSlot);
            }
            catch
            {
                return View(newSlot);
            }
        }
        public ActionResult AddSlot(int id)
        {
            var tutorModules = db.TutorModules.Where(t => t.studNumber == id.ToString()).ToList();
            MultiSelectList list = new MultiSelectList(tutorModules, "moduleCode", "moduleCode");
            ViewBag.SelectModule = list;

            Slot newOne = new Slot();
            newOne.startTime = new TimeSpan();
            newOne.endTime = new TimeSpan();


            return View(newOne);
        }

        [HttpPost]
        public ActionResult AddSlot([Bind(Exclude = "Id")] Slot newSlot, string id)
        {
            var tutorModules = db.TutorModules.Where(t => t.studNumber == id.ToString()).ToList();
            MultiSelectList list = new MultiSelectList(tutorModules, "moduleCode", "moduleCode");
            ViewBag.SelectModule = list;

            try
            {
                newSlot.tutorId = id;
                if (!(newSlot.endTime > newSlot.startTime))
                {
                    ViewData["Invalid"] = "Invalid Time Slot";
                }
                else
                {
                    db.Slots.Add(newSlot);
                    db.SaveChanges();
                    ViewData["Success"] = "Data Saved Successfully.";
                    ModelState.Clear();
                    return RedirectToAction("Profile");
                }
                return View(newSlot);
            }
            catch
            {
                return View(newSlot);
            }
        }

    }
}

