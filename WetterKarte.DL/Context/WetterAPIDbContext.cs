using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WetterKarte.DL.Entities.User;

namespace WetterKarte.DL.Context
{
    public class WetterAPIDbContext : DbContext
    {
        public WetterAPIDbContext(DbContextOptions<WetterAPIDbContext> options) : base(options)
        {

        }

        #region User
        public DbSet<User> Users { get; set; }
        #endregion
    }
}
