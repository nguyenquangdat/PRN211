using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.DBcontext
{
    public class SugasContext : DbContext
    {
        public SugasContext() : base("SugasContext")
        {

        }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.UserPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.CategoryName)
                .IsFixedLength();

            modelBuilder.Entity<Tags>()
                .Property(e => e.TagName)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductPrice)
                .HasPrecision(18, 0);

          


        }
    }
}