using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows;
using Control;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Handler _handler;
        private DataTable _supporterTable;
        private DataTable _typeTable;

        public MainWindow()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
            FillSupporterComboBox();
            FillTypeComboBox();
        }

        private void FillSupporterComboBox()
        {
            _supporterTable = _handler.ViewAllSupporters();
            CmbBoxDocumentationSupporter.ItemsSource = _supporterTable.DefaultView;
            CmbBoxDocumentationSupporter.SelectedValuePath = "SupporterID";
            CmbBoxDocumentationSupporter.DisplayMemberPath = "Navn";
        }

        private void FillTypeComboBox()
        {
            _typeTable = _handler.ViewAllTypes();
            cmbBoxTypes.ItemsSource = _typeTable.DefaultView;
            cmbBoxTypes.SelectedValuePath = "TypeID";
            cmbBoxTypes.DisplayMemberPath = "Navn";

        }

        private void AddSupporter(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TxtBoxName.Text == String.Empty || TxtBoxInitials.Text == String.Empty)
                {
                    MessageBox.Show("Udfyld alle felter", "Udfyld felter", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    _handler.AddSupporter(TxtBoxName.Text, TxtBoxInitials.Text);
                    FillSupporterComboBox();
                    FillTypeComboBox();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Der opstod en fejl: " + exception.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddDocumentation(object sender, RoutedEventArgs e)
        {
            _handler.AddDocumentation(TxtBoxDocumentationHeadline.Text,
                TxtBoxDocumentationDescription.Text,
                Convert.ToInt32(cmbBoxTypes.SelectedValue),
                Convert.ToInt32(CmbBoxDocumentationSupporter.SelectedValue),
                Convert.ToDateTime(datePickerDateCreated.SelectedDate),
                Convert.ToInt32(TxtBoxDocumentationTimeSpent.Text)
                );
            FillSupporterComboBox();
            FillTypeComboBox();
        }
    }
}
