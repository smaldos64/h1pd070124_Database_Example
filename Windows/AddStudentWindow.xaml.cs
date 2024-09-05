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
using Database_Example;

namespace Database_Example.Windows
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        public Student Student_Object;
        private List<Team> TeamList = new List<Team>();
        private DatabaseContext db = new DatabaseContext();

        private List<Course> CourseList;
        private static List<ItemEntry> ItemEntryList = new List<ItemEntry>();

        public AddStudentWindow()
        {
            InitializeComponent();

            //TeamList.Add(new Models.Team(0, "Test"));
            TeamList.AddRange(db.Teams.OrderBy(t => t.TeamID).ToList());
                        
            lvTeam.ItemsSource = TeamList;

            // Mange til Mange Relation håndtering herunder
            CourseList = db.Courses.ToList();
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
            Student_Object = new Student();

            Student_Object.StudentName = txtStudentName.Text;
            Student_Object.StudentLastName = txtStudentLastName.Text;
            Student_Object.TeamID = TeamList.ElementAt(lvTeam.SelectedIndex).TeamID;

            // Mange til Mange Relation håndtering herunder
            List<int> ItemEntryListInt = new List<int>();
            foreach (ItemEntry ItemEntry_Object in ItemEntryList)
            {
                if (true == ItemEntry_Object.IsSelected)
                {
                    ItemEntryListInt.Add(ItemEntry_Object.ID);
                }
            }
         
            CourseList.Clear();
            try
            {
                CourseList = db.Courses.Where(i => ItemEntryListInt.Contains(i.CourseID)).ToList();

                Student_Object.Courses = new List<Course>();
                Student_Object.Courses.AddRange(CourseList);

                db.Students.Add(Student_Object);
                db.SaveChanges();
            }
            catch (Exception Error)
            {

            }
            
            Close();
        }
    }
}
