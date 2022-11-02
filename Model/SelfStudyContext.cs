using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_Poe_Part_2.Model
{
    public class SelfStudyContext : DbContext
    {
        public DbSet<SelfStudy> SelfStudy { get; set; }
    }
}
