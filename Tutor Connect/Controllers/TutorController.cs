using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutor_Connect.Models;

namespace Tutor_Connect.Controllers
{
    public class TutorController : Controller
    {

        DataModel db = new DataModel();
        // GET: Tutor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tutor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Apply()
        {
            var getSession = db.Modules.ToList();
            MultiSelectList list = new MultiSelectList(getSession, "moduleCode", "moduleName");
            ViewBag.SelectSession = list;
            return View();
        }

        // POST: Tutor/Create
        [HttpPost]
        public ActionResult Apply([Bind(Exclude = "Id")] Applicant newApplicant)
        {
            try
            {
                // TODO: Add insert logic here
                newApplicant.applicationDate = DateTime.Now.ToString("dd/MM/yyyy");
                db.Applicants.Add(newApplicant);

                db.SaveChanges();

                ModelState.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tutor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tutor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tutor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tutor/Edit/5
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

        // GET: Tutor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tutor/Delete/5
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
