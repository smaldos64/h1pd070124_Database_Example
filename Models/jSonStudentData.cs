using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Example.Models
{

    public class jSonStudentData
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentLastName { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }

        // Mange til Mange Relation håndtering herunder
        public virtual List<int> CourseIDList { get; set; }
        public virtual List<string> CourseNameList { get; set; }
    }

}
