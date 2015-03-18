using System.Data.Entity;
using System.Windows;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddSupporter(object sender, RoutedEventArgs e)
        {
            txtBoxName.Clear();
            txtBoxInitials.Clear();
        }

    }
}
