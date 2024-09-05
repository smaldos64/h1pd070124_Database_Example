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
using Database_Example.Tools;
using Database_Example.Settings;

namespace Database_Example.Windows
{
    /// <summary>
    /// Interaction logic for jSonModifyStudentWindow.xaml
    /// </summary>
    public partial class jSonModifyStudentWindow : Window
    {
        private int StudentID = 0;
        private jSonStudentData Student_Object;
        private List<jSonTeamData> TeamList;
        private List<jSonCourseData> CourseList;
        private static List<ItemEntry> ItemEntryList = new List<ItemEntry>();

        public jSonModifyStudentWindow(int StudentID)
        {
            InitializeComponent();
            this.StudentID = StudentID;

            Student_Object = jsonTools.GetjSonData<jSonStudentData>(StudentID, MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.STUDENT_API_CONTROLLER));
            TeamList = jsonTools.GetjSonDataList<jSonTeamData>(MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.TEAM_API_CONTROLLER));
            lvTeam.ItemsSource = TeamList;

            bool SelectedItemFound = false;
            int Counter = 0;
            do
            {
                if (TeamList.ElementAt(Counter).TeamID == Student_Object.TeamID)
                {
                    SelectedItemFound = true;
                }
                else
                {
                    Counter++;
                }
            } while ((Counter < TeamList.Count) && (false == SelectedItemFound));

            if (true == SelectedItemFound)
            {
                lvTeam.SelectedIndex = Counter;
            }
            else
            {
                lvTeam.SelectedIndex = 0;
            }

            // Mange til Mange Relation håndtering herunder
            CourseList = jsonTools.GetjSonDataList<jSonCourseData>(MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.COURSE_API_CONTROLLER));
            IEnumerable<ItemEntry> ItemEntryEnumerable = from item in CourseList
                                                         select new ItemEntry()
                                                         {
                                                             Name = item.CourseName,
                                                             ID = item.CourseID,
                                                             IsSelected = Student_Object.CourseIDList.IndexOf(item.CourseID) != -1
                                                         };   
       
            ItemEntryList = ItemEntryEnumerable.ToList();
            lbCourses.ItemsSource = ItemEntryList;

            txtStudentName.Text = Student_Object.StudentName;
            txtStudentLastName.Text = Student_Object.StudentLastName;
        }

        private void btnSaveModifiedStudent_Click(object sender, RoutedEventArgs e)
        {
            Student_Object.StudentID = this.StudentID; 
            Student_Object.StudentName = txtStudentName.Text;
            Student_Object.StudentLastName = txtStudentLastName.Text;
            Student_Object.TeamID = TeamList.ElementAt(lvTeam.SelectedIndex).TeamID;

            // Mange til Mange Relation håndtering herunder
            List<int> ItemEntryListInt = new List<int>();
            List<string> ItemEntryListString = new List<string>();

            foreach (ItemEntry ItemEntry_Object in ItemEntryList)
            {
                if (true == ItemEntry_Object.IsSelected)
                {
                    ItemEntryListInt.Add(ItemEntry_Object.ID);
                    ItemEntryListString.Add(ItemEntry_Object.Name);
                }
            }

            try
            {
                Student_Object.CourseIDList = new List<int>();
                Student_Object.CourseNameList = new List<string>();
                Student_Object.CourseIDList.AddRange(ItemEntryListInt);
                Student_Object.CourseNameList.AddRange(ItemEntryListString);

                jsonTools.ModifyjSonData<jSonStudentData>(StudentID, Student_Object, MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.STUDENT_API_CONTROLLER));
            }
            catch (Exception Error)
            {

            }
            
            Close();
        }
    }
}
