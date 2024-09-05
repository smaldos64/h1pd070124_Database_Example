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
    /// Interaction logic for StudentCourseWindow.xaml
    /// </summary>
    /// 
    public partial class StudentCourseWindow : Window
    {
        private readonly StudentCourseViewModel StudentCourseViewModel_Object = new StudentCourseViewModel();

        public StudentCourseWindow()
        {
            InitializeComponent();
            this.DataContext = StudentCourseViewModel_Object;
            CreateDataGrid();
        }

        private void CreateDataGrid()
        {
            this.dataGrid.AutoGenerateColumns = false;
            this.dataGrid.CanUserAddRows = false;

            /* Add a column for the displaying the name of the Student */
            this.dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Student",
                Binding = new Binding("StudentName")
                {
                    Mode = BindingMode.OneWay
                }
            });

            this.dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Height",
                Binding = new Binding("Height")
                {
                    Mode = BindingMode.OneWay
                }
            });

            this.dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Weight",
                Binding = new Binding("Weight")
                {
                    Mode = BindingMode.OneWay
                }
            });

            this.dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Team",
                Binding = new Binding("Standard.StandardName")
                {
                    Mode = BindingMode.OneWay
                }
            });
            
            /* Add a column for each group */
            foreach (Course Course_Object in StudentCourseViewModel_Object.Courses)
            {
                DataGridCheckBoxColumn chkBoxColumn = new DataGridCheckBoxColumn();
                chkBoxColumn.Header = Course_Object.CourseName;

                Binding binding = new Binding("Courses");
                CoursesToBooleanConverter converter = new CoursesToBooleanConverter();
                binding.Converter = converter;
                binding.ConverterParameter = Course_Object;
                binding.Mode = BindingMode.OneWay;
                chkBoxColumn.Binding = binding;

                this.dataGrid.Columns.Add(chkBoxColumn);
            }

            /* Bind the ItemsSource property of the DataGrid to the Users collection */
            this.dataGrid.SetBinding(DataGrid.ItemsSourceProperty, "Students");
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (StudentCourseViewModel_Object != null)
                StudentCourseViewModel_Object.Dispose();
        }


    }
}
