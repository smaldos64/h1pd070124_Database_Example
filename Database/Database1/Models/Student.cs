using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database1.Models
{
    public class Student
    {
        public Student()
        {

        }

        public int StudentID { get; set; }

        [ConcurrencyCheck]
        public string StudentName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhotoURL { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }

        public int StandardID { get; set; }
        public virtual Standard Standard { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

    }
}
