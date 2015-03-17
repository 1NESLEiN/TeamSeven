using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Supporter
    {
        public Supporter()
        {
        }

        public int SupporterID { get; set; }
        public string SupporterName { get; set; }
        public string SupporterInitials { get; set; }
    }
}
