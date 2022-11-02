using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_Poe_Part_2.Model
{
    public class SelfStudy
    {
        [Key]
        public int studyID { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfStudy { get; set; }
        public string Code { get; set; }
        public int HoursStudied { get; set; }
        public int WorkWeek { get; set; }
    }
}
