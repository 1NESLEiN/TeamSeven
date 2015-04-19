using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Control;
using Model;

namespace DevelopmentProject.PeterGUI.Pages
{
    /// <summary>
    /// Interaction logic for UpdateDocumentationPage.xaml
    /// </summary>
    public partial class UpdateDocumentationPage : Page
    {
        private readonly int _id;
        private int _getSupporterId;
        private readonly Handler _handler;
        private DataTable _typesTable;
        private DataTable _supportersTable;
        private DataTable _statesTable;
        private DataTable _documentation;
        private DataTable _getSupporter;
        private readonly ContentVisibility _contentVisibility;
        /// <summary>
        /// Constructor for the UpdateDocumentationPage class
        /// </summary>
        /// <param name="id">Sets the documentation id for the selected documentation</param>
        /// <param name="contentVisibility">Sets the contentvisibility for UpdateDocumentationPage</param>
        public UpdateDocumentationPage(int id, ContentVisibility contentVisibility)
        {
            _id = id;
            _contentVisibility = contentVisibility;
            InitializeComponent();
            _handler = Handler.GetInstance();
            GetDocumentation();
            PrepareDropBoxes();
            DataContext = contentVisibility;
            FillBoxesWithSelectedDocumentationValues();
        }
        /// <summary>
        /// Back method to return to SearchDocumentationpage
        /// </summary>
        private void Back_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (NavigationService != null) NavigationService.Navigate(new SearchDocumentationPage(_contentVisibility));
        }
        /// <summary>
        /// update method for documentation
        /// </summary>
        private void UpdateDocumentation_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextBoxHeadline.Text == String.Empty || TextBoxDescription.Text == String.Empty)
                {
                    MessageBox.Show("Udfyld alle felter", "Udfyld felter", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    //Checks if the update documentation method from the handler can succeed with the parameters
                    bool success = _handler.UpdateDocumentation(_id, TextBoxHeadline.Text, TextBoxDescription.Text, Convert.ToInt32(ComboBoxType.SelectedValue), Convert.ToInt32(ComboBoxSupporter.SelectedValue), DatePickerCompleted.SelectedDate, Convert.ToInt32(TextBoxTimeSpent.Text), Convert.ToInt32(ComboBoxStatus.SelectedValue));
                    if (success)
                    {
                        MessageBox.Show("Documentation was succesfully Updated.");
                        if (NavigationService != null) NavigationService.Navigate(new SearchDocumentationPage(_contentVisibility));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Populate method to populate the comboboxes, textfields and datepickers with the given id
        /// </summary>
        private void FillBoxesWithSelectedDocumentationValues()
        {
            TextBoxHeadline.Text = _documentation.Rows[0].ItemArray[1].ToString();
            TextBoxDescription.Text = _documentation.Rows[0].ItemArray[2].ToString();
            ComboBoxType.SelectedValue = _documentation.Rows[0].ItemArray[3];
            TextBoxTimeSpent.Text = _documentation.Rows[0].ItemArray[6].ToString();
            ComboBoxSupporter.SelectedValue = _documentation.Rows[0].ItemArray[4];
            ComboBoxStatus.SelectedValue = _documentation.Rows[0].ItemArray[8];
            DatePickerCompleted.SelectedDate = _documentation.Rows[0].ItemArray[5] as DateTime?;
        }
        /// <summary>
        /// Get method to aquire all documentation properties with given id
        /// </summary>
        private void GetDocumentation()
        {
            _documentation = _handler.GetDocumentation(_id);
        }
        /// <summary>
        /// populates comboboxes
        /// </summary>
        private void PrepareDropBoxes()
        {
            //Get data and populate ComboBoxType
            _typesTable = _handler.GetTypesTable();

            ComboBoxType.ItemsSource = _typesTable.DefaultView;
            ComboBoxType.DisplayMemberPath = "Name";
            ComboBoxType.SelectedValuePath = "ID";

            //Get data Populate ComboBoxSupporter
            _supportersTable = _handler.GetSupportersWorkingTable();
            var supporterid = _documentation.Rows[0].ItemArray[4];

            //checks to see if the supporter combobox contains the supporter who created the documentation
            //if it doesn't, it will fetch and set the supporter into the supporter datatable
            DataRow[] result = _supportersTable.Select("ID = " + supporterid);
            if (result.Length == 0)
            {
                DataRow datarow = _supportersTable.NewRow();
                _getSupporterId = (int)_documentation.Rows[0].ItemArray[4];
                _getSupporter = _handler.GetSupporter(_getSupporterId);

                datarow["ID"] = _getSupporter.Rows[0].ItemArray[0];
                datarow["Name"] = _getSupporter.Rows[0].ItemArray[1];
                datarow["Initials"] = _getSupporter.Rows[0].ItemArray[2];
                datarow["Pass"] = _getSupporter.Rows[0].ItemArray[3];
                datarow["UserAccess"] = _getSupporter.Rows[0].ItemArray[4];
                datarow["Position"] = _getSupporter.Rows[0].ItemArray[5];

                _supportersTable.Rows.Add(datarow);
            }

            ComboBoxSupporter.ItemsSource = _supportersTable.DefaultView;
            ComboBoxSupporter.DisplayMemberPath = "Name";
            ComboBoxSupporter.SelectedValuePath = "ID";

            //Get data and populate ComboBoxStatus
            _statesTable = _handler.GetStatesTable();

            //restricts access to changing a status to completed, to admins only
            if (_contentVisibility.UserVisibility == false)
            {
                _statesTable.Rows.RemoveAt(2);
            }

            ComboBoxStatus.ItemsSource = _statesTable.DefaultView;
            ComboBoxStatus.DisplayMemberPath = "Name";
            ComboBoxStatus.SelectedValuePath = "ID";
        }
    }
}
