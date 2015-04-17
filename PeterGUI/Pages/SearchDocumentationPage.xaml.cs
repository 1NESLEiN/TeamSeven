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
        private int _status;
       private ContentVisibility _contentVisibility;

        public ContentVisibility ContentVisibility { get; set; }
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

        public void Search()
        {
            GridViewSearch.AutoGenerateColumns = true;
            GetFilterOptions();
            GridViewSearch.ItemsSource = new DataView(_handler.GetFilteredDocumentationsTable(_keyword, _startDate, _endDate, _supporter, _type, _status));

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
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {

            DataRowView rowview = GridViewSearch.SelectedItem as DataRowView;

            if (rowview != null)
            {
               int selectedId = (int)rowview.Row.ItemArray[0];
               string statusname = rowview.Row.ItemArray[7].ToString();

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
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
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
            if (ComboBoxState.SelectedValue != null || Convert.ToInt32(ComboBoxState.SelectedValue) > 0)
                if (ComboBoxState.SelectedValue != null) _status = Int32.Parse(ComboBoxState.SelectedValue.ToString());
        }

        private void PrepareDropBoxes()
        {
            //Get data and populate ComboBoxType
            _typesTable = _handler.GetTypesTable();

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

            DataRow statesRow = _statesTable.NewRow();
            statesRow["ID"] = 0;
            statesRow["Name"] = "Alle Statuser";

            _statesTable.Rows.InsertAt(statesRow, 0);

            ComboBoxState.ItemsSource = _statesTable.DefaultView;
            ComboBoxState.DisplayMemberPath = "Name";
            ComboBoxState.SelectedValuePath = "ID";
            ComboBoxState.SelectedValue = 0;

        }
    }
}
