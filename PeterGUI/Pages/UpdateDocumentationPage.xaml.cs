﻿using System;
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

namespace DevelopmentProject.PeterGUI.Pages
{
    /// <summary>
    /// Interaction logic for UpdateDocumentationPage.xaml
    /// </summary>
    public partial class UpdateDocumentationPage : Page
    {
        private readonly int _id;
        private readonly Handler _handler;
        private DataTable _typesTable;
        private DataTable _supportersTable;
        private DataTable _statesTable;
        private DataTable _documentation;

        private DateTime? _endDate;
        private int _supporter;
        private int _type;
        private int _status;
        private string _headline ;
        private string _description;
        private string _timespent;

        public UpdateDocumentationPage(int id)
        {
            _id = id;
            InitializeComponent();
            _handler = Handler.GetInstance();
            PrepareDropBoxes();
            GetFilterOptions();
            GetDocumentation();
            FillBoxesWithSelectedDocumentationValues();
        }

        private void UpdateDocumentation_OnClick(object sender, RoutedEventArgs e)
        {

            try
            {




                MessageBox.Show("Docmentation was succesfully Updated.");
                if (NavigationService != null) NavigationService.Navigate(new SearchDocumentationPage());
            }
            catch (Exception)
            {
                
                throw;
            }
        }

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
        private void GetDocumentation()
        {
            _documentation = _handler.GetDocumentation(_id);
        }
        public void GetFilterOptions()
        {
            if (TextBoxHeadline.Text != null) _headline = TextBoxDescription.Text;
            if (TextBoxDescription.Text != null) _description = TextBoxDescription.Text;
            if (TextBoxTimeSpent.Text != null) _timespent = TextBoxTimeSpent.Text;
            if (DatePickerCompleted.SelectedDate != null) _endDate = DatePickerCompleted.SelectedDate.Value.Date;
            if (ComboBoxSupporter.SelectedValue != null || Convert.ToInt32(ComboBoxSupporter.SelectedValue) > 0) _supporter = Int32.Parse(ComboBoxSupporter.SelectedValue.ToString());
            if (ComboBoxType.SelectedValue != null || Convert.ToInt32(ComboBoxType.SelectedValue) > 0) _type = Int32.Parse(ComboBoxType.SelectedValue.ToString());
            if (ComboBoxStatus.SelectedValue != null || Convert.ToInt32(ComboBoxStatus.SelectedValue) > 0) _status = Int32.Parse(ComboBoxStatus.SelectedValue.ToString());
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

            //Get data and populate ComboBoxStatus
            _statesTable = _handler.GetStatesTable();

            ComboBoxStatus.ItemsSource = _statesTable.DefaultView;
            ComboBoxStatus.DisplayMemberPath = "Name";
            ComboBoxStatus.SelectedValuePath = "ID";

        }
    }
}
