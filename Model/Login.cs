using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_Poe_Part_2.Model
{
    public class Login
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }

        

    }
}
