using System;
using System.Collections.Generic;
using System.Data;
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
using Control;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for AddDocumentationPage.xaml
    /// </summary>
    public partial class AddDocumentationPage : Page
    {
        private Handler _handler;
        private DataTable _typesTable;
        private DataTable _supportersTable;

        public AddDocumentationPage()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();

            PrepareDropBoxes();
        }

        private void AddDocumentation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool success = _handler.AddDocumentation(Convert.ToInt32(ComboBoxType.SelectedValue), TextBoxHeadline.Text, TextBoxDescription.Text, DatePickerDateCompleted.DisplayDate, Convert.ToInt32(TextBoxTimeSpent.Text), Convert.ToInt32(ComboBoxSupporter.SelectedValue));
                if (success)
                {
                    MessageBox.Show("Documentation added succesfully");
                    ClearTextboxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextboxes()
        {
            TextBoxHeadline.Clear();
            TextBoxDescription.Clear();
            TextBoxTimeSpent.Clear();
            DatePickerDateCompleted.SelectedDate = null;
            ComboBoxSupporter.SelectedValue = null;
            ComboBoxType.SelectedValue = null;
        }

        private void PrepareDropBoxes()
        {
            //Get data and populate ComboBoxType
            _typesTable = _handler.GetTypesTable();
            ComboBoxType.ItemsSource = _typesTable.DefaultView;
            ComboBoxType.DisplayMemberPath = "Name";
            ComboBoxType.SelectedValuePath = "ID";

            //Get data Populate ComboBoxSupporter
            _supportersTable = _handler.GetSupportersTable();
            ComboBoxSupporter.ItemsSource = _supportersTable.DefaultView;
            ComboBoxSupporter.DisplayMemberPath = "Name";
            ComboBoxSupporter.SelectedValuePath = "ID";
        }
    }
}
