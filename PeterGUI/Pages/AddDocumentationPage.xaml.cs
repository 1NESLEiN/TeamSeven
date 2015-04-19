using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Control;

namespace DevelopmentProject.PeterGUI.Pages
{
    /// <summary>
    /// Interaction logic for AddDocumentationPage.xaml
    /// </summary>
    public partial class AddDocumentationPage : Page
    {
        private readonly Handler _handler;
        private DataTable _typesTable;
        private DataTable _supportersTable;
        /// <summary>
        /// Constructor for the AddDocumentationPage class
        /// </summary>
        public AddDocumentationPage()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
            PrepareDropBoxes();
        }
        /// <summary>
        /// Add documentation method
        /// </summary>
        private void AddDocumentation_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Sets the date created to be today if left empty
                if (!DateCreateDatePicker.SelectedDate.HasValue)
                {
                    DateCreateDatePicker.SelectedDate = DateTime.Today.Date;
                }

                //Checks to see if the Add documentation method from the handler can succeed with the parameters selected
                bool success = _handler.AddDocumentation(TextBoxHeadline.Text, TextBoxDescription.Text, Convert.ToInt32(ComboBoxType.SelectedValue), Convert.ToInt32(ComboBoxSupporter.SelectedValue), null, Convert.ToInt32(TextBoxTimeSpent.Text), DateCreateDatePicker.SelectedDate.Value.Date, 1);
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
        /// <summary>
        /// Clear textboxes method
        /// </summary>
        private void ClearTextboxes()
        {
            TextBoxHeadline.Clear();
            TextBoxDescription.Clear();
            TextBoxTimeSpent.Clear();
            ComboBoxSupporter.SelectedValue = null;
            ComboBoxType.SelectedValue = null;
        }
        /// <summary>
        /// Populate Comboboxes method
        /// </summary>
        private void PrepareDropBoxes()
        {
            //Get data and populate ComboBoxType
            _typesTable = _handler.GetTypesTable();
            ComboBoxType.ItemsSource = _typesTable.DefaultView;
            ComboBoxType.DisplayMemberPath = "Name";
            ComboBoxType.SelectedValuePath = "ID";

            //Get data Populate ComboBoxSupporterDelete
            _supportersTable = _handler.GetSupportersWorkingTable();
            ComboBoxSupporter.ItemsSource = _supportersTable.DefaultView;
            ComboBoxSupporter.DisplayMemberPath = "Name";
            ComboBoxSupporter.SelectedValuePath = "ID";
        }
    }
}
