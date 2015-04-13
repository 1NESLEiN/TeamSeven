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
                if (TextBoxSupporterName.Text == String.Empty || TextBoxSupporterInitials.Text == String.Empty)
                {
                    MessageBox.Show("Udfyld alle felter", "Udfyld felter", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    bool success = _handler.AddSupporter(TextBoxSupporterName.Text, TextBoxSupporterInitials.Text);
                    if (success)
                    {
                        MessageBox.Show("Supporter added succesfully");
                        TextBoxSupporterName.Clear();
                        TextBoxSupporterInitials.Clear();
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
            //Get data Populate ComboBoxSupporter
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
        }

        private void RemoveSupporter_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComboBoxSupporter.SelectedValue != null)
            {
                int sup = (int) ComboBoxSupporter.SelectedValue;

                var result = MessageBox.Show("Are you certain you wish to remove " + ComboBoxSupporter.Text, "Confirm",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    //Fucking delete employee
                    Console.WriteLine(sup+"");
                }
            }
        }
    }
}
