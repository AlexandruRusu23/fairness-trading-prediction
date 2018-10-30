using SmartHack_UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartHack_UI
{
    public class MyDBContext : DbContext
    {
        static MyDBContext()
        {
            Database.SetInitializer<MyDBContext>(null);
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<TransactionTable> TransactionTable { get; set; }
        public DbSet<TransactionTableCopy> TransactionTableCopy { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasKey(t => t.ID);

            modelBuilder.Entity<TransactionTable>()
               .HasRequired<Company>(s => s.Seller)
               .WithMany(g => g.SellerRel)
               .HasForeignKey<int>(s => s.SellerID);

            modelBuilder.Entity<TransactionTable>()
                .HasRequired<Company>(s => s.Buyer)
                .WithMany(g => g.BuyerRel)
                .HasForeignKey<int>(s => s.BuyerID);

            modelBuilder.Entity<TransactionTableCopy>()
               .HasRequired<Company>(s => s.Seller)
               .WithMany(g => g.SellerRelCopy)
               .HasForeignKey<int>(s => s.SellerID);

            modelBuilder.Entity<TransactionTableCopy>()
                .HasRequired<Company>(s => s.Buyer)
                .WithMany(g => g.BuyerRelCopy)
                .HasForeignKey<int>(s => s.BuyerID);
        }
    }
}