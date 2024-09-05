using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Example.Models;

namespace Database_Example.ViewModels
{
    public class jSonStudentCourseViewModel
    {
        private jSonStudentData _student_Object;
        private string _studentCourseString;

        public jSonStudentData Student_Object
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

        public jSonStudentCourseViewModel()
        {

        }

        public jSonStudentCourseViewModel(jSonStudentData Student_Object)
        {
            this.Student_Object = Student_Object;
        }

        public void SetCourses(jSonStudentData Student_Object)
        {
            if ((null != Student_Object.CourseIDList) &&
                (null != Student_Object.CourseNameList))
            {
                if (Student_Object.CourseIDList.Count > 0)
                {
                    this.StudentCourseString = "";

                    foreach (string CourseName in Student_Object.CourseNameList)
                    {
                        this.StudentCourseString += CourseName + "\r\n";
                    }
                }
                else
                {
                    this.StudentCourseString = "----------";
                }
            }
            else
            {
                this.StudentCourseString = "----------";
            }
        }
    }
}
