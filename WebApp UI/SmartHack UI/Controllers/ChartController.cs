using SmartHack_UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartHack_UI.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetPieChartData()
        {
            List<PieDataChartFormat> pieChartData = new List<PieDataChartFormat>();

            PieDataChartFormat firstData = new PieDataChartFormat();
            firstData.value = 700;
            firstData.color = "#f56954";
            firstData.highlight = "#f56954";
            firstData.label = "Chrome";

            PieDataChartFormat secondData = new PieDataChartFormat();
            secondData.value = 500;
            secondData.color = "#00a65a";
            secondData.highlight = "#00a65a";
            secondData.label = "IE";

            pieChartData.Add(firstData);
            pieChartData.Add(secondData);

            return Json(pieChartData);
        }

        [HttpPost]
        public JsonResult GetSellerLineChartData(int? id)
        {
            List<TransactionTable> TransactionTableList = new List<TransactionTable>();

            using (MyDBContext dbc = new MyDBContext())
            {
                TransactionTableList = dbc.TransactionTable.Where(x => x.SellerID == id).Include(i => i.Seller).Include(i => i.Buyer).ToList();
            }
            int[] arrRefusedTransactions = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] arrAcceptedTransactions = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


            foreach (var x in TransactionTableList)
            {
                var date = x.OrderStartDate.Value.Month;
                if (x.TransactionStatus.Contains("rejected"))
                {
                    arrRefusedTransactions[date - 1]++;
                } else if (x.TransactionStatus.Contains("accepted"))
                {
                    arrAcceptedTransactions[date - 1]++;
                }
            }

       
            List<int> dataRefusedTransactions = new List<int>(arrRefusedTransactions);
            List<int> dataAcceptedTransactions = new List<int>(arrAcceptedTransactions);

            List<List<int>> data = new List<List<int>>();
            data.Add(dataRefusedTransactions);
            data.Add(dataAcceptedTransactions);


            return Json(data);

        }

        [HttpPost]
        public JsonResult getBuyerLineChartData(int? id)
        {
            List<TransactionTable> TransactionTableList = new List<TransactionTable>();

            using (MyDBContext dbc = new MyDBContext())
            {
                TransactionTableList = dbc.TransactionTable.Where(x => x.BuyerID == id).Include(i => i.Seller).Include(i => i.Buyer).ToList();
            }
            int[] arrRefusedTransactions = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] arrAcceptedTransactions = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


            foreach (var x in TransactionTableList)
            {
                var date = x.OrderStartDate.Value.Month;
                if (x.TransactionStatus.Contains("rejected"))
                {
                    arrRefusedTransactions[date - 1]++;
                }
                else if (x.TransactionStatus.Contains("accepted"))
                {
                    arrAcceptedTransactions[date - 1]++;
                }
            }


            List<int> dataRefusedTransactions = new List<int>(arrRefusedTransactions);
            List<int> dataAcceptedTransactions = new List<int>(arrAcceptedTransactions);

            List<List<int>> data = new List<List<int>>();
            data.Add(dataRefusedTransactions);
            data.Add(dataAcceptedTransactions);


            return Json(data);

        }

        
    }
}