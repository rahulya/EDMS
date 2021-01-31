using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSAPI.Models
{
    public class DMSDbContext: DbContext
    {
        public DMSDbContext(DbContextOptions<DMSDbContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<tblUser>()
            //     .HasOne(p => p.tblGroup)
            //     .WithMany(b => b.tblUser)
            //     .HasForeignKey(p => p.GroupID);

        }

        public DbSet<tblUserGroup> tblUserGroup { get; set; }
        public DbSet<tblUser>  tblUser { get; set; }
        public DbSet<Login> logins { get; set; }
        public DbSet<tblCompanyGroup> tblCompanyGroup { get; set; }
        public DbSet<tblUserRole> tblUserRole { get; set; }
        public DbSet<tblCompanyDatabase>  tblCompanyDatabase { get; set; }
        public DbSet<tblUser_tblDatabase> tblUser_TblDatabase { get; set; }
      //  public DbSet<tblCustomer>  tblCustomer { get; set; }
    }
}
