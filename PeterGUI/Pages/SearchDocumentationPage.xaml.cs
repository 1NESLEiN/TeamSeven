using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Control;
using Model;

namespace DevelopmentProject.PeterGUI.Pages
{
    /// <summary>
    /// Interaction logic for SearchDocumentationPage.xaml
    /// </summary>
    public partial class SearchDocumentationPage : Page
    {
        private readonly Handler _handler;
        private DataTable _typesTable;
        private DataTable _supportersTable;
        private DataTable _statesTable;
        private string _keyword;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _supporter;
        private int _type;
        private string _status;
        private readonly ContentVisibility _contentVisibility;

        public ContentVisibility ContentVisibility { get; set; }
        /// <summary>
        /// Constructor for the SearchDocumentationPage class
        /// </summary>
        /// <param name="contentVisibility">Sets the contentvisibility for SearchDocumentationPage</param>
        public SearchDocumentationPage(ContentVisibility contentVisibility)
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
            PrepareDropBoxes();
            ContentVisibility = contentVisibility;
            _contentVisibility = contentVisibility;
            Search();
        }
        public SearchDocumentationPage()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
            PrepareDropBoxes();
            ContentVisibility = _contentVisibility;
            Search();
        }
        /// <summary>
        /// Search method to populate gridview
        /// </summary>
        public void Search()
        {
            //Ensures validation if no checkbox is checked
            if (CheckBoxAll.IsChecked == false && CheckBoxNew.IsChecked == false && CheckBoxPartly.IsChecked == false && CheckBoxComplete.IsChecked == false)
            {
                MessageBox.Show("You need to select a status");
            }
            else
            {
                //If checkbox all is checked then the other checkboxes should be rendered irrelevant
                if (CheckBoxAll.IsChecked == true)
                {
                    CheckBoxNew.IsChecked = false;
                    CheckBoxPartly.IsChecked = false;
                    CheckBoxComplete.IsChecked = false;

                    _status = 0.ToString();
                }
                //Adds a number for each checkbox checked to a string, that is ultimately turned into an integer
                else
                {
                    int counter = 0;
                    if (CheckBoxNew.IsChecked == true)
                    {
                        if (counter == 0)
                        {
                            _status = 1.ToString();
                            counter = 1;
                        }
                        else
                        {
                            _status += 1;
                        }
                    }
                    if (CheckBoxPartly.IsChecked == true)
                    {
                        if (counter == 0)
                        {
                            _status = 2.ToString();
                            counter = 1;
                        }
                        else
                        {
                            _status += 2;
                        }
                    }
                    if (CheckBoxComplete.IsChecked == true)
                    {
                        if (counter == 0)
                        {
                            _status = 3.ToString();
                        }
                        else
                        {
                            _status += 3;
                        }
                    }
                }
            }

            GridViewSearch.AutoGenerateColumns = true;
            GetFilterOptions();
            GridViewSearch.ItemsSource = new DataView(_handler.GetFilteredDocumentationsTable(_keyword, _startDate, _endDate, _supporter, _type, Convert.ToInt32(_status)));

            //sets column widths to gridview
            if (GridViewSearch.Columns.Count > 0)
            {
                GridViewSearch.Columns[0].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                GridViewSearch.Columns[1].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                GridViewSearch.Columns[2].Width = new DataGridLength(3, DataGridLengthUnitType.Star);
                GridViewSearch.Columns[3].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                GridViewSearch.Columns[4].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                GridViewSearch.Columns[5].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                GridViewSearch.Columns[6].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                GridViewSearch.Columns[7].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                GridViewSearch.Columns[8].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                GridViewSearch.Columns[9].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            }
            else
            {
                GridViewSearch.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
        /// <summary>
        /// Update method to open UpdateDocumentationPage with a selected row
        /// </summary>
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            //Selects rowview from gridview
            var rowview = GridViewSearch.SelectedItem as DataRowView;

            if (rowview != null)
            {
                int selectedId = (int)rowview.Row.ItemArray[0];
                string statusname = rowview.Row.ItemArray[7].ToString();

                //Restricts access for supports to documentations that are marked as completed
                if (_contentVisibility.UserVisibility == false && statusname == "Færdig")
                {
                    MessageBox.Show("Du har ikke adgang til at rette I en dokumentation der er sat til færdig");
                }
                else
                {
                    if (NavigationService != null) NavigationService.Navigate(new UpdateDocumentationPage(selectedId, ContentVisibility));
                }
            }
        }
        /// <summary>
        /// Button click to initiate search method
        /// </summary>
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
        /// <summary>
        /// Filter method for search options, sets selected fields to private variables
        /// </summary>
        public void GetFilterOptions()
        {
            _keyword = KeywordTextBox.Text;
            if (StartDatePicker.SelectedDate != null) _startDate = StartDatePicker.SelectedDate.Value.Date;
            if (EndDatePicker.SelectedDate != null) _endDate = EndDatePicker.SelectedDate.Value.Date;
            if (ComboBoxSupporter.SelectedValue != null || Convert.ToInt32(ComboBoxSupporter.SelectedValue) > 0)
                if (ComboBoxSupporter.SelectedValue != null)
                    _supporter = Int32.Parse(ComboBoxSupporter.SelectedValue.ToString());
            if (ComboBoxType.SelectedValue != null || Convert.ToInt32(ComboBoxType.SelectedValue) > 0)
                if (ComboBoxType.SelectedValue != null) _type = Int32.Parse(ComboBoxType.SelectedValue.ToString());
            //if (ComboBoxState.SelectedValue != null || Convert.ToInt32(ComboBoxState.SelectedValue) > 0)
            //    if (ComboBoxState.SelectedValue != null) _status = Int32.Parse(ComboBoxState.SelectedValue.ToString());
        }
        /// <summary>
        /// Populate comboboxes method
        /// </summary>
        private void PrepareDropBoxes()
        {
            //Get data and populate ComboBoxType
            _typesTable = _handler.GetTypesTable();

            //Inserts an extra row with an all parameter
            DataRow typeRow = _typesTable.NewRow();
            typeRow["ID"] = 0;
            typeRow["Name"] = "Alle typer";

            _typesTable.Rows.InsertAt(typeRow, 0);

            ComboBoxType.ItemsSource = _typesTable.DefaultView;
            ComboBoxType.DisplayMemberPath = "Name";
            ComboBoxType.SelectedValuePath = "ID";
            ComboBoxType.SelectedValue = 0;

            //Get data Populate ComboBoxSupporterDelete
            _supportersTable = _handler.GetSupportersTable();

            //Inserts an extra row with an all parameter
            DataRow supporterRow = _supportersTable.NewRow();
            supporterRow["ID"] = 0;
            supporterRow["Name"] = "Alle supportere";
            supporterRow["Initials"] = "All";
            supporterRow["Pass"] = "All";
            supporterRow["UserAccess"] = 1;
            supporterRow["Position"] = 1;

            _supportersTable.Rows.InsertAt(supporterRow, 0);

            ComboBoxSupporter.ItemsSource = _supportersTable.DefaultView;
            ComboBoxSupporter.DisplayMemberPath = "Name";
            ComboBoxSupporter.SelectedValuePath = "ID";
            ComboBoxSupporter.SelectedValue = 0;

            //Get data and populate ComboBoxStatus
            _statesTable = _handler.GetStatesTable();

            //Inserts an extra row with an all parameter
            DataRow statesRow = _statesTable.NewRow();
            statesRow["ID"] = 0;
            statesRow["Name"] = "Alle Statuser";

            _statesTable.Rows.InsertAt(statesRow, 0);

            //ComboBoxState.ItemsSource = _statesTable.DefaultView;
            //ComboBoxState.DisplayMemberPath = "Name";
            //ComboBoxState.SelectedValuePath = "ID";
            //ComboBoxState.SelectedValue = 0;

        }
    }
}
