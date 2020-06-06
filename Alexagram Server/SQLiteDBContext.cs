using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexagram_Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alexagram_Server
{
    public class SQLiteDBContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=sqlite.db");
    }
}
