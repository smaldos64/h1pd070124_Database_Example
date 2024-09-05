using Database_Example.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Example.Tools
{
    public static class ObjectHandlingTools
    {
        public static Student CopyStudentObjectFields(this Student StudentSourceObject)
        {
            int Counter;
            Student StudentTargetObject = new Student();

            StudentTargetObject.StudentID = StudentSourceObject.StudentID;
            StudentTargetObject.StudentName = StudentSourceObject.StudentName;
            StudentTargetObject.StudentLastName = StudentSourceObject.StudentLastName;  
            StudentTargetObject.TeamID = StudentSourceObject.TeamID;
            StudentTargetObject.Team = new Team();
            StudentTargetObject.Team.TeamName = StudentSourceObject.Team.TeamName;

            StudentTargetObject.Courses = new List<Course>();

            for (Counter = 0; Counter < StudentSourceObject.Courses.Count; Counter++)
            {
                Course CourseObject = new Course();
                CourseObject.CourseID = StudentSourceObject.Courses[Counter].CourseID;
                CourseObject.CourseName = StudentSourceObject.Courses[Counter].CourseName;
                StudentTargetObject.Courses.Add(CourseObject);
            }

            return (StudentTargetObject);

        }
    }
}
