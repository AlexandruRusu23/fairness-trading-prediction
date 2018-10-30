using SmartHack_UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartHack_UI.Controllers
{
    public class TransactionController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? id)
        {
            Company company = new Company();
            if(id != null)
            {
                using (MyDBContext dbc = new MyDBContext())
                {
                    company = dbc.Company.Where(x => x.ID == id).FirstOrDefault();
                }
                return View(company);
            }
            return View();
         
        }

        [HttpPost]
        public ActionResult GetAllSellerTransactions(int? id)
        {
            List<TransactionTable> TransactionTableList = new List<TransactionTable>();

            using (MyDBContext dbc = new MyDBContext())
            {
                TransactionTableList = dbc.TransactionTable.Where(x => x.SellerID == id).Include(i => i.Seller).Include(i => i.Buyer).ToList();
            }

            foreach(var x in TransactionTableList)
            {
                var sellerAssetSplit = x.SellerAsset.Split('_');
                x.SellerAsset = "";
               for (var i = 1; i < sellerAssetSplit.Length; i++)
                {
                    x.SellerAsset = x.SellerAsset + sellerAssetSplit[i] + " ";
                }
                var buyerAssetSplit = x.BuyerAsset.Split('_');
                x.BuyerAsset = "";
                for (var i = 1; i < buyerAssetSplit.Length; i++)
                {
                    x.BuyerAsset = x.BuyerAsset + buyerAssetSplit[i] + " ";
                }
                var transactionStatusSplit = x.TransactionStatus.Split('_');
                x.TransactionStatus = "";
                for (var i = 0; i < transactionStatusSplit.Length; i++)
                {
                    x.TransactionStatus = x.TransactionStatus + transactionStatusSplit[i] + " ";
                }
                x.Seller.SellerRel = null;
                x.Seller.SellerRelCopy = null;
                x.Seller.BuyerRel = null;
                x.Seller.BuyerRelCopy = null;

                x.Buyer.SellerRel = null;
                x.Buyer.SellerRelCopy = null;
                x.Buyer.BuyerRel = null;
                x.Buyer.BuyerRelCopy = null;
                x.OrderStartDateString = x.OrderStartDate.Value.ToString("d");
                x.OrderEndDateString = x.OrderEndDate.Value.ToString("d");
                x.OrderStartDate = null;
                x.OrderEndDate = null;
            }
            return Json(new { data = TransactionTableList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllBuyerTransactions(int? id)
        {
            List<TransactionTable> TransactionTableList = new List<TransactionTable>();

            using (MyDBContext dbc = new MyDBContext())
            {
                TransactionTableList = dbc.TransactionTable.Where(x => x.BuyerID == id).Include(i => i.Seller).Include(i => i.Buyer).ToList();
            }

            foreach (var x in TransactionTableList)
            {
                var sellerAssetSplit = x.SellerAsset.Split('_');
                x.SellerAsset = "";
                for (var i = 1; i < sellerAssetSplit.Length; i++)
                {
                    x.SellerAsset = x.SellerAsset + sellerAssetSplit[i] + " ";
                }
                var buyerAssetSplit = x.BuyerAsset.Split('_');
                x.BuyerAsset = "";
                for (var i = 1; i < buyerAssetSplit.Length; i++)
                {
                    x.BuyerAsset = x.BuyerAsset + buyerAssetSplit[i] + " ";
                }
                var transactionStatusSplit = x.TransactionStatus.Split('_');
                x.TransactionStatus = "";
                for (var i = 0; i < transactionStatusSplit.Length; i++)
                {
                    x.TransactionStatus = x.TransactionStatus + transactionStatusSplit[i] + " ";
                }
                x.Seller.SellerRel = null;
                x.Seller.SellerRelCopy = null;
                x.Seller.BuyerRel = null;
                x.Seller.BuyerRelCopy = null;

                x.Buyer.SellerRel = null;
                x.Buyer.SellerRelCopy = null;
                x.Buyer.BuyerRel = null;
                x.Buyer.BuyerRelCopy = null;
                x.OrderStartDateString = x.OrderStartDate.Value.ToString("d");
                x.OrderEndDateString = x.OrderEndDate.Value.ToString("d");
                x.OrderStartDate = null;
                x.OrderEndDate = null;
            }
            return Json(new { data = TransactionTableList }, JsonRequestBehavior.AllowGet);
        }

    }
}