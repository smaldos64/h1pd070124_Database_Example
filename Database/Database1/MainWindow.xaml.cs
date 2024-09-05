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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Database1.Models;
using Database1.Windows;

namespace Database1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //using (var db = new CodeModelDB())
            //{
            //    try
            //    {
            //        Student Student_Object = new Student { StudentName = "Lars Pedersen", StandardID = 1 };
            //        db.Students.Add(Student_Object);

            //        db.SaveChanges();
            //    }
            //    catch (Exception error)
            //    {
            //        lblError.Content = error.ToString();
            //    }
            //}
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ViewStudents_Click(object sender, RoutedEventArgs e)
        {
            StudentsWindow dlg = new StudentsWindow();
            dlg.ShowDialog();
        }

        private void ViewClasses_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewCourses_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ViewStudentCourses_Click(object sender, RoutedEventArgs e)
        {
            StudentCourseWindow dlg = new StudentCourseWindow();
            dlg.ShowDialog();
        }

        private void ViewStudentCourseMethod1Window_Click(object sender, RoutedEventArgs e)
        {
            StudentCourseMethod1Window dlg = new StudentCourseMethod1Window();
            dlg.ShowDialog();
        }

        private void ViewStudentCourseMethod2Window_Click(object sender, RoutedEventArgs e)
        {
            StudentCourseMethod2Window dlg = new StudentCourseMethod2Window();
            dlg.ShowDialog();
        }
    }
}
