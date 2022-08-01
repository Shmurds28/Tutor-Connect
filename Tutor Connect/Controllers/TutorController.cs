using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tutor_Connect.Models;
using System.Threading.Tasks;
using System.Text;

namespace Tutor_Connect.Controllers
{
    public class TutorController : Controller
    {
        DataModel db = new DataModel();
        // GET: Tutor
        public ActionResult Index(string module)
        {
            List<Student> list = new List<Student>();

            var tutors = db.Tutors.ToList();

            foreach (Tutor tutor in tutors)
            {
                Student student = db.Students.Find(tutor.TutorId.ToString());

                TutorModule tutorModule = db.TutorModules.Where(x => x.studNumber == student.StudNumber && x.moduleCode == module).FirstOrDefault();

                List<TutorModule> tutorModules = new List<TutorModule>();

                tutorModules = db.TutorModules.ToList();
                ViewBag.tutorModules = tutorModules;

                if (tutorModule != null || module == null || module == "")
                    list.Add(student);


            }
            return View(list);
        }

        public ActionResult ViewTutorProfile(int id)
        {
            Student tutor = new Student();
            List<Slot> slots = new List<Slot>();
            tutor = db.Students.Where(t => t.StudNumber == id.ToString()).FirstOrDefault();

            slots = db.Slots.Where(s => s.tutorId == id.ToString()).ToList();
            ViewBag.slots = slots;

            return View(tutor);
        }
        public ActionResult ListOfTutors()
        {
            return View(db.Tutors.ToList());
        }
        public ActionResult makeReviewTutor(int id)
        {
            Tutor cur = db.Tutors.Where(x => x.TutorId == id).FirstOrDefault();
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult makeReviewTutor([Bind(Exclude = "Id")] Review tutorToCreate, int id)

        {
            try
            {

                // TODO: Add insert logic here
                tutorToCreate.tutorId = id;
                tutorToCreate.Date = DateTime.Now.ToString("dd/MM/yyyy");
                tutorToCreate.studNumber = "217885332";
                db.Reviews.Add(tutorToCreate);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ViewReviews(int id)
        {
            var reviewlist = db.Reviews.Where(r => r.tutorId == id);
            ViewBag.reviewList = reviewlist; 

            Review review = db.Reviews.Where(r => r.tutorId == id).FirstOrDefault();

            var tutor = db.Students.Where(r => r.StudNumber == id.ToString()).FirstOrDefault();
            ViewBag.tutorName = tutor.Firstname;
            ViewBag.tutorSurname = tutor.Surname;

            var studentlist = db.Students.ToList(); 
            ViewBag.studentList = studentlist; 

            return View(review);
        }

        public ActionResult NotificationView(int id)
        {

            if (Session["logged"] == null)
                return RedirectToAction("Login", "User");

            //Record Booking


            Student tutor = db.Students.Where(r => r.StudNumber == id.ToString()).FirstOrDefault();
            ViewData["name"] = tutor.Firstname;
            ViewData["surname"] = tutor.Surname;

            int Sid = Convert.ToInt32(Session["LoggedUserId"]);
            User user = db.Users.Find(Sid);
           

            Student student = db.Students.Find(user.username);

                try
            {
                var sendermail = new MailAddress("tutorconnectnmu@gmail.com", "Tutor Connect");
                var receivermail = new MailAddress(tutor.Email, tutor.Firstname);
                var password = "thirdyearproject";
                var sub = "New Slot Booking";
                var body = student.Firstname+" "+student.Surname+ " has made a slot booking.";


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
            }
            catch
            {
                ViewData["Fail"] = "Error: Booking notification could not be sent.";
            }
            return View(tutor);
        }

        //public bool SendEmail(string toEmail, string subject, string emailBody)
        //{
        //    try {
        //        string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
        //        string  senderPasword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();
        //        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        //        client.EnableSsl = true;
        //        client.Timeout = 100000;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = new NetworkCredential(senderEmail, senderPasword);

        //        MailMessage mailmessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
        //        mailmessage.IsBodyHtml = true;
        //        mailmessage.BodyEncoding = UTF8Encoding.UTF8;
        //        client.Send(mailmessage);
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return false;
        //    }
        //}
    }
}