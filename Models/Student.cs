using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Database_Example.Models
{
    public class Student : INotifyPropertyChanged
    {
        //public string StudentName { get; set; }
        private int _studentID;
        private string _studentName;
        private string _studentLastName;
        private int _teamID;
        private Team _team;
        private List<Course> _courses;

        public int StudentID 
        {
            get
            {
                return (this._studentID);
            }
            set
            {
                this._studentID = value;
                NotifyPropertyChanged("StudentID");
            }
        }
        public string StudentName
        {
            get
            {
                return (this._studentName);
            }
            set
            {
                this._studentName = value;
                NotifyPropertyChanged("StudentName");
            }
        }

        public string StudentLastName 
        { 
            get
            {
                return this._studentLastName;
            }
            set
            {
                this._studentLastName = value;
                NotifyPropertyChanged("StudentLastName");
            }
        }

        public int TeamID 
        { 
            get
            {
                return (this._teamID); 
            }
            set
            {  
                this._teamID = value;
                NotifyPropertyChanged("TeamID");
            }
        }
        public virtual Team Team 
        { 
            get
            {
                return (this._team);
            }
            set
            {
                this._team = value;
                NotifyPropertyChanged("Team");
            }
        }

        public virtual List<Course> Courses 
        {
            get
            {
                return (this._courses);
            }
            set
            { 
                this._courses = value;
                NotifyPropertyChanged("StudentCourseString");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
