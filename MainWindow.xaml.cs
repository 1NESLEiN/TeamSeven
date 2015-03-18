using System;
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
        public MainWindow()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
        }

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
                    _handler.AddSupporter(txtBoxName.Text, txtBoxInitials.Text);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Der opstod en fejl: " + exception.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
