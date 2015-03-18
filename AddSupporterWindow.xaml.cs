using System;
using System.Windows;
using Control;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for AddSupporterWindow.xaml
    /// </summary>
    public partial class AddSupporterWindow : Window
    {
        public AddSupporterWindow()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
        }

        private readonly Handler _handler;

        private void AddSupporter(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtBoxName.Text == String.Empty || txtBoxInitials.Text == String.Empty)
                {
                    MessageBox.Show("Udfyld alle felter", "Udfyld felter", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    bool success = _handler.AddSupporter(txtBoxName.Text, txtBoxInitials.Text);
                    if (success)
                    {
                        MessageBox.Show("Supporter added succesfully");
                        txtBoxName.Clear();
                        txtBoxInitials.Clear();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Der opstod en fejl: " + exception.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
