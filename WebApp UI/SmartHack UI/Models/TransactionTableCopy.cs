using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartHack_UI.Models
{
    [Table("TransactionTableCopy")]
    public class TransactionTableCopy
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }
        public string SellerAsset { get; set; }
        public double? SellerAmount { get; set; }
        public string BuyerAsset { get; set; }
        public double? BuyerAmount { get; set; }
        public string TransactionStatus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? OrderStartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? OrderEndDate { get; set; }

        public int SellerID { get; set; }
        public Company Seller { get; set; }

        public int BuyerID { get; set; }
        public Company Buyer { get; set; }

        public bool Output { get; set; }

        [NotMapped]
        public string OrderStartDateString { get; set; }
        [NotMapped]
        public string OrderEndDateString { get; set; }
        [NotMapped]
        public string OutputString { get; set; }

        [NotMapped]
        public IEnumerable<Company> companySellerDropDown = new List<Company>();
        [NotMapped]
        public IEnumerable<Company> companyBuyerDropDown = new List<Company>();

    }

}