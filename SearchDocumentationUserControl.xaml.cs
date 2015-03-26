using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for SearchDocumentationUserControl.xaml
    /// </summary>
    public partial class SearchDocumentationUserControl : UserControl
    {
        private Handler _handler;
        private DataTable _typesTable;
        private DataTable _supportersTable;
        private string keyword;
        private DateTime startDate;
        private DateTime endDate;
        private int supporter;
        private int type;

        public SearchDocumentationUserControl()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
            PrepareDropBoxes();

            GridViewSearch.AutoGenerateColumns = true;
            GetFilterOptions();
            GridViewSearch.ItemsSource = new DataView(_handler.GetFilteredDocumentationsTable(keyword, startDate, endDate, supporter, type));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //GridViewSearch.DataContext = _handler.GetAllDocumentationsTable();
            GridViewSearch.AutoGenerateColumns = true;
            GetFilterOptions();
            GridViewSearch.ItemsSource = new DataView(_handler.GetFilteredDocumentationsTable(keyword, startDate, endDate, supporter, type));
        }

        public void GetFilterOptions()
        {

            keyword = KeywordTextBox.Text;
            if (StartDatePicker.SelectedDate != null) startDate = StartDatePicker.SelectedDate.Value.Date;
            if (EndDatePicker.SelectedDate != null) endDate = EndDatePicker.SelectedDate.Value.Date;
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
            KeywordTextBox.Clear();

            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            ComboBoxSupporter.SelectedValue = 0;
            ComboBoxType.SelectedValue = 0;
        }
    }
}
