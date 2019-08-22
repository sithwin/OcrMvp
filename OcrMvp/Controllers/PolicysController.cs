using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using OcrMvp.Data;
using OcrMvp.Models;

namespace OcrMvp.Controllers
{
    public class PolicysController : Controller
    {
        public readonly MongoContext context = new MongoContext();

        // GET: Policys
        public ActionResult Index(string policyNumber)
        {
            if (ModelState.IsValid)
            {
                var result = context.PolicyInfo.Find<PolicyInfo>(r => r.PolicyNumber == policyNumber).FirstOrDefault();

                this.ViewBag.GenderList = getGenderList();
                this.ViewBag.MaritalStatusList = getMaritalStatus();
                return View(result);
            }
            else
            {
                return View();
            }
        }

        // GET: Policys/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Policys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Policys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PolicyInfo model)
        {
            try
            {
                // TODO: Add insert logic here
                context.PolicyInfo.InsertOne(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Policys/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Policys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Policys/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Policys/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private List<SelectListItem> getGenderList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Select Gender",
                Value = "0",
                Selected = true
            });

            items.Add(new SelectListItem
            { Text = "Male", Value = "M" });

            items.Add(new SelectListItem
            { Text = "Female", Value = "F" });

            return items;
        }

        private List<SelectListItem> getMaritalStatus()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Select Marital Status",
                Value = "0",
                Selected = true
            });

            items.Add(new SelectListItem
            { Text = "Single", Value = "S" });

            items.Add(new SelectListItem
            { Text = "Married", Value = "M" });

            return items;
        }
    }
}