using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database1.Models;
using System.Windows.Data;
using System.Globalization;

namespace Database1.ViewModels 
{
    public class StudentCourseViewModel : IDisposable
    {
        private readonly CodeModelDB db = new CodeModelDB();

        public void Dispose()
        {
            if (null != db)
            {
                db.Dispose();
            }
        }

        public StudentCourseViewModel()
        {
            this.Students = db.Students.ToList();
            this.Courses = db.Courses.ToList();
        }

        #region Properties
        public List<Student> Students
        {
            get;
            private set;
        }

        public List<Course> Courses
        {
            get;
            private set;
        }

        #endregion
    }

    internal class CoursesToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ICollection<Course> Courses = value as ICollection<Course>;
            if (Courses != null)
            {
                Course Course = parameter as Course;
                if (Course != null)
                    return Courses.Contains(Course);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
