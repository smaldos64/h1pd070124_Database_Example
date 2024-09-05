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

namespace Database_Example.Windows
{
    /// <summary>
    /// Interaction logic for ModifyStudentWindow.xaml
    /// </summary>
    public partial class ModifyStudentWindow : Window
    {
        private int StudentID = 0;
        public Student Student_Object;
        private List<Team> TeamList;
        private List<Course> CourseList;
        private DatabaseContext db = new DatabaseContext();

        private Student Student_Object_Backup;

        private static List<ItemEntry> ItemEntryList = new List<ItemEntry>();

        public ModifyStudentWindow(int StudentID)
        //public ModifyStudentWindow(ref Student StudentObjectFunc)
            
        {
            this.DataContext = new ItemEntry();
            InitializeComponent();
            this.StudentID = StudentID;
            
            //this.Student_Object_Backup = StudentObjectFunc;
            //StudentID = Student_Object_Backup.StudentID;

            TeamList = db.Teams.ToList();
            Student_Object = db.Students.Find(StudentID);

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

            CourseList = db.Courses.ToList();
            IEnumerable<ItemEntry> ItemEntryEnumerable = from item in CourseList
                                                         select new ItemEntry()
                                                         {
                                                            Name = item.CourseName,
                                                            ID = item.CourseID,
                                                            IsSelected = Student_Object.Courses.Contains<Course>(item)
                                                        };

            ItemEntryList = ItemEntryEnumerable.ToList();
            lbCourses.ItemsSource = ItemEntryList;
            
            txtStudentName.Text = Student_Object.StudentName;
            txtStudentLastName.Text = Student_Object.StudentLastName;
        }

        private void btnSaveModifiedStudent_Click(object sender, RoutedEventArgs e)
        {
            Student_Object.StudentName = txtStudentName.Text;
            Student_Object.StudentLastName = txtStudentLastName.Text;
            Student_Object.TeamID = TeamList.ElementAt(lvTeam.SelectedIndex).TeamID;
            //string TeamName = TeamList.ElementAt(lvTeam.SelectedIndex).TeamName;

            // Anden måde at lave ovennævnte kode på !!!
            //Team Team_Object = TeamList.ElementAt(lvTeam.SelectedIndex);
            //Student_Object.TeamID = Team_Object.TeamID;
            
            // Mange til Mange Relation håndtering herunder
            List<int> ItemEntryListInt = new List<int>();
            foreach(ItemEntry ItemEntry_Object in ItemEntryList)
            {
                if (true == ItemEntry_Object.IsSelected)
                {
                    ItemEntryListInt.Add(ItemEntry_Object.ID);
                }
            }
            //ItemEntryListInt.AddRange(ItemEntryList.Select(j => j.ID));
            
            CourseList.Clear();
            try
            {
                //CourseList = db.Courses.Where(i => (ItemEntryList.Select(j => j.ID)).Contains(i.CourseID)).ToList();
                CourseList = db.Courses.Where(i => ItemEntryListInt.Contains(i.CourseID)).ToList();
                
                Student_Object.Courses.Clear();
                Student_Object.Courses.AddRange(CourseList);

                int NumberOfObjectsSavedInDB = db.SaveChanges();
                //Student_Object_Backup = (Student)Student_Object;
            }
            catch (Exception Error)
            {
                string ErrorString = Error.ToString();
            }

            Close();
        }
    }
}
