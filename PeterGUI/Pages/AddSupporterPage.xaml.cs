using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Control;

namespace DevelopmentProject.PeterGUI.Pages
{
    /// <summary>
    /// Interaction logic for AddSupporterPage.xaml
    /// </summary>
    public partial class AddSupporterPage : Page
    {
        private DataTable _supportersTable;
        private DataTable _accessTable;

        public AddSupporterPage()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
            PopulateCombo();
        }

        private readonly Handler _handler;

        private void AddSupporter_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextBoxSupporterName.Text == String.Empty || TextBoxSupporterInitials.Text == String.Empty || TextBoxPassword.Text == String.Empty || ComboBoxAccess.SelectedValue == null)
                {
                    MessageBox.Show("Udfyld alle felter", "Udfyld felter", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {

                    bool success = _handler.AddSupporter(TextBoxSupporterName.Text, TextBoxSupporterInitials.Text, TextBoxPassword.Text, Convert.ToInt32(ComboBoxAccess.SelectedValue), 1);
                    if (success)
                    {
                        MessageBox.Show("Supporter added succesfully");
                        TextBoxSupporterName.Clear();
                        TextBoxSupporterInitials.Clear();
                        TextBoxPassword.Clear();
                        PopulateCombo();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Der opstod en fejl: " + exception.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void PopulateCombo()
        {
            //Get data Populate both ComboBoxSupporter boxes
            _supportersTable = _handler.GetSupportersTable();

            //DataRow supporterRow = _supportersTable.NewRow();
            //supporterRow["ID"] = 0;
            //supporterRow["Name"] = "";
            //supporterRow["Initials"] = "";

            //_supportersTable.Rows.InsertAt(supporterRow, 0);

            ComboBoxSupporter.ItemsSource = _supportersTable.DefaultView;
            ComboBoxSupporter.DisplayMemberPath = "Name";
            ComboBoxSupporter.SelectedValuePath = "ID";
            ComboBoxSupporter.SelectedValue = 0;

            ComboBox2Supporter.ItemsSource = _supportersTable.DefaultView;
            ComboBox2Supporter.DisplayMemberPath = "Name";
            ComboBox2Supporter.SelectedValuePath = "ID";
            ComboBox2Supporter.SelectedValue = 0;

            //Get data Populate ComboBoxAccess
            _accessTable = _handler.GetUserAccessTable();

            //DataRow supporterRow = _supportersTable.NewRow();
            //supporterRow["ID"] = 0;
            //supporterRow["Name"] = "";
            //supporterRow["Initials"] = "";

            //_supportersTable.Rows.InsertAt(supporterRow, 0);

            ComboBoxAccess.ItemsSource = _accessTable.DefaultView;
            ComboBoxAccess.DisplayMemberPath = "Name";
            ComboBoxAccess.SelectedValuePath = "ID";
        }

        private void RemoveSupporter_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComboBoxSupporter.SelectedValue != null)
            {
                int supporterId = Convert.ToInt32(ComboBoxSupporter.SelectedValue);

                var result = MessageBox.Show("Are you certain you wish to remove " + ComboBoxSupporter.Text, "Confirm",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _handler.DeleteSupporter(supporterId);
                    PopulateCombo();
                }
            }
        }

        private void ResignSupporter_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComboBox2Supporter.SelectedValue != null)
            {
                int supporterId = Convert.ToInt32(ComboBox2Supporter.SelectedValue);

                var result = MessageBox.Show("Are you certain you wish to resign " + ComboBox2Supporter.Text, "Confirm",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _handler.ResignSupporter(supporterId);
                    PopulateCombo();
                }
            }
        }
    }
}
