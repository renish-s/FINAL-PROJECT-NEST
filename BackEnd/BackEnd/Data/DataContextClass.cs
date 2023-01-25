using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Data
{
    public class DataContextClass:DbContext
    {
        public DataContextClass(DbContextOptions<DataContextClass> options) : base(options)
        {

        }
        public DbSet<User> tbluser { get; set; }
        public DbSet<Product> tblproduct { get; set; }
        public DbSet<Complaint> tblcomplaint { get; set; }  // chumma..!!
        public DbSet<NewComplaint> complaint_tbl { get; set; }
    }
}
