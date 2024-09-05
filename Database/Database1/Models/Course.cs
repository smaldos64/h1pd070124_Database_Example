using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database1.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        public string CourseName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
