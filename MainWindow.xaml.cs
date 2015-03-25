using System.Windows;
using System.Windows.Controls;
using DevelopmentProject.PeterGUI.Pages;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page[] _pages = new Page[] {new AddDocumentationPage(), new AddSupporterPage(), new SearchDocumentationPage() };

        public MainWindow()
        {
            InitializeComponent();
        }
        //private void BtnAddSupporter_OnClick(object sender, RoutedEventArgs e)
        //{
        //    AddSupporterWindow addSupporterWindow = new AddSupporterWindow();
        //    addSupporterWindow.Show();
        //}

        //private void BtnAddDocumentation_OnClick(object sender, RoutedEventArgs e)
        //{
        //    AddDocumentationWindow addDocumentationWindow = new AddDocumentationWindow();
        //    addDocumentationWindow.Show();
        //}

        private void AddDocumentation_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = _pages[0];
        }

        private void AddSupporter_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = _pages[1];
        }

        private void SearchDocumentation_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = _pages[2];
        }
    }
}
