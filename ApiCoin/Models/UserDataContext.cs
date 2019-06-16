using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ApiCoin.Models
{
    public class UserDataContext : DbContext
    {
        public UserDataContext() : base("MyConnectionString") { }

        public DbSet<User> Users { get; set; }
    }
}