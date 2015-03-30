using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddSupporterPage.xaml
    /// </summary>
    public partial class AddSupporterPage : Page
    {
        private readonly Handler _handler;

        public AddSupporterPage()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
        }

        private void AddSupporter_Click(object sender, RoutedEventArgs e)
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
    }
}
