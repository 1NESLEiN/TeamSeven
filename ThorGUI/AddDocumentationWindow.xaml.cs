﻿using System;
using System.Data;
using System.Windows;
using Control;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for AddDocumentationWindow.xaml
    /// </summary>
    public partial class AddDocumentationWindow : Window
    {
        private Handler _handler;
        private DataTable _typesTable;
        private DataTable _supportersTable;
        public AddDocumentationWindow()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
            
            PrepareDropBoxes();
        }

        private void AddDocumentation_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bool success = _handler.AddDocumentation(Convert.ToInt32(ComboBoxType.SelectedValue), TextBoxHeadline.Text, TextBoxDescription.Text, DatePickerDateCompleted.DisplayDate, Convert.ToInt32(TextBoxTimeSpent.Text), Convert.ToInt32(ComboBoxSupporter.SelectedValue), 1);
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
