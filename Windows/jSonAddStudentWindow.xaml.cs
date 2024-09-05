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
using Database_Example.Settings;
using Database_Example.Tools;

namespace Database_Example.Windows
{
    /// <summary>
    /// Interaction logic for jSonAddStudentWindow.xaml
    /// </summary>
    /// 
    
    public partial class jSonAddStudentWindow : Window
    {
        private List<jSonTeamData> TeamList;

        private List<jSonCourseData> CourseList;
        private static List<ItemEntry> ItemEntryList = new List<ItemEntry>();

        public jSonAddStudentWindow()
        {
            InitializeComponent();

            TeamList = jsonTools.GetjSonDataList<jSonTeamData>(MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.TEAM_API_CONTROLLER));
            lvTeam.ItemsSource = TeamList;

            // Mange til Mange Relation håndtering herunder
            //CourseList = db.Courses.ToList();
            CourseList = jsonTools.GetjSonDataList<jSonCourseData>(MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.COURSE_API_CONTROLLER));
            IEnumerable<ItemEntry> ItemEntryEnumerable = from item in CourseList
                                                         select new ItemEntry()
                                                         {
                                                             Name = item.CourseName,
                                                             ID = item.CourseID,
                                                             IsSelected = false
                                                         };

            ItemEntryList = ItemEntryEnumerable.ToList();
            lbCourses.ItemsSource = ItemEntryList;
        }

        private void btnSaveStudent_Click(object sender, RoutedEventArgs e)
        {
            jSonStudentData Student_Object = new jSonStudentData();
            List<jSonCourseData> CourseListSave = new List<jSonCourseData>();

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
                //CourseListSave = CourseList.Where(i => ItemEntryListInt.Contains(i.CourseID)).ToList();
                Student_Object.CourseIDList = new List<int>();
                Student_Object.CourseNameList = new List<string>();
                Student_Object.CourseIDList.AddRange(ItemEntryListInt);
                Student_Object.CourseNameList.AddRange(ItemEntryListString);

                jsonTools.InsertjSonData<jSonStudentData>(Student_Object, MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.STUDENT_API_CONTROLLER));
            }
            catch (Exception Error)
            {

            }

            //jsonTools.InsertjSonData<jSonStudentData>(Student_Object, MainWindow.Find_WEB_API_URL(WEB_API_CONTROLLER_ENUM.STUDENT_API_CONTROLLER));
            Close();
        }
    }
}
