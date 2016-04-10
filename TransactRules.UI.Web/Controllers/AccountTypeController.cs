using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransactRules.Configuration.Data;
using TransactRules.Core.Utilities;

namespace TransactRules.UI.Web.Controllers
{
    public class AccountTypeController : Controller
    {
        //
        // GET: /AccountType/

        public ActionResult Index()
        {
            var accountTypeRepository = new AccountTypeRepository() { UnitOfWork = SessionState.Current.UnitOfWork };

            return View(accountTypeRepository.Items());
        }

        //
        // GET: /AccountType/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AccountType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AccountType/Create

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

        //
        // GET: /AccountType/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /AccountType/Edit/5

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

        //
        // GET: /AccountType/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /AccountType/Delete/5

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
