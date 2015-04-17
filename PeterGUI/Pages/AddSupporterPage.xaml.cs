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
        private DataTable _supportersWorkingTable;
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
            //Get data Populate both ComboBoxSupporterDelete, ComboBoxSupporterResign and ComboboxAssign boxes
            _supportersTable = _handler.GetSupportersTable();
            _supportersWorkingTable = _handler.GetSupportersWorkingTable();

            ComboBoxSupporterDelete.ItemsSource = _supportersTable.DefaultView;
            ComboBoxSupporterDelete.DisplayMemberPath = "Name";
            ComboBoxSupporterDelete.SelectedValuePath = "ID";
            ComboBoxSupporterDelete.SelectedValue = 0;

            ComboBoxSupporterResign.ItemsSource = _supportersWorkingTable.DefaultView;
            ComboBoxSupporterResign.DisplayMemberPath = "Name";
            ComboBoxSupporterResign.SelectedValuePath = "ID";
            ComboBoxSupporterResign.SelectedValue = 0;

            ComboBoxSupporterAssign.ItemsSource = _supportersWorkingTable.DefaultView;
            ComboBoxSupporterAssign.DisplayMemberPath = "Name";
            ComboBoxSupporterAssign.SelectedValuePath = "ID";
            ComboBoxSupporterAssign.SelectedValue = 0;

            //Get data Populate ComboBoxAccess And combobox UserAccess boxes
            _accessTable = _handler.GetUserAccessTable();

            ComboBoxAccess.ItemsSource = _accessTable.DefaultView;
            ComboBoxAccess.DisplayMemberPath = "Name";
            ComboBoxAccess.SelectedValuePath = "ID";

            ComboBoxUserAccessAssign.ItemsSource = _accessTable.DefaultView;
            ComboBoxUserAccessAssign.DisplayMemberPath = "Name";
            ComboBoxUserAccessAssign.SelectedValuePath = "ID";
        }

        private void RemoveSupporter_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComboBoxSupporterDelete.SelectedValue != null)
            {
                int supporterId = Convert.ToInt32(ComboBoxSupporterDelete.SelectedValue);

                var result = MessageBox.Show("Are you certain you wish to remove " + ComboBoxSupporterDelete.Text + " and all of the documentations affiliated?", "Confirm",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (_handler.LookForAdmin().Rows.Count == 1)
                {
                    MessageBox.Show("There is only 1 admin left. Deletion cannot take place.");
                }
                else
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        _handler.DeleteSupporter(supporterId);
                        PopulateCombo();
                        MessageBox.Show("Supporter was successfully removed");
                    }
                }

            }
        }

        private void ResignSupporter_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComboBoxSupporterResign.SelectedValue != null)
            {
                int supporterId = Convert.ToInt32(ComboBoxSupporterResign.SelectedValue);

                var result = MessageBox.Show("Are you certain you wish to resign " + ComboBoxSupporterResign.Text, "Confirm",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (_handler.LookForAdmin().Rows.Count == 1)
                {
                    MessageBox.Show("There is only 1 admin left. Resignation cannot take place.");
                }
                else
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        _handler.ResignSupporter(supporterId);
                        PopulateCombo();
                        MessageBox.Show("Supporter was successfully resigned");
                    }
                }

            }
        }

        private void AssignSupporter_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComboBoxSupporterAssign.SelectedValue != null || ComboBoxUserAccessAssign.SelectedValue != null)
            {
                int supporterId = Convert.ToInt32(ComboBoxSupporterAssign.SelectedValue);
                int accessId = Convert.ToInt32(ComboBoxUserAccessAssign.SelectedValue);

                var result = MessageBox.Show("Are you certain you wish to assign " + ComboBoxSupporterAssign.Text + " The status of " + ComboBoxUserAccessAssign.Text, "Confirm",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (_handler.LookForAdmin().Rows.Count == 1 && accessId == 2)
                {
                    MessageBox.Show("There is only 1 admin left. The assignation cannot take place.");
                }
                else
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        _handler.AssignSupporter(supporterId, accessId);
                        PopulateCombo();
                        MessageBox.Show("Supporter was successfully assigned");
                    }
                }

            }
        }
    }
}
