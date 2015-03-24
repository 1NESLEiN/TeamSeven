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
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
          //GridViewSearch.DataContext = _handler.GetAllDocumentationsTable();
          GridViewSearch.AutoGenerateColumns = true;
          GridViewSearch.ItemsSource = new DataView(_handler.GetAllDocumentationsTable());
          GetFilterOptions();
          _handler.GetFilteredDocumentationsTable(keyword,startDate,endDate,supporter,type);
      }

       public void GetFilterOptions()
       {
           keyword = KeywordTextBox.Text;
           startDate = StartDatePicker.DisplayDate;
           endDate = EndDatePicker.DisplayDate;
           if (ComboBoxSupporter.SelectedValue != null) supporter = Int32.Parse(ComboBoxSupporter.SelectedValue.ToString());
           if (ComboBoxType.SelectedValue != null) type = Int32.Parse(ComboBoxType.SelectedValue.ToString());
          
           
           
           
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

      private void ClearTextboxes()
      {
          KeywordTextBox.Clear();

          StartDatePicker.SelectedDate = null;
          EndDatePicker.SelectedDate = null;
          ComboBoxSupporter.SelectedValue = null;
          ComboBoxType.SelectedValue = null;
      }
   }
}
