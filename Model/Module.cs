using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_Poe_Part_2.Model
{
    public class Module
    {
        [Key]
        public int ModuleID { get; set; }
        public string UserName { get; set; }   
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
        public int Credits { get; set; }
        public int ClassHours { get; set; }
        public int Weeks { get; set; }
        public DateTime StartDate { get; set; }
        public int HrsPerWeek { get; set; }
    }
}
