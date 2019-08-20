using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var result =  context.PolicyInfo.Find<PolicyInfo>(r => r.PolicyNumber == policyNumber).FirstOrDefault();
            return View(result);
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
    }
}