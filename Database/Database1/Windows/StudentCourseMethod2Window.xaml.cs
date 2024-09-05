using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Database1.Models;
using Database1.ViewModels;

namespace Database1.Windows
{
    /// <summary>
    /// Interaction logic for StudentCourseMethod2Window.xaml
    /// </summary>
    public partial class StudentCourseMethod2Window : Window
    {

        private CodeModelDB db = new CodeModelDB();
        private List<Student> StudentList = new List<Student>();

        public StudentCourseMethod2Window()
        {
            //this.DataContext = new StudentCourseViewModel2();
            InitializeComponent();
            CreateDataGrid();
        }

        private void CreateDataGrid()
        {
            //int Counter = 1;
            //foreach (Student Student_Object in StudentList)
            //{
            //    dataStudents.Items.Add(Student_Object);
            //    //DataGridRow ThisRow = dataStudents.ItemContainerGenerator.ContainerFromIndex(dataStudents.Items.Count - 1) as DataGridRow;
            //    var DataItem = dataStudents.Items[dataStudents.Items.Count - 1] as DataGridRow;

            //    if (Student_Object.Courses.Count > 0)
            //    {
            //        //ThisRow.colu
            //    }
            //    else
            //    {

            //    }
            //    Counter++;
            //}

            StudentList = db.Students.ToList();

            foreach (Student Student_Object in StudentList)
            {
                StudentCourseViewModel2 StudentCourseMethod2Window_Object = new StudentCourseViewModel2(Student_Object);

                StudentCourseMethod2Window_Object.SetCourses(Student_Object);

                dataStudents.Items.Add(StudentCourseMethod2Window_Object);
            }
        }

        private void btnEraseStudent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
