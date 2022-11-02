using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_Poe_Part_2.Model
{
    public class LoginContext : DbContext
    {
        public DbSet<Login> Logins { get; set; }
    }
}
