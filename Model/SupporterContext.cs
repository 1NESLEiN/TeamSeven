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
        public SupporterContext()
            : base("Name=SupporterContext")
        {
        }

        public DbSet<Supporter> Supporters { get; set; } 
    }
}
