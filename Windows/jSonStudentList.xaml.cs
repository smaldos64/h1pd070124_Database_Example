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
using Database_Example.Models;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using Database_Example.Tools;
using Database_Example.Settings;
using System.Windows.Threading;
using Database_Example.ViewModels;

namespace Database_Example.Windows
{
    /// <summary>
    /// Interaction logic for jSonStudentList.xaml
    /// </summary>
    public partial class jSonStudentList : Window
    {
        //private List<Student> StudentList = new List<Student>();
        private static DispatcherTimer DispatcherTimer_Object = new DispatcherTimer();
        
        public jSonStudentList()
        {
            this.DataContext = new jSonStudentCourseViewModel();
            InitializeComponent();

            DispatcherTimer_Object.Tick += new EventHandler(DispatcherTimer_Timeout);
            DispatcherTimer_Object.Interval = new TimeSpan(0, 0, 0, 0, 200);
            
            BindStudentList();
        }

        private void BindStudentList()
        {
            dataGrid.Items.Clear();

            //List<jSonStudentData> jSonDataList = jsonTools.GetjSonDataList<jSonStudentData>(MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.STUDENT_API_CONTROLLER));
            //dataGrid.ItemsSource = jSonDataList;

            List<jSonStudentData> jSonDataList = jsonTools.GetjSonDataList<jSonStudentData>(MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.STUDENT_API_CONTROLLER));

            foreach (jSonStudentData Student_Object in jSonDataList)
            {
                jSonStudentCourseViewModel StudentCourseMethodWindow_Object = new jSonStudentCourseViewModel(Student_Object);

                StudentCourseMethodWindow_Object.SetCourses(Student_Object);

                dataGrid.Items.Add(StudentCourseMethodWindow_Object);
            }
        }

        private void btnNormalMode_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEraseStudent_Click(object sender, RoutedEventArgs e)
        {
            Button ThisButon = sender as Button;
            int StudentID = Convert.ToInt32(ThisButon.Content);

            jSonStudentData Student_Object = jsonTools.GetjSonData<jSonStudentData>(StudentID, MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.STUDENT_API_CONTROLLER));
       
            MessageBoxResult Result = MessageBox.Show("Ønsker du virkelig at slette eleven " + Student_Object.StudentName, "Slet Elev ?", MessageBoxButton.OKCancel);

            if (MessageBoxResult.OK == Result)
            {
                jsonTools.DeletejSonData(StudentID, MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.STUDENT_API_CONTROLLER));
                DispatcherTimer_Object.Start();
                //BindStudentList();
            }
        }

        private void btnModifyStudent_Click(object sender, RoutedEventArgs e)
        {
            Button ThisButon = sender as Button;
            int StudentID = Convert.ToInt32(ThisButon.Content);

            // Skift til ModifyStudentWindow vindue/view
            jSonModifyStudentWindow dlg = new jSonModifyStudentWindow(StudentID);
            dlg.ShowDialog();

            DispatcherTimer_Object.Start();
            //BindStudentList();
        }

        private void btnNewStudent_Click(object sender, RoutedEventArgs e)
        {
            jSonAddStudentWindow dlg = new jSonAddStudentWindow();
            dlg.ShowDialog();

            DispatcherTimer_Object.Start();
        }

        private void DispatcherTimer_Timeout(object sender, EventArgs e)
        {
            if (jsonTools.IsjSonTransactionInProgress())
            {
                DispatcherTimer_Object.Start();
            }
            else
            {
                DispatcherTimer_Object.Stop();
                BindStudentList();
            }
        }
    }
}
