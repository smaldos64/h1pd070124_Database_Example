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
using Database1.ViewModels;
using Database1.Models;

namespace Database1.Windows
{
    /// <summary>
    /// Interaction logic for StudentCourseMethod1Window.xaml
    /// </summary>
    public partial class StudentCourseMethod1Window : Window
    {
        private CodeModelDB db = new CodeModelDB();
        private List<Student> StudentList = new List<Student>();

        public StudentCourseMethod1Window()
        {
            InitializeComponent();
            CreateDataGrid();
        }

        private void CreateDataGrid()
        {
            //StudentList = db.Students.ToList();

            DataGridTextColumn Column_Object1 = new DataGridTextColumn();
            Column_Object1.Header = "Name";
            Column_Object1.Binding = new Binding("Student_Object.StudentName");
            Column_Object1.Width = 120;
            dataStudents.Columns.Add(Column_Object1);

            DataGridTextColumn Column_Object2 = new DataGridTextColumn();
            Column_Object2.Header = "Fødsesldato";
            Column_Object2.Binding = new Binding("Path=Student_Object.DateOfBirth");
            Column_Object2.Width = 120;
            dataStudents.Columns.Add(Column_Object2);

            DataGridTextColumn Column_Object3 = new DataGridTextColumn();
            Column_Object3.Header = "Foto";
            Column_Object3.Binding = new Binding("Student_Object.PhotoURL");
            Column_Object3.Width = 120;
            dataStudents.Columns.Add(Column_Object3);

            DataGridTextColumn Column_Object4 = new DataGridTextColumn();
            Column_Object4.Header = "Højde";
            Column_Object4.Binding = new Binding("Student_Object.Height");
            Column_Object4.Width = 74;
            dataStudents.Columns.Add(Column_Object4);

            DataGridTextColumn Column_Object5 = new DataGridTextColumn();
            Column_Object5.Header = "Vægt";
            Column_Object5.Binding = new Binding("Student_Object.Weight");
            Column_Object5.Width = 74;
            dataStudents.Columns.Add(Column_Object5);

            DataGridTextColumn Column_Object6 = new DataGridTextColumn();
            Column_Object6.Header = "Klasse";
            Column_Object6.Binding = new Binding("Student_Object.Standard.StandardName");
            Column_Object6.Width = 74;
            dataStudents.Columns.Add(Column_Object6);

            DataGridTextColumn Column_Object7 = new DataGridTextColumn();
            Column_Object7.Header = "Fag";
            Column_Object7.Binding = new Binding("StudentCourseList");
            Column_Object7.Width = 74;
            dataStudents.Columns.Add(Column_Object7);

            DataGridTextColumn Column_Object8 = new DataGridTextColumn();
            Column_Object8.Header = "Fag";
            Column_Object8.Binding = new Binding("StudentCourseString");
            Column_Object8.Width = 74;
            dataStudents.Columns.Add(Column_Object8);

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
                StudentCourseViewModel1 StudentCourseMethod1Window_Object = new StudentCourseViewModel1(Student_Object);

                StudentCourseMethod1Window_Object.SetCourses(Student_Object);

                dataStudents.Items.Add(StudentCourseMethod1Window_Object);
            }
        }

        public static void SelectRowByIndex(DataGrid dataGrid, int rowIndex)
        {
            if (!dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.FullRow))
                throw new ArgumentException("The SelectionUnit of the DataGrid must be set to FullRow.");

            if (rowIndex < 0 || rowIndex > (dataGrid.Items.Count - 1))
                throw new ArgumentException(string.Format("{0} is an invalid row index.", rowIndex));

            dataGrid.SelectedItems.Clear();
            /* set the SelectedItem property */
            object item = dataGrid.Items[rowIndex]; // = Product X
            dataGrid.SelectedItem = item;

            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            if (row == null)
            {
                /* bring the data item (Product object) into view
                 * in case it has been virtualized away */
                dataGrid.ScrollIntoView(item);
                row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            }
            //TODO: Retrieve and focus a DataGridCell object
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            //if (StudentCourseViewModel_Object != null)
            //    StudentCourseViewModel_Object.Dispose();
        }

    }
}
