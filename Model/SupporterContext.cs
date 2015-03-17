using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SupporterContext : DbContext
    {
        private static string connectionString = @"Data Source=PETER-PC\SQLEXPRESS;Initial Catalog=Team7;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        public SupporterContext()
            : base(connectionString)
        {
        }

        public DbSet<Supporter> Supporters { get; set; } 
    }
}
