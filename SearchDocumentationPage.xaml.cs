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
    /// Interaction logic for SearchDocumentationPage.xaml
    /// </summary>
    public partial class SearchDocumentationPage : Page
    {
        private Handler _handler;
        private DataTable _typesTable;
        private DataTable _supportersTable;
        private string keyword;
        private DateTime startDate;
        private DateTime endDate;
        private int supporter;
        private int type;

        public SearchDocumentationPage()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
            PrepareDropBoxes();

            Search();
        }

        public void Search()
        {
            DataGridSearch.AutoGenerateColumns = true;
            GetFilterOptions();
            DataGridSearch.ItemsSource = new DataView(_handler.GetFilteredDocumentationsTable(keyword, startDate, endDate, supporter, type));
            if (DataGridSearch.Columns.Count > 0)
            {
                DataGridSearch.Columns[0].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                DataGridSearch.Columns[1].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                DataGridSearch.Columns[2].Width = new DataGridLength(3, DataGridLengthUnitType.Star);
                DataGridSearch.Columns[3].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                DataGridSearch.Columns[4].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                DataGridSearch.Columns[5].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                DataGridSearch.Columns[6].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                DataGridSearch.Columns[7].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                DataGridSearch.Columns[8].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            }
            else
            {
                DataGridSearch.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void ButtonSearch_OnClick(object sender, RoutedEventArgs e)
        {
            Search();
        }

        public void GetFilterOptions()
        {

            keyword = TextBoxKeyword.Text;
            if (DatePickerFromDate.SelectedDate != null) startDate = DatePickerFromDate.SelectedDate.Value.Date;
            if (DatePickerToDate.SelectedDate != null) endDate = DatePickerToDate.SelectedDate.Value.Date;
            if (ComboBoxSupporter.SelectedValue != null || Convert.ToInt32(ComboBoxSupporter.SelectedValue) > 0) supporter = Int32.Parse(ComboBoxSupporter.SelectedValue.ToString());
            if (ComboBoxType.SelectedValue != null || Convert.ToInt32(ComboBoxType.SelectedValue) > 0) type = Int32.Parse(ComboBoxType.SelectedValue.ToString());
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

            //Get data Populate ComboBoxSupporter
            _supportersTable = _handler.GetSupportersTable();

            DataRow supporterRow = _supportersTable.NewRow();
            supporterRow["ID"] = 0;
            supporterRow["Name"] = "Alle supportere";
            supporterRow["Initials"] = "All";

            _supportersTable.Rows.InsertAt(supporterRow, 0);

            ComboBoxSupporter.ItemsSource = _supportersTable.DefaultView;
            ComboBoxSupporter.DisplayMemberPath = "Name";
            ComboBoxSupporter.SelectedValuePath = "ID";
            ComboBoxSupporter.SelectedValue = 0;
        }

        private void ClearTextboxes()
        {
            TextBoxKeyword.Clear();

            DatePickerFromDate.SelectedDate = null;
            DatePickerToDate.SelectedDate = null;
            ComboBoxSupporter.SelectedValue = 0;
            ComboBoxType.SelectedValue = 0;
        }
    }
}
