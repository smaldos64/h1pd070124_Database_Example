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

namespace Database1.Windows
{
    /// <summary>
    /// Interaction logic for StudentsWindow.xaml
    /// </summary>
    public partial class StudentsWindow : Window
    {
        private CodeModelDB db = new CodeModelDB();
        private List<Student> StudentList = new List<Student>();

        public StudentsWindow()
        {
            InitializeComponent();
            UpdateStudentList();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void btnEraseStudent_Click(object sender, RoutedEventArgs e)
        {
            Button ThisButon = sender as Button;
            int StudentID = Convert.ToInt32(ThisButon.Content);

            Student Student_Object = db.Students.Find(StudentID);

            MessageBoxResult Result = MessageBox.Show("Ønsker du virkelig at slette eleven " + Student_Object.StudentName, "Slet Elev ?", MessageBoxButton.OKCancel);

            if (MessageBoxResult.OK == Result)
            {
                db.Students.Remove(Student_Object);
                db.SaveChanges();
                UpdateStudentList();
            }
        }

        private void btnModifyStudent_Click(object sender, RoutedEventArgs e)
        {
            Button ThisButon = sender as Button;
            MessageBox.Show(ThisButon.Content.ToString());
        }

        private void UpdateStudentList()
        {
            try
            {
                StudentList = db.Students.ToList();
            }
            catch (Exception Error)
            {
                int ErrNo = 10;
            }

            DataGridTextColumn TextColumn = new DataGridTextColumn();
            TextColumn.Header = "Fag1";
            TextColumn.Binding = new Binding("StudentName");
            dataStudents.Columns.Add(TextColumn);
            dataStudents.ItemsSource = StudentList;

            bool ColumnFound = false;
            int ColumnIndex = -1;
            for (int Counter = 0; Counter < dataStudents.Columns.Count; Counter++)
            {
                if ("Fag" == dataStudents.Columns[Counter].Header.ToString())
                {
                    ColumnFound = true;
                    ColumnIndex = Counter;
                }
            }

            //var index1 = (dataStudents.Columns.Single().Header.ToString() == "Fag");
            var index = dataStudents.Columns.Single(c => c.Header.ToString() == "Fag").DisplayIndex;
        }
    }
}
