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
using Database_Example.Models;
using Database_Example.Windows;
using Database_Example.Settings;
using Database_Example.ViewModels;
using System.Collections.ObjectModel;

using Database_Example.ExtensionMethods;
using Database_Example.Tools;

namespace Database_Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private DatabaseContext db = new DatabaseContext();
        public static List<ProjectSettings> SettingsList = new List<ProjectSettings>();
        public static ObservableCollection<StudentCourseViewModel> StudentList = new ObservableCollection<StudentCourseViewModel>();

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.DataContext = StudentList;

            BindStudentList();
            SetupWebApiUrls();
        }

        private void SetupWebApiUrls()
        {
            SettingsList.Clear();
            SettingsList.Add(new ProjectSettings(WEB_API_CONTROLLER_ENUM.STUDENT_API_CONTROLLER, 
                             Properties.Settings.Default.WEB_API_STUDENT_URL));
            SettingsList.Add(new ProjectSettings(WEB_API_CONTROLLER_ENUM.TEAM_API_CONTROLLER,
                             Properties.Settings.Default.WEB_API_TEAM_URL));
            SettingsList.Add(new ProjectSettings(WEB_API_CONTROLLER_ENUM.COURSE_API_CONTROLLER,
                             Properties.Settings.Default.WEB_API_COURSE_URL));
        }

        public static string Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM This_Web_Api_Controller_Enum)
        {
            return (SettingsList.First(item => item.Web_Api_Controller_Enum == This_Web_Api_Controller_Enum).Web_Api_Controller_Url);
        }

        private void BindStudentList()
        { 
            DatabaseContext db = new DatabaseContext();

            List<Student> StudentListFromDB = new List<Student>();
            StudentListFromDB = db.Students.ToList();

            dataGrid.Items.Clear();

            foreach (Student Student_Object in StudentListFromDB)
            {
                StudentCourseViewModel StudentCourseMethodWindow_Object = new StudentCourseViewModel(Student_Object);

                StudentCourseMethodWindow_Object.SetCourses();
                StudentList.Add(StudentCourseMethodWindow_Object);

                dataGrid.Items.Add(StudentCourseMethodWindow_Object);
            }
            
        }

        private void btnEraseStudent_Click(object sender, RoutedEventArgs e)
        {
            Button ThisButon = sender as Button;
            int StudentID = Convert.ToInt32(ThisButon.Content);

            StudentCourseViewModel StudentViewModelObject = StudentList.Single(s => s.Student_Object.StudentID == StudentID);

            MessageBoxResult Result = MessageBox.Show("Ønsker du virkelig at slette eleven " + StudentViewModelObject.Student_Object.StudentName, "Slet Elev ?", MessageBoxButton.OKCancel);

            if (MessageBoxResult.OK == Result)
            {
                Student StudentObjectFromDB = db.Students.Find(StudentID);
                db.Students.Remove(StudentObjectFromDB);
                db.SaveChanges();
                bool Test = StudentList.Remove(StudentViewModelObject);
                dataGrid.Items.Remove(StudentViewModelObject);
            }
        }

        private void btnModifyStudent_Click(object sender, RoutedEventArgs e)
        {
            int IndexInlist;
            int Counter;

            Button ThisButon = sender as Button;
            int StudentID = Convert.ToInt32(ThisButon.Content);
           
            // Skift til ModifyStudentWindow vindue/view
            ModifyStudentWindow dlg = new ModifyStudentWindow(StudentID);
            dlg.ShowDialog();

            IndexInlist = StudentList.FindIndex(s => s.Student_Object.StudentID == StudentID);
            if (-1 != IndexInlist)
            {
                //StudentList[IndexInlist].Student_Object.StudentName = dlg.Student_Object.StudentName;
                //StudentList[IndexInlist].Student_Object.StudentLastName = dlg.Student_Object.StudentLastName;
                //StudentList[IndexInlist].Student_Object.TeamID = dlg.Student_Object.TeamID;
                //StudentList[IndexInlist].Student_Object.Team = dlg.Student_Object.Team;

                //StudentList[IndexInlist].Student_Object.Courses.Clear();
                //for (Counter = 0; Counter < dlg.Student_Object.Courses.Count; Counter++)
                //{
                //    Course CourseObject = new Course();
                //    CourseObject.CourseID = dlg.Student_Object.Courses[Counter].CourseID;
                //    CourseObject.CourseName = dlg.Student_Object.Courses[Counter].CourseName;
                //    StudentList[IndexInlist].Student_Object.Courses.Add(CourseObject);
                //}
                StudentList[IndexInlist].Student_Object = dlg.Student_Object.CopyStudentObjectFields();
                StudentList[IndexInlist].SetCourses();
            }
        }

        private void btnNewStudent_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow dlg = new AddStudentWindow();
            dlg.ShowDialog();

            StudentList.Add(new StudentCourseViewModel(dlg.Student_Object));
            StudentList.Last().SetCourses();
            dataGrid.Items.Add(StudentList.Last());
        }

        private void btnjSonMode_Click(object sender, RoutedEventArgs e)
        {
            jSonStudentList dlg = new jSonStudentList();
            dlg.ShowDialog();
        }
    }
}
