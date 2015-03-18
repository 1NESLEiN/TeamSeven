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
using System.Windows.Shapes;
using Control;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for AddDocumentationWindow.xaml
    /// </summary>
    public partial class AddDocumentationWindow : Window
    {
        private Handler _handler;
        public AddDocumentationWindow()
        {
            InitializeComponent();
            _handler = Handler.GetInstance();
        }

        private void AddDocumentation_OnClick(object sender, RoutedEventArgs e)
        {
            bool success = _handler.AddDocumentation(Convert.ToInt32(TextBoxType.Text), TextBoxHeadline.Text, TextBoxDescription.Text, DateTime.Now, Convert.ToInt32(TextBoxTimeSpent.Text), Convert.ToInt32(TextBoxSupporter.Text));
            if (success)
            {
                MessageBox.Show("Documentation added succesfully");
                ClearTextboxes();
            }
        }

        private void ClearTextboxes()
        {
            TextBoxType.Clear();
            TextBoxHeadline.Clear();
            TextBoxDescription.Clear();
            TextBoxTimeSpent.Clear();
            TextBoxSupporter.Clear();
        }
    }
}
