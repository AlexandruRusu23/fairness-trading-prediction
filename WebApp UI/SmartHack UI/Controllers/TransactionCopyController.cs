using SmartHack_UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartHack_UI.Controllers
{
    public class TransactionCopyController : Controller
    {
        // GET: TransactionCopy
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Save()
        {
            TransactionTableCopy TransactionTableCopy = new TransactionTableCopy();
            using (MyDBContext dbc = new MyDBContext())
            {
                TransactionTableCopy.companySellerDropDown = dbc.Company.ToList();
                TransactionTableCopy.companyBuyerDropDown = dbc.Company.ToList();
            }
            return View(TransactionTableCopy);
        }

        [HttpPost]
        public ActionResult Save(TransactionTableCopy model)
        {
            using (MyDBContext dc = new MyDBContext())
            {
                dc.TransactionTableCopy.Add(model);
                dc.SaveChanges();
            }
            return RedirectToAction("Index", "Company");
        }


    }
}