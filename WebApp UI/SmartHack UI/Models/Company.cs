using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartHack_UI.Models
{
    [Table("Company")]
    public class Company
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? Popularity { get; set; }

        public ICollection<TransactionTable> SellerRel { get; set; }
        public ICollection<TransactionTable> BuyerRel { get; set; }

        public ICollection<TransactionTableCopy> SellerRelCopy { get; set; }
        public ICollection<TransactionTableCopy> BuyerRelCopy { get; set; }
    }

}