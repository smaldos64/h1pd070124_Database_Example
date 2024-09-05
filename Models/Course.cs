using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Example.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        public string CourseName { get; set; }

        public virtual List<Student> Students { get; set; }
    }
}
