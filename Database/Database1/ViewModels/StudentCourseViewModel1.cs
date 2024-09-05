using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database1.Models;

namespace Database1.ViewModels
{
    public class StudentCourseViewModel1
    {
        private Student _student_Object;
        private List<string> _studentCourseList = new List<string>();
        private string _studentCourseString;

        public Student Student_Object
        {
            get
            {
                return (this._student_Object);
            }
            set
            {
                this._student_Object = value;
            }
        }

        public List<string> StudentCourseList
        {
            get
            {
                return (this._studentCourseList);
            }
            set
            {
                this._studentCourseList.AddRange(value);
            }
        }

        public string StudentCourseString
        {
            get
            {
                return (this._studentCourseString);
            }
            set
            {
                this._studentCourseString = value;
            }
        }

        public StudentCourseViewModel1(Student Student_Object)
        {
            this.Student_Object = Student_Object;
        }

        public StudentCourseViewModel1()
        {
        }

        public void SetCourses(Student Student_Object)
        {
            if (Student_Object.Courses.Count > 0)
            {
                this._studentCourseList.Clear();
                this._studentCourseString = "";

                foreach (Course Course_Object in Student_Object.Courses)
                {
                    this._studentCourseList.Add(Course_Object.CourseName);
                    this._studentCourseString += Course_Object.CourseName + "\r\n";
                }
            }
            else
            {
                this._studentCourseString = "----------";
            }
        }
    }
}
