using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Example.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;

namespace Database_Example.ViewModels
{
    public class StudentCourseViewModel : ViewModelBase
    {
        private Student _student_Object;
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
                OnPropertyChanged("Student_Object");
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

        public StudentCourseViewModel(Student Student_Object)
        {
            this.Student_Object = Student_Object;
        }
                
        public void SetCourses()
        {
            if (this.Student_Object.Courses.Count > 0)
            {
                this.StudentCourseString = "";

                foreach (Course Course_Object in this.Student_Object.Courses)
                {
                    this.StudentCourseString += Course_Object.CourseName + "\r\n";
                }
            }
            else
            {
                this.StudentCourseString = "----------";
            }
            OnPropertyChanged("StudentCourseString");
        }
    }
}
